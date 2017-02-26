using Foundation;
using System;
using UIKit;

namespace StoryboardAndNavigation
{
    public partial class SecondViewController : UIViewController
    {
		public string Parameter
		{
			get;
			set;
		}

        public SecondViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidAppear(bool animated)
		{
			parameterField.Text = Parameter;
		}

		partial void CloseButton_TouchUpInside(UIButton sender)
		{
			this.DismissViewController(true, null);
		}
	}
}