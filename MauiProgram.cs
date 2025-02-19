using Microsoft.Extensions.Logging;
using SkiApp.Views;
using SkiApp.ViewModels;
using SkiApp.Services;

namespace SkiApp
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
                    fonts.AddFont("Freshman.ttf", "Freshman");
                    fonts.AddFont("Polar Snow.ttf", "Snow");
                });
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<SignUp>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<Tips>();
            builder.Services.AddTransient<TipsViewModel>();
            builder.Services.AddTransient<Resorts>();
            builder.Services.AddTransient<Coaches>();
            builder.Services.AddTransient<Profile>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<Homepage>();
            builder.Services.AddTransient<HomepageViewModel>();
            builder.Services.AddTransient<UpgradePro>();
            builder.Services.AddTransient<UploadPost>();
            builder.Services.AddSingleton<SkiServiceWebAPIProxy>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
