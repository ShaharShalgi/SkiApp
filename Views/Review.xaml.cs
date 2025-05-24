using SkiApp.ViewModels;
namespace SkiApp.Views;

public partial class Review : ContentPage
{
	public Review(ReviewViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}