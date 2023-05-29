using UI.ViewModels;

namespace UI.Pages;

public partial class AddWalletPage : ContentPage
{

	private AddWalletViewModel _vm;

	public AddWalletPage(AddWalletViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}