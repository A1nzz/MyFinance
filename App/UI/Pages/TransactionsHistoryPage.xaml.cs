using UI.ViewModels;

namespace UI.Pages;

public partial class TransactionsHistoryPage : ContentPage
{

	TransactionsHistoryViewModel _vm;
	public TransactionsHistoryPage(TransactionsHistoryViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}