using SkiApp.ViewModels;
namespace SkiApp.Views;

public partial class UploadReview : ContentPage
{
	public UploadReview(ReviewViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}