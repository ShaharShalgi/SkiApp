using SkiApp.ViewModels;
namespace SkiApp.Views;

public partial class Profile : ContentPage
{
	public Profile(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}