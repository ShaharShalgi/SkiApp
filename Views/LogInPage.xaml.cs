using SkiApp.ViewModels;

namespace SkiApp.Views;

public partial class LogInPage : ContentPage
{
	public LogInPage(LogInViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}