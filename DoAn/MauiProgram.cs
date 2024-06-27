using DoAn.Models;
using DoAn.Services;
using DoAn.ViewModels;
using DoAn.ViewModels.AdminViewModel;
using DoAn.Views;
using DoAn.Views.AdminView;
using DoAn.Views.Loading;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using MQTT;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;
using System;
using Microsoft.Maui.LifecycleEvents;
using System.Timers;


namespace DoAn
{
    public static class MauiProgram
    {
        private static System.Timers.Timer backgroundTimer;
        private static DateTime backgroundStartTime;

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMicrocharts()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android
                        .OnStart((activity) => {
                            StopBackgroundTimer();
                        })
                        .OnStop((activity) => {
                            StartBackgroundTimer();
                        })
                        .OnDestroy((activity) => {
                            PerformCleanupCode();
                        })
                        //.OnBackPressed((activity) => {
                        //    PerformCleanupCode();
                        //    return false;  // Để cho phép đóng ứng dụng
                        //})
                        );
#endif
                });





#if DEBUG
            builder.Logging.AddDebug();
#endif
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXlfeHVUQ2dcVEZxXEQ=");

            new MQTT.Broker();
            string AppDataDir = FileSystem.AppDataDirectory;
            string CacheDataDir = FileSystem.CacheDirectory;
            //System.File.Instance.CreateNewDatafolder("app_data", AppDataDir);
            System.File.Instance.CreateNewDatafolder("cache_data", CacheDataDir);
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<UserModel>();
            builder.Services.AddScoped<StationModel>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddTransient<LoadingViewModel>();
            //
            builder.Services.AddTransient<StationListViewModel>();
            builder.Services.AddTransient<StationProfileViewModel>();
            builder.Services.AddTransient<UserListViewModel>();
            builder.Services.AddTransient<UserProfileViewModel>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            //
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<UserView>();
            builder.Services.AddSingleton<HomeView>();
            builder.Services.AddTransient<StationDetailView>();
            builder.Services.AddTransient<AdminPageView>();
            builder.Services.AddTransient<AddUserView>();
            //
            builder.Services.AddTransient<StationListView>();
            builder.Services.AddTransient<UserListView>();
            builder.Services.AddTransient<StationProfileView>();
            builder.Services.AddTransient<UserProfileView>();
            //
            builder.Services.AddTransient<CheckingNetworkView>();
            builder.Services.AddTransient<CheckingLoginView>();
            builder.Services.AddTransient<LoadingServerView>();
            builder.Services.AddTransient<LoadingView>();
            builder.Services.AddTransient<WaitingView>();


            builder.Services.AddTransient<Views.ListView>();


            


            return builder.Build();
        }

        private static void StartBackgroundTimer()
        {
            backgroundStartTime = DateTime.Now;
            backgroundTimer = new System.Timers.Timer(1000);  // Kiểm tra mỗi giây
            backgroundTimer.Elapsed += CheckBackgroundTime;
            backgroundTimer.Start();
        }

        private static void StopBackgroundTimer()
        {
            backgroundTimer?.Stop();
            backgroundTimer?.Dispose();
        }

        private static void CheckBackgroundTime(object sender, ElapsedEventArgs e)
        {
            if ((DateTime.Now - backgroundStartTime).TotalSeconds > 30)
            {
                PerformCleanupCode();
                StopBackgroundTimer();
            }
        }

        private static void PerformCleanupCode()
        {
            Broker.Instance.StopListeningAll();
            Broker.Instance.Send($"dane/user/logout/{Service.Instance.UserID}", new Document()
            {
                Token = Service.Instance.Token,
            });
        }
    }
}
