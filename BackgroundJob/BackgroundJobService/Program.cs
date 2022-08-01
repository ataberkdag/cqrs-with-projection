using BackgroundJobService;
using BackgroundJobService.Services.Implementations;
using BackgroundJobService.Services.Interfaces;
using MassTransit;

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
        var commandProjectDbString = Configuration.GetConnectionString("CommandProjectDb");

        services.AddSingleton<IDbConnectionFactory>(x => new PostgreDbConnectionFactory(Configuration.GetConnectionString("CommandProjectDb")));
        services.AddSingleton<IMassTransitHandler, MassTransitHandler>();

        services.AddMassTransit(_ => {
            _.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host: Configuration.GetConnectionString("RabbitMQ"), h =>
                {
                    h.Username(Configuration.GetSection("RabbitMQ")["UserName"]);
                    h.Password(Configuration.GetSection("RabbitMQ")["Password"]);
                });

            });
        });

        services.AddHostedService<CommandProjectOutboxWorker>();
    })
    .Build();

await host.RunAsync();
