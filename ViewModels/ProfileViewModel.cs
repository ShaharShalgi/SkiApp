using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using SkiApp.Models;
using SkiApp.Services;
using SkiApp.Views;
//using static Java.Util.Jar.Attributes;

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
            UpdateRequestCommand = new Command(RequestUpdate);
            SaveCommand = new Command(OnSave);
            NameError = "Name is required";
            UpdateRequest = false;
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
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
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }
        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }
        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Username);
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
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }
        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }
        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
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
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }
        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        private bool updateRequest;
        public bool UpdateRequest
        {
            get { return updateRequest; }
            set
            {
                updateRequest = value;
                OnPropertyChanged();
                ((Command)UpdateRequestCommand).ChangeCanExecute();
               
            }
        }
        public ICommand UpdateRequestCommand { get; }

        private async void RequestUpdate()
        {
            UpdateRequest = !UpdateRequest;

        }


        public ICommand SaveCommand { get; }


        private async void OnSave()
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();
            if (!ShowPasswordError && !ShowEmailError && !ShowNameError)
            {


                VisitorInfo theUser = ((App)App.Current).LoggedInUser;
                theUser.Username = Username;
                theUser.Gender = Gender;
                theUser.Email = Email;
                theUser.Pass = Password;
               
                InServerCall = true;
                bool success = await proxy.UpdateUser(theUser);

                if (success)
                {
                    RequestUpdate();
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                    
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "Save Profile failed. Please try again.";
                    await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
                }

            }

           

        }

    }

    }
