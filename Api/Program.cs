using Serilog;
using Serilog.Debugging;

namespace Api;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                SelfLog.Enable(Console.Error);
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            });

}