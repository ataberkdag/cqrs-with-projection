using MassTransit;
using ProjectorService;
using ProjectorService.Consumers;
using ProjectorService.Services.Impl;
using ProjectorService.Services.Interfaces;
using Shared.Messages;

IConfiguration Configuration = null;

IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, cfg) =>
        {

            var env = hostingContext.HostingEnvironment;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        })
    .ConfigureServices(services =>
    {

        services.AddMassTransit(x => {

            x.AddConsumer<TodoItemMessageConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host: Configuration.GetConnectionString("RabbitMQ"), h =>
                {
                    h.Username(Configuration.GetSection("RabbitMQ")["UserName"]);
                    h.Password(Configuration.GetSection("RabbitMQ")["Password"]);
                });

                cfg.ReceiveEndpoint(RabbitMqConsts.TodoItemCreatedQueueName, e => {
                    e.ConfigureConsumer<TodoItemMessageConsumer>(context);
                });

            });
        });

        services.AddStackExchangeRedisCache(options => {
            options.Configuration = Configuration.GetConnectionString("Cache");
        });

        services.AddSingleton<ICacheService, RedisCacheService>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
