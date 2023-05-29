using UI.ViewModels;

namespace UI.Pages;

public partial class AddCategoryPage : ContentPage
{

	private AddCategoryViewModel _vm;
	public AddCategoryPage(AddCategoryViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}