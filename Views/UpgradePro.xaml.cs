
using SkiApp.ViewModels;
namespace SkiApp.Views;

public partial class UpgradePro : ContentPage
{
	public UpgradePro(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}