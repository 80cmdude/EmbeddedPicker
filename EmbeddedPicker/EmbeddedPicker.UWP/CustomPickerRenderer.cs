using EmbeddedPicker;
using EmbeddedPicker.UWP;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EmbeddedPicker.UWP {
	public class CustomPickerRenderer : ViewRenderer<CustomPicker, ComboBox> {
		protected override void OnElementChanged(ElementChangedEventArgs<CustomPicker> e) {
			base.OnElementChanged(e);

			if (Control is null) {
				SetNativeControl(new ComboBox());
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

		private void UpdateItemsSource() =>
			Control.ItemsSource = Element.ItemsSource;

		private void UpdateSelectedIndex() =>
			Control.SelectedIndex = Element.SelectedIndex;

		/// <summary>
		/// Updates the font family of each element in the picker
		/// </summary>
		private void UpdateFont() {
			//var font = string.IsNullOrEmpty(Element.FontFamily) ?
			//	Font.SystemFontOfSize(Element.FontSize) :
			//	Font.OfSize(Element.FontFamily, Element.FontSize);

			//SetTextSize(Control, font.ToTypeface(), (float)(Element.FontSize * Context.Resources.DisplayMetrics.Density));
		}

		void Control_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Element.SelectedIndex = Element.ItemsSource.Cast<object>().IndexOf(
				item => item.ToString() == e.AddedItems[0].ToString()
			);
		}

		/// <summary>
		/// Sets the text decoration of the individual elements of the picker
		/// </summary>
		//private static void SetTextSize(ComboBox comboBox, Typeface fontFamily, float textSizeInSp) {
		//	var count = comboBox.ChildCount;
		//	for (var i = 0; i < count; i++) {
		//		var child = comboBox.GetChildAt(i);

		//		if (child is EditText editText) {
		//			try {
		//				var selectorWheelPaintField = comboBox.Class
		//					.GetDeclaredField("mSelectorWheelPaint");
		//				selectorWheelPaintField.Accessible = true;
		//				((Paint)selectorWheelPaintField.Get(comboBox)).TextSize = textSizeInSp;
		//				editText.Typeface = fontFamily;
		//				editText.SetTextSize(ComplexUnitType.Px, textSizeInSp);

		//				// Need this property set to false or you can edit the elements in the picker
		//				editText.Focusable = false;

		//				comboBox.Invalidate();
		//			} catch (Exception e) {
		//				System.Diagnostics.Debug.WriteLine("SetNumberPickerTextColor failed.", e);
		//			}
		//		}
		//	}
		//}
	}
}
