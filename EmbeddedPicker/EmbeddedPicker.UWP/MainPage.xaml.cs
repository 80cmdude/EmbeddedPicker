using Xamarin.Forms.Platform.UWP;

namespace EmbeddedPicker.UWP {
	public sealed partial class MainPage : WindowsPage {
		public MainPage() {
			InitializeComponent();

			LoadApplication(new EmbeddedPicker.App());
		}
	}
}
