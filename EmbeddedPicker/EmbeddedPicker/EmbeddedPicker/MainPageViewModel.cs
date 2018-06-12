using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EmbeddedPicker
{
	public class MainPageViewModel
	{
		public List<string> PickerItemSource { get; set; }
		public int PickerIndex { get; set; }

		public string PickerItem { get; set; }

		public MainPageViewModel()
		{
			PickerItemSource = new List<string>
			{
				"1",
				"2",
				"3",
				"4",
				"5",
			};

			PickerItem = PickerItemSource[PickerIndex];
		}
	}
}
