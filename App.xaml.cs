using SkiApp.ViewModels;
using SkiApp.Views;
namespace SkiApp
{
    public partial class App : Application
    {
        public App(LogInViewModel vm)
        {
            InitializeComponent();

            MainPage = new LogInPage(vm);
        }
    }
}
