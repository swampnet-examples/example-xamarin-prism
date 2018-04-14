using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace PrismApp.Views
{
    public partial class ModalPage : PopupPage
    {
        public ModalPage()
        {
            InitializeComponent();
        }

		protected override bool OnBackButtonPressed()
		{
			// Disable back buitton
			return true;
		}
	}
}
