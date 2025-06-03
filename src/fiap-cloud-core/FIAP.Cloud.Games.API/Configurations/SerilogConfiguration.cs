using Serilog;

namespace FIAP.Cloud.Games.API.Configurations
{
    public static class SerilogConfiguration
    {
        public static Serilog.ILogger GetSerilogConfiguration()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}
