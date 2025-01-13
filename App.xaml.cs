using SkiApp.Models;
using SkiApp.ViewModels;
using SkiApp.Views;
namespace SkiApp
{
    public partial class App : Application
    {

        public VisitorInfo? LoggedInUser { get; set; }
        public App(IServiceProvider serviceProvider)
        {

            InitializeComponent();
            Homepage? v = serviceProvider.GetService<Homepage>();
            //SignUpView? v = serviceProvider.GetService<SignUpView>();

            MainPage = new NavigationPage(v);
        }
    }
}
    

