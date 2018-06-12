using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EmbeddedPicker
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public List<string> PickerItemSource { get; set; }
		public int PickerIndex { get; set; }

		public string PickerItem => PickerItemSource[PickerIndex];

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
		}
	}
}
