using UI.ViewModels;

namespace UI.Pages;

public partial class LogInPage : ContentPage
{

	private LogInViewModel _vm;
	public LogInPage(LogInViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}