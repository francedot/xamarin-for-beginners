using System;
using CoreGraphics;
using UIKit;

namespace TipCalculator
{
	public partial class ViewController : UIViewController
	{
		public ViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			UITextField totalAmount = new UITextField(new CGRect(20, 28, View.Bounds.Width - 40, 35))
			{
				KeyboardType = UIKeyboardType.DecimalPad,
				BorderStyle = UITextBorderStyle.RoundedRect,
				Placeholder = "Enter Total Amount"
			};

			UIButton calcButton = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(20, 71, View.Bounds.Width - 40, 45),
				BackgroundColor = UIColor.FromRGB(0.2f, 0.5f, 0.4f),
			};
			calcButton.SetTitle("Calculate", UIControlState.Normal);

			UILabel resultLabel = new UILabel(new CGRect(20, 124, View.Bounds.Width - 40, 40))
			{
				TextAlignment = UITextAlignment.Center,
				Font = UIFont.SystemFontOfSize(24),
				Text = "Tip is $0.00"
			};

			View.AddSubviews(totalAmount, calcButton, resultLabel);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
