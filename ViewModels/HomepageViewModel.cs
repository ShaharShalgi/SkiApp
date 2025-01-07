using SkiApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Models;
using SkiApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace SkiApp.ViewModels
{
    public class HomepageViewModel : Homepage
    {
        public HomepageViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
           
            OnLoginCommand = new Command(OnLogin);
            OnRegisterCommand = new Command(OnRegister);
        }
        private IServiceProvider serviceProvider;

        public ICommand OnLoginCommand { get; }
        public ICommand OnRegisterCommand { get; }
        private void OnRegister()
        {

            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<SignUp>());
        }
        private void OnLogin()
        {
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<LogInPage>());
        }
}
