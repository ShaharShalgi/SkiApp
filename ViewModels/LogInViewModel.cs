namespace SkiApp.ViewModels;

public class LogInViewModel : ViewModelBase
{
    private string username;
    public string Username
    {
        get => Username;
        set
        {
            if (Username != value)
            {
                Username = value;
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
    public LogInViewModel()
	{
		
	}
}