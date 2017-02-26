// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace StoryboardAndNavigation
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton navigateButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch nightSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField parameterField { get; set; }

        [Action ("SwitchNight_ValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SwitchNight_ValueChanged (UIKit.UISwitch sender);

        void ReleaseDesignerOutlets ()
        {
            if (navigateButton != null) {
                navigateButton.Dispose ();
                navigateButton = null;
            }

            if (nightSwitch != null) {
                nightSwitch.Dispose ();
                nightSwitch = null;
            }

            if (parameterField != null) {
                parameterField.Dispose ();
                parameterField = null;
            }
        }
    }
}