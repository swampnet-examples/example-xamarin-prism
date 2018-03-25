using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PrismApp.ViewModels
{
    public class SomeItem
    {
		public string Title { get; set; }
		public ICommand	Command { get; set; }
	}
}
