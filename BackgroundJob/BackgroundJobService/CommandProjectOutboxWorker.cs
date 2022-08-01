using BackgroundJobService.Services.Interfaces;
using Dapper;
using Newtonsoft.Json;
using Shared.Domain.Entities;

namespace BackgroundJobService
{
    public class CommandProjectOutboxWorker : BackgroundService
    {
        private readonly ILogger<CommandProjectOutboxWorker> _logger;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IMassTransitHandler _massTransitHandler;

        public CommandProjectOutboxWorker(ILogger<CommandProjectOutboxWorker> logger,
            IDbConnectionFactory dbConnectionFactory,
            IMassTransitHandler massTransitHandler)
        {
            _logger = logger;
            _dbConnectionFactory = dbConnectionFactory;
            _massTransitHandler = massTransitHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                string sql = $@"         SELECT
                                          ""Id"",
                                          ""Type"",
                                          ""Data"",
                                          ""QueueName"",
                                          ""CreatedOn""
                                     FROM public.""OutboxMessages""
                                     ORDER BY ""Id""
                                     LIMIT 100 FOR UPDATE SKIP LOCKED; 
                                ";

                var connection = _dbConnectionFactory.GetOpenConnection();

                var messages = await connection.QueryAsync<OutboxMessage>(sql);

                var listOfIds = new List<Guid>();
                foreach (var outboxMessage in messages)
                {
                    try
                    {
                        var outboxMessageData = JsonConvert.DeserializeObject(outboxMessage.Data, Type.GetType(outboxMessage.Type));

                        if (!String.IsNullOrEmpty(outboxMessage.QueueName))
                        {
                            await this._massTransitHandler.Send(outboxMessage.QueueName, outboxMessageData);
                        }
                        else
                        {
                            await this._massTransitHandler.Publish(outboxMessageData);
                        }

                        listOfIds.Add(outboxMessage.Id);
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError($"Error: {ex?.Message}");
                    }
                }

                if (listOfIds.Count > 0)
                {
                    var transaction = connection.BeginTransaction(); // Isnt it a bug? It has to begin transaction in foreach...
                    await connection.ExecuteAsync($@"DELETE FROM public.""OutboxMessages"" WHERE ""Id"" IN ('{string.Join("','", listOfIds)}')");
                    transaction.Commit();
                }
            }
        }
    }
}
