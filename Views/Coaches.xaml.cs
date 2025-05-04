
using SkiApp.ViewModels;

namespace SkiApp.Views;

public partial class Coaches : ContentPage
{
	public Coaches(CoachesViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
        
	}
}