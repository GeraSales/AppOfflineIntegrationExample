using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;

#if ANDROID
using PocSharedFile.Platforms.Android;
#endif

namespace PocSharedFile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUserDialogs(registerInterface: true)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

#if ANDROID
            builder.Services.AddTransient<IBroadcast, MainActivity>();
            builder.Services.AddTransient<IBroadcastResponse, BroadcastResponseService>();
            

#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
