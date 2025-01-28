using System.ComponentModel;
using System.Windows.Input;
using SkiApp.ViewModels;
namespace SkiApp.Views;

public partial class Tips : ContentPage
{
	public Tips(TipsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}