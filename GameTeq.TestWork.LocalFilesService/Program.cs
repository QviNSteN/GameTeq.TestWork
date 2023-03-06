using GameTeq.TestWork.General.FileService.Engines;
using GameTeq.TestWork.General.FileService.Options;
using GameTeq.TestWork.LocalFilesService.BI.Workers;
using GameTrq.TestWork.General.Redis.Engines;
using GameTrq.TestWork.General.Redis.Interfaces;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redis.OM;
using System.Diagnostics.CodeAnalysis;
using GameTeq.TestWork.LocalFilesService;
using GameTeq.TestWork.LocalFilesService.Options;
using GameTeq.TestWork.LocalFilesService.BI;

[ExcludeFromCodeCoverage]
public class Program
{
    private static async Task Main()
    {
        IConfiguration Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

        using IHost host = Host.CreateDefaultBuilder()
        .ConfigureServices((host, services) =>
        {

            services.AddHostedService<FilesChecker>();
            services.AddHostedService<FilesSender>();

            services.Configure<Config>(host.Configuration);

            var ff = host.Configuration.GetValue<string>("Redis");

            services.AddSingleton(new RedisConnectionProvider(host.Configuration.GetValue<string>("Redis")));

            services.AddTransient<DiskService>();

            services.AddTransient<FileRedisService>();
            services.AddTransient<IRedisFileToWork>(x => x.GetRequiredService<FileRedisService>());
            services.AddTransient<IRedisNewFiles>(x => x.GetRequiredService<FileRedisService>());

            services.AddGrpcClient<FilesTransfer.FilesTransferClient>(o =>
            {
                o.Address = new Uri(host.Configuration["InputFilesHost"]);
            });
        })
        .Build();

        await host.RunAsync();
    }
}