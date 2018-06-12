using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EmbeddedPicker
{
    public class CustomPicker : View
    {
		// Item Source
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(CustomPicker), null);
		public IEnumerable ItemsSource
		{
			get => (IEnumerable)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		// Selected Index
		public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(CustomPicker), -1, BindingMode.TwoWay, coerceValue: CoerceSelectedIndex);
		public int SelectedIndex
		{
			get => (int)GetValue(SelectedIndexProperty);
			set => SetValue(SelectedIndexProperty, value);
		}
		private static object CoerceSelectedIndex(BindableObject bindable, object value)
		{
			return value ?? 0;
		}

		// Font Size
		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomPicker), -1.0, defaultValueCreator: bindable => Device.GetNamedSize(NamedSize.Default, (CustomPicker)bindable), coerceValue: CoerceFontSize);
		public double FontSize
		{
			get => (double)GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}
		private static object CoerceFontSize(BindableObject bindable, object value)
		{
			return value ?? Device.GetNamedSize(NamedSize.Default, (CustomPicker)bindable);
		}

		// Font Family
		public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(CustomPicker), default(string));
		public string FontFamily
		{
			get => (string)GetValue(FontFamilyProperty);
			set => SetValue(FontFamilyProperty, value);
		}

		// Selector Line Color _Android Only_
		public static readonly BindableProperty SelectorLineColorProperty = BindableProperty.Create(nameof(SelectorLineColor), typeof(Color), typeof(CustomPicker), Color.Gray);
		public Color SelectorLineColor
		{
			get => (Color)GetValue(SelectorLineColorProperty);
			set => SetValue(SelectorLineColorProperty, value);
		}

		// Wrap Wheel _Android Only_
		public static readonly BindableProperty WrapSelectorWheelProperty = BindableProperty.Create(nameof(WrapSelectorWheel), typeof(bool), typeof(CustomPicker), false);
		public bool WrapSelectorWheel
		{
			get => (bool)GetValue(WrapSelectorWheelProperty);
			set => SetValue(WrapSelectorWheelProperty, value);
		}

		// Cell height _iOS Only_
		public static readonly BindableProperty CellHeightProperty = BindableProperty.Create(nameof(CellHeight), typeof(float), typeof(CustomPicker), 40F);
		public float CellHeight
		{
			get => (float)GetValue(CellHeightProperty);
			set => SetValue(CellHeightProperty, value);
		}
	}
}
