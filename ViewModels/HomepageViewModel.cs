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
using SkiApp.Services;

namespace SkiApp.ViewModels
{
    public class HomepageViewModel : ViewModelBase
    {
        public HomepageViewModel(IServiceProvider serviceProvider, SkiServiceWebAPIProxy proxy)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy; 
            OnLoginCommand = new Command(OnLogin);
            OnRegisterCommand = new Command(OnRegister);
        }
        private IServiceProvider serviceProvider;
        private SkiServiceWebAPIProxy proxy;


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
}
