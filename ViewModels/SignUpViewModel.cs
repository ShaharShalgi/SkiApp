using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.ViewModels;
using System.Windows.Input;
using SkiApp.Services;
using SkiApp.Models;
namespace SkiApp.ViewModels;

public class SignUpViewModel : ViewModelBase
{
    private SkiServiceWebAPIProxy proxy;
    public SignUpViewModel(SkiServiceWebAPIProxy proxy)
    {
        this.proxy = proxy;
        RegisterCommand = new Command(OnRegister);
        CancelCommand = new Command(OnCancel);
        //ShowPasswordCommand = new Command(OnShowPassword);
        //IsPassword = true;
        ProfessionalSelectedCommand = new Command(ProfessionalSelected, () => !IsProfessional);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsProfessional);
        NameError = "Name is required";
        IsProfessional = false;
        EmailError = "Email is required";
        PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
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

    private string username;

    public string Username
    {
        get => username;
        set
        {
            username = value;
            ValidateName();
            OnPropertyChanged("Username");
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




    private string email;

    public string Email
    {
        get => email;
        set
        {
            email = value;
            ValidateEmail();
            OnPropertyChanged("Email");
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
    private bool isProfessional;
    public bool IsProfessional
    {
        get { return isProfessional; }
        set
        {
            isProfessional = value;
            OnPropertyChanged();
            ((Command)ProfessionalSelectedCommand).ChangeCanExecute();
            ((Command)VisitorSelectedCommand).ChangeCanExecute();
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

    private string password;

    public string Password
    {
        get => password;
        set
        {
            password = value;
            ValidatePassword();
            OnPropertyChanged("Password");
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
    private string loc;
    public string Loc
    {
        get => loc;
        set
        {
            loc = value;
            OnPropertyChanged("Loc");
        }
    }
    private int typeId;
    public int TypeId
    {
        get => typeId;
        set
        {
            typeId = value;
            OnPropertyChanged("TypeId");
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
    private double price;
    public double Price
    {
        get => price;
        set
        {
            price = value;
            OnPropertyChanged("Price");
        }
    }
    private string txt;
    public string Txt
    {
        get => txt;
        set
        {
            txt = value;
            OnPropertyChanged("Txt");
        }
    }
  


    public ICommand RegisterCommand { get; }  
    public ICommand CancelCommand { get; }

    private async void OnRegister()
    {
        ValidateName();
        ValidateEmail();
        ValidatePassword();
        if (!ShowPasswordError && !ShowEmailError && !ShowNameError)
        {

            if (IsProfessional)
            {

                //Create a new user that is a manager
                var newUser = new VisitorInfo
                {
                    Username = this.Username,
                    Pass = this.Password,
                    Email = this.Email,
                    Gender = this.Gender,
                    UserID = 0
                };
                newUser = await proxy.SignUp(newUser);

                var newPro = new ProfessionalInfo
                {
                    Loc = this.Loc,
                    TypeId = this.TypeId + 1,
                    Price = this.Price,
                    UserId = newUser.UserID,
                    Txt = this.Txt,
                    Rating = 0
                };
            

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                
                newPro = await proxy.SignUpPro(newPro);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    InServerCall = false;

                    //ASK OFER

                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }

            }


            if (!IsProfessional)
            {
                var newUser = new VisitorInfo
                {
                    Username = this.Username,
                    Pass = this.Password,
                   Email = this.Email,
                   Gender = this.Gender,
                    UserID = 0
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newUser = await proxy.SignUp(newUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    InServerCall = false;

                    //ASK OFER NAVIGATE TO WHERE?

                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
            }

        }
        else
        {

            //If the registration failed, display an error message
            string errorMsg = "Registration failed. Please try again.";
            await Application.Current.MainPage.DisplayAlert("Registration", "failed", "ok");
        }

    }
    public void OnCancel()
    {
        //Navigate back to the login page
        ((App)(Application.Current)).MainPage.Navigation.PopAsync();
    }


    //נחבר את זה לכפתורים
    public ICommand ProfessionalSelectedCommand { get; set; }
    public ICommand VisitorSelectedCommand { get; set; }



    //נחבר את זה אם המנהל הוא האופציה הנבחרת
  

    




    private async void ProfessionalSelected()
    {
        IsProfessional = true;

    }

    private async void VisitorSelected()
    {
        IsProfessional = false;
    }


}