using Microsoft.Extensions.Logging;
using MQTT;
using System;

namespace DoAn
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            string AppDataDir = FileSystem.AppDataDirectory;
            string CacheDataDir = FileSystem.CacheDirectory;
            //System.File.Instance.CreateNewDatafolder("app_data", AppDataDir);
            System.File.Instance.CreateNewDatafolder("cache_data", CacheDataDir);

            Broker.Instance.Connect();


            return builder.Build();
        }
    }
}
