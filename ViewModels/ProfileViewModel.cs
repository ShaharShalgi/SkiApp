using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SkiApp.Models;
using SkiApp.Services;
using SkiApp.Views;

namespace SkiApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private SkiServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ProfileViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            VisitorInfo u = ((App)Application.Current).LoggedInUser;
            Username = u.Username;
            Gender = u.Gender;
            Email = u.Email;
            Password = u.Pass;
        }


        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                //ValidateName();
                OnPropertyChanged("Username");
            }
        }


        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                //ValidatePassword();
                OnPropertyChanged("Password");
            }
        }
        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                //ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

    }

}
