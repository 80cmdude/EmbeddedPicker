using EmbeddedPicker;
using EmbeddedPicker.UWP;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UWP;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EmbeddedPicker.UWP {
	public class CustomPickerRenderer : ViewRenderer<CustomPicker, ListBox> {
		protected override void OnElementChanged(ElementChangedEventArgs<CustomPicker> e) {
			base.OnElementChanged(e);

			if (Control is null) {
				SetNativeControl(new ListBox());
			} else {
				Control.SelectionChanged -= Control_SelectionChanged;
			}

			if (e.NewElement is null)
				return;

			Control.SelectionChanged += Control_SelectionChanged;

			UpdateItemsSource();
			UpdateSelectedIndex();
			UpdateFont();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == CustomPicker.ItemsSourceProperty.PropertyName) {
				UpdateItemsSource();
			} else if (e.PropertyName == CustomPicker.SelectedIndexProperty.PropertyName) {
				UpdateSelectedIndex();
			} else if (e.PropertyName == CustomPicker.FontFamilyProperty.PropertyName) {
				UpdateFont();
			} else if (e.PropertyName == CustomPicker.FontSizeProperty.PropertyName) {
				UpdateFont();
			}
		}

		private void UpdateItemsSource() => Control.ItemsSource = Element.ItemsSource;

		private void UpdateSelectedIndex() => Control.SelectedIndex = Element.SelectedIndex;

		/// <summary>
		/// Updates the font family of each element in the picker
		/// </summary>
		private void UpdateFont() {
			var font = string.IsNullOrEmpty(Element.FontFamily) ?
				XF.Font.SystemFontOfSize(Element.FontSize) :
				XF.Font.OfSize(Element.FontFamily, Element.FontSize);

			Control.ApplyFont(font);
			Control.FontSize = Element.FontSize;
		}

		void Control_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Element.SelectedIndex = Element.ItemsSource.Cast<object>().IndexOf(
				item => item.ToString() == e.AddedItems[0].ToString()
			);
		}
	}
}
