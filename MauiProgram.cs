using Microsoft.Extensions.Logging;

namespace Cinepolis
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
