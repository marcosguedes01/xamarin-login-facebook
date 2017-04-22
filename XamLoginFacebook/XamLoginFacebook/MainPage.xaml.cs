using Xamarin.Forms;

namespace XamLoginFacebook
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            
            this.btnLogar.Clicked += BtnLogar_Clicked;
        }

        private void BtnLogar_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new Login();
        }
    }
}
