using System;

using UIKit;

namespace StoryboardAndNavigation
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void SwitchNight_ValueChanged(UISwitch sender)
		{
			if (sender.On)
				this.View.BackgroundColor = UIColor.FromRGB(25, 25, 112);
			else
				this.View.BackgroundColor = UIColor.White;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
