using SkiApp.ViewModels;
using System.ComponentModel;
using System.Windows.Input;
namespace SkiApp.Views;

public partial class SignUp : ContentPage
{
	public SignUp(SignUpViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}