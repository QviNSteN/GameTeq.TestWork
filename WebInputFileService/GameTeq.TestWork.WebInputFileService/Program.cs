using GameTeq.TestWork.General.FileService.Engines;
using GameTeq.TestWork.General.FileService.Intefaces;
using GameTeq.TestWork.OutputLocalFilesService.BI.Services;
using GameTeq.TestWork.WebInputFileService.BI.Interfaces;
using GameTeq.TestWork.WebInputFileService.Services;

namespace GameTeq.TestWork.WebInputFileService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            builder.Services.AddTransient<IFiles, FilesService>();

            builder.Services.AddTransient<IDiskWriter, DiskService>();

            var app = builder.Build();

            app.MapGrpcService<InputService>();
            app.MapGet("/", () => "Error");

            app.Run();
        }
    }
}