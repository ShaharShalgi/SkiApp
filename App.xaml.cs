using SkiApp.ViewModels;
using SkiApp.Views;
namespace SkiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
