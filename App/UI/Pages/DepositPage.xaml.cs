using UI.ViewModels;

namespace UI.Pages;

public partial class DepositPage : ContentPage
{
    private DepositViewModel _vm;


    public DepositPage(DepositViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}