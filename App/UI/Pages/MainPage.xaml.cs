using UI.ViewModels;

namespace UI.Pages;

public partial class MainPage : ContentPage
{

	private MainViewModel _vm;

    
    public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}

  


}