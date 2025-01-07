namespace SkiApp.ViewModels;
using SkiApp.Models;
using SkiApp.Views;
using SkiApp.Services;
using Microsoft.Win32;
using System.Windows.Input;

public class LogInViewModel : ViewModelBase
{
    private string username;
    public string Username
    {
        get => username;
        set
        {
            if (username != value)
            {
                username = value;
                OnPropertyChanged(nameof(Username));
               
                // can add error message property and set it here
            }
        }
    }
    private string password;
    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
               
                // can add error message property and set it here
            }
        }
    }
    private SkiServiceWebAPIProxy proxy;
    private IServiceProvider serviceProvider;
    public LogInViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        LoginCommand = new Command(OnLogin);
        RegisterCommand = new Command(OnRegister);
        username = "";
        password = "";
        InServerCall = false;
        errorMsg = "";
    }

    private string errorMsg;
    public string ErrorMsg
    {
        get => errorMsg;
        set
        {
            if (errorMsg != value)
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
    }


    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }



    private async void OnLogin()
    {
        //Choose the way you want to blobk the page while indicating a server call
        InServerCall = true;
        ErrorMsg = "";
        //Call the server to login
        VisitorInfo loginInfo = new VisitorInfo { Username = Username, Pass = Password };
        VisitorInfo? u = await this.proxy.LoginAsync(loginInfo);

        InServerCall = false;

        //Set the application logged in user to be whatever user returned (null or real user)
        ((App)Application.Current).LoggedInUser = u;
        if (u == null)
        {
            ErrorMsg = "Invalid name or password";
        }
        else
        {
            ErrorMsg = "";
            //Navigate to the main page
            AppShell shell = serviceProvider.GetService<AppShell>();

            ((App)Application.Current).MainPage = shell;
            Shell.Current.FlyoutIsPresented = false; //close the flyout
            
        }
    }

   
    } private void OnRegister()
    {
        ErrorMsg = "";
        Username = "";
        Password = "";
        // Navigate to the Register View page
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<SignUp>());

}