using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using EmbeddedPicker;
using EmbeddedPicker.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EmbeddedPicker.iOS
{
	public class CustomPickerRenderer : ViewRenderer<CustomPicker, UIPickerView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<CustomPicker> e)
		{
			base.OnElementChanged(e);
			
			if (Control == null)
			{
				SetNativeControl(new UIPickerView(RectangleF.Empty));
			}

			if (e.NewElement == null) return;

			UpdateItemsSource();
			UpdateSelectedIndex();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == CustomPicker.ItemsSourceProperty.PropertyName)
			{
				UpdateItemsSource();
			}
			else if (e.PropertyName == CustomPicker.SelectedIndexProperty.PropertyName)
			{
				UpdateSelectedIndex();
			}
			else if (e.PropertyName == CustomPicker.FontSizeProperty.PropertyName)
			{
				UpdateItemsSource();
			}
			else if (e.PropertyName == CustomPicker.FontFamilyProperty.PropertyName)
			{
				UpdateItemsSource();
			}
		}

		private void UpdateItemsSource()
		{
			var font = string.IsNullOrEmpty(Element.FontFamily) ?
				Font.SystemFontOfSize(Element.FontSize) :
				Font.OfSize(Element.FontFamily, Element.FontSize);
			var nativeFont = font.ToUIFont();
			Control.Model = new DataModel(this.Element.ItemsSource, row =>
			{
				Element.SelectedIndex = row;
			}, nativeFont, Element.CellHeight);
		}

		private void UpdateSelectedIndex()
		{
			if (Control.Model == null)
			{
				return;
			}

			if (Element.SelectedIndex < 0 || Element.SelectedIndex >= Control.Model.GetRowsInComponent(Control, 0))
			{
				return;
			}

			Control.Select(Element.SelectedIndex, 0, true);
		}
	}

	internal class DataModel : UIPickerViewModel
	{
		private readonly IList<string> _list = new List<string>();
		private readonly Action<int> _selectedHandler;
		private readonly UIFont _nativeFont;
		private readonly nfloat _cellHeight;

		public DataModel(IEnumerable items, Action<int> selectedHandler, UIFont nativeFont, nfloat cellHeight)
		{
			_selectedHandler = selectedHandler;
			_nativeFont = nativeFont;
			_cellHeight = cellHeight;

			if (items != null)
			{
				foreach (var item in items)
				{
					_list.Add(item.ToString());
				}
			}
		}

		public override System.nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override System.nint GetRowsInComponent(UIPickerView pickerView, System.nint component)
		{
			return _list.Count;
		}

		// Update the height of each individual cell
		public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
		{
			return _cellHeight;
		}

		public override string GetTitle(UIPickerView pickerView, System.nint row, System.nint component)
		{
			return _list[(int)row];
		}

		public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
		{
			var label = new UILabel(pickerView.Bounds)
			{
				Font = _nativeFont,
				Text = _list[(int) row],
				TextAlignment = UITextAlignment.Center
			};
			return label;
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			_selectedHandler?.Invoke((int)row);
		}
	}
}