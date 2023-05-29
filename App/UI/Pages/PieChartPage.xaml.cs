using Application.Services;
using UI.Storages;
using UI.ViewModels;

namespace UI.Pages;

public partial class PieChartPage : ContentPage
{
	private PieChartViewModel _vm;
	public PieChartPage(PieChartViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
	}
}