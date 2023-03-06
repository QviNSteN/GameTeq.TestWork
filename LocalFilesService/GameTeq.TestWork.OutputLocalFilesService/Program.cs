using GameTeq.TestWork.OutputLocalFilesService.Services;

namespace GameTeq.TestWork.OutputLocalFilesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<GetFilesService>();
            app.MapGet("/", () => "Error");

            app.Run();
        }
    }
}