using System;
using Foundation;
using UIKit;

namespace StoryboardAndNavigation
{
	public partial class ViewController : UIViewController, IUITextFieldDelegate
	{
		public string Parameter
		{
			get;
			set;
		}

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		[Export("textFieldShouldReturn:")]
		public bool ShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			parameterField.Delegate = this;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void ParameterField_TextChanged(UITextField sender)
		{
			Parameter = sender.Text;
		}

		partial void NightSwitch_ValueChanged(UISwitch sender)
		{
			if (sender.On)
				this.View.BackgroundColor = UIColor.FromRGB(25, 25, 112);
			else
				this.View.BackgroundColor = UIColor.White;
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			var targetcontroller = segue.DestinationViewController as SecondViewController;
			targetcontroller.Parameter = Parameter;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
