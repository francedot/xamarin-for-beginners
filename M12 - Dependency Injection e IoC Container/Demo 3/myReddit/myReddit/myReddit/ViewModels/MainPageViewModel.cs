using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyReddit.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {

        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}
