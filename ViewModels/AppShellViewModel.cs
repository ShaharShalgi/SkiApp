﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SkiApp.Models;
using SkiApp.Views;

namespace SkiApp.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        private VisitorInfo? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
            LogoutCommand = new Command(OnLogout);
        }
        public ICommand LogoutCommand { get; }



        //this command will be used for logout menu item
        //public Command LogoutCommand
        //{
        //    get
        //    {
        //        return new Command(OnLogout);
        //    }
        //}
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<Homepage>());
        }
    }
}
