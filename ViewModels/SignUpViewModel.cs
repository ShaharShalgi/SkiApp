using SkiApp.ViewModels;
using System.Windows.Input;
namespace SkiApp.ViewModels;

public class SignUpViewModel : ViewModelBase
{
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
    public ICommand ProffesionalSelectedCommand { get; set; }
    public ICommand VisitorSelectedCommand { get; set; }



    //נחבר את זה אם המנהל הוא האופציה הנבחרת
    private bool isProffesional;
    public bool IsProffesional
    {
        get { return isProffesional; }
        set
        {
            isProffesional = value;
            OnPropertyChanged();
            ((Command)ProffesionalSelectedCommand).ChangeCanExecute();
            ((Command)VisitorSelectedCommand).ChangeCanExecute();
        }
    }

    public SignUpViewModel()
    {

        ProffesionalSelectedCommand = new Command(ProffesionalSelected, () => !IsProffesional);
        VisitorSelectedCommand = new Command(VisitorSelected, () => IsProffesional);
        IsProffesional = false;

    }




    private async void ProffesionalSelected()
    {
        IsProffesional = true;

    }

    private async void VisitorSelected()
    {
        IsProffesional = false;
    }


}