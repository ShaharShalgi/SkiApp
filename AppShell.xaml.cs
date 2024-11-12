using SkiApp.ViewModels;
using SkiApp.Views;

namespace SkiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("SignUp", typeof(SignUp));
            Routing.RegisterRoute("Login", typeof(LogInPage));
        }
    }
}
