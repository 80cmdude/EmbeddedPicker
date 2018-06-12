using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using EmbeddedPicker;
using EmbeddedPicker.Droid;
using Java.Lang.Reflect;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace EmbeddedPicker.Droid
{
	public class CustomPickerRenderer : ViewRenderer<CustomPicker, NumberPicker>
	{
		public CustomPickerRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<CustomPicker> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				SetNativeControl(new NumberPicker(Context));
			}
			else
			{
				Control.ValueChanged -= Control_ValueChanged;
			}

			if (e.NewElement != null)
			{
				Control.ValueChanged += Control_ValueChanged;

				// This property controls if the picker wraps around in a circle inifintiely
				Control.WrapSelectorWheel = false;

				UpdateItemsSource();
				UpdateSelectedIndex();
				UpdateFont();
				SetDividerColor(Control, Element.SelectorLineColor.ToAndroid());
			}
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
			else if (e.PropertyName == CustomPicker.FontFamilyProperty.PropertyName)
			{
				UpdateFont();
			}
			else if (e.PropertyName == CustomPicker.FontSizeProperty.PropertyName)
			{
				UpdateFont();
			}
		}

		private void UpdateItemsSource()
		{
			var arr = new List<string>();
			if (Element.ItemsSource != null)
			{
				foreach (var item in Element.ItemsSource)
				{
					arr.Add(item.ToString());
				}

			}

			if (arr.Count > 0)
			{
				int newMax = arr.Count - 1;
				if (newMax < Control.Value)
				{
					Element.SelectedIndex = newMax;
				}

				var extend = Control.MaxValue <= newMax;

				if (extend)
				{
					Control.SetDisplayedValues(arr.ToArray());
				}

				Control.MaxValue = newMax;
				Control.MinValue = 0;

				if (!extend)
				{
					Control.SetDisplayedValues(arr.ToArray());
				}
			}
		}

		private void UpdateSelectedIndex()
		{
			if (Element.SelectedIndex < Control.MinValue || Element.SelectedIndex >= Control.MaxValue)
			{
				return;
			}

			Control.Value = Element.SelectedIndex;
		}

		/// <summary>
		/// Updates the font family of each element in the picker
		/// </summary>
		void UpdateFont()
		{
			var font = string.IsNullOrEmpty(Element.FontFamily) ?
				Font.SystemFontOfSize(Element.FontSize) :
				Font.OfSize(Element.FontFamily, Element.FontSize);

			SetTextSize(Control, font.ToTypeface(), (float)(Element.FontSize * Context.Resources.DisplayMetrics.Density));
		}

		void Control_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
		{
			Element.SelectedIndex = e.NewVal;
		}

		/// <summary>
		/// Sets the text decoration of the individual elements of the picker
		/// </summary>
		private static void SetTextSize(NumberPicker numberPicker, Typeface fontFamily, float textSizeInSp)
		{
			int count = numberPicker.ChildCount;
			for (int i = 0; i < count; i++)
			{
				var child = numberPicker.GetChildAt(i);
				var editText = child as EditText;

				if (editText != null)
				{
					try
					{
						Field selectorWheelPaintField = numberPicker.Class
							.GetDeclaredField("mSelectorWheelPaint");
						selectorWheelPaintField.Accessible = true;
						((Paint)selectorWheelPaintField.Get(numberPicker)).TextSize = textSizeInSp;
						editText.Typeface = fontFamily;
						editText.SetTextSize(ComplexUnitType.Px, textSizeInSp);

						// Need this property set to false or you can edit the elements in the picker :potatoe:
						editText.Focusable = false;

						numberPicker.Invalidate();
					}
					catch (Exception e)
					{
						System.Diagnostics.Debug.WriteLine("SetNumberPickerTextColor failed.", e);
					}
				}
			}
		}

		/// <summary>
		/// Sets the colour of the divider lines in the center of the picker
		/// </summary>
		private void SetDividerColor(NumberPicker picker, Android.Graphics.Color color)
		{
			try
			{
				var numberPickerType = Java.Lang.Class.FromType(typeof(NumberPicker));
				var divider = numberPickerType.GetDeclaredField("mSelectionDivider");
				divider.Accessible = true;

				var dividerDraw = new ColorDrawable(color);
				divider.Set(picker, dividerDraw);
			}
			catch
			{
				// ignored
			}
		}
	}
}