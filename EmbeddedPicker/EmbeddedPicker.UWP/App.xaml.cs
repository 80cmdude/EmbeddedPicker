using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace EmbeddedPicker.UWP {
	sealed partial class App : Application {
		public App() {
			InitializeComponent();
			Suspending += OnSuspending;
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e) {
			var rootFrame = Window.Current.Content as Frame;

			if (rootFrame is null) {
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;
				Xamarin.Forms.Forms.Init(e);

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {	}

				Window.Current.Content = rootFrame;
			}

			if (!e.PrelaunchActivated) {
				if (rootFrame.Content is null) 
					rootFrame.Navigate(typeof(MainPage), e.Arguments);

				Window.Current.Activate();
			}
		}

		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e) =>
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);

		private void OnSuspending(object sender, SuspendingEventArgs e) =>
			e.SuspendingOperation.GetDeferral().Complete();
	}
}
