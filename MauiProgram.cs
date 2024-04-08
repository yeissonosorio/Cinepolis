using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace Cinepolis
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Caveat-Regular.ttf", "CaveatRegular");
                    fonts.AddFont("ProtestGuerrilla-Regular.ttf", "ProtestGuerrillaRegular");
                    fonts.AddFont("Oswald-Regular.ttf", "OswaldRegular");
                });


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
