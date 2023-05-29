namespace UI.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

        if (App.UserDetails != null)
        {
            lblUserName.Text = App.UserDetails.Name;
            lblUserEmail.Text = App.UserDetails.Email;
        }
    }
}