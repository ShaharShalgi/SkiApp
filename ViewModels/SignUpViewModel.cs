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
    //public RegisterViewModel(SkiWebAPIProxy proxy)
    //{
    //    this.proxy = proxy;
    //    RegisterCommand = new Command(OnRegister);
    //    CancelCommand = new Command(OnCancel);
    //    ShowPasswordCommand = new Command(OnShowPassword);
    //    UploadPhotoCommand = new Command(OnUploadPhoto);
    //    PhotoURL = proxy.GetDefaultProfilePhotoUrl();
    //    LocalPhotoPath = "";
    //    IsPassword = true;
    //    NameError = "Name is required";
    //    LastNameError = "Last name is required";
    //    EmailError = "Email is required";
    //    PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
    //}

    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; OnPropertyChanged(); }
    }


    private string? passError;
    public string PassError
    {
        get { return passError; }
        set
        {
            passError = value; OnPropertyChanged();
        }
    }

    private string password;
    public string? Password
    {
        get { return password; }

        set
        {
            password = value;
            PassError = "";
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(PassError));
            if (string.IsNullOrEmpty(password))
            {
                PassError = "";
            }
            else
            {
                if (password != null)
                {
                    bool IsPasswordOkay = IsValidPassword(password);
                    if (!IsPasswordOkay)
                    {
                        PassError = "סיסמה לא טובה ";
                    }
                }

            }
        }
    }


    private bool IsValidPassword(string password)
    {
        bool hasUpperCase = false;
        bool hasDigit = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                hasUpperCase = true;
            }
            if (char.IsDigit(c))
            {
                hasDigit = true;
            }

            if (hasUpperCase && hasDigit)
            {
                break; // אם מצאנו כבר גם אות גדולה וגם ספרה, אפשר לעצור את הלולאה
            }
        }
        return hasUpperCase && hasDigit;

    }

    //נחבר את זה לכפתורים
    public ICommand ProfessionalSelectedCommand { get; set; }
    public ICommand VisitorSelectedCommand { get; set; }



    //נחבר את זה אם המנהל הוא האופציה הנבחרת
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

    public SignUpViewModel()
    {

        ProfessionalSelectedCommand = new Command(ProfessionalSelected, () => !IsProfessional);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsProfessional);
        IsProfessional = false;

    }




    private async void ProfessionalSelected()
    {
        IsProfessional = true;

    }

    private async void VisitorSelected()
    {
        IsProfessional = false;
    }


}