using SkiApp.ViewModels;
namespace SkiApp.Views;

public partial class UploadPost : ContentPage
{
	public UploadPost(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}