using SkiApp.ViewModels;

namespace SkiApp.Views;

public partial class Homepage : ContentPage
{
	public Homepage(HomepageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}