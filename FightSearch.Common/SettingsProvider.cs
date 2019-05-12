namespace FightSearch.Common
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public static class SettingsProvider
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IHostingEnvironment HostingEnvironment { get; set; }
        public static ILoggerFactory LoggerFactory { get; set; }

        public static void SetConfiguration(IConfigurationRoot configurationRoot)
        {
            Configuration = configurationRoot;
        }

        public static void SetHostingEnvironment(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        public static void SetLoggerFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }
    }
}