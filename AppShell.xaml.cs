using SkiApp.ViewModels;
using SkiApp.Views;

namespace SkiApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
            Routing.RegisterRoute("SignUp", typeof(SignUp));
            Routing.RegisterRoute("Login", typeof(LogInPage));
        }
    }
}
