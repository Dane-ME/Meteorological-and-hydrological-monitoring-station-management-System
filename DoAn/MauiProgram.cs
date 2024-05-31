using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels;
using DoAn.Views;
using DoAn.Views.Loading;
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

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<UserModel>();
            builder.Services.AddSingleton<StationModel>();


            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<LoadingViewModel>();


            builder.Services.AddTransient<LoadingView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<UserView>();
            builder.Services.AddTransient<HomeView>();

            builder.Services.AddTransient<CheckingNetworkView>();
            builder.Services.AddTransient<CheckingLoginView>();
            builder.Services.AddTransient<LoadingServerView>();
            builder.Services.AddTransient<Views.ListView>();


            


            return builder.Build();
        }
    }
}
