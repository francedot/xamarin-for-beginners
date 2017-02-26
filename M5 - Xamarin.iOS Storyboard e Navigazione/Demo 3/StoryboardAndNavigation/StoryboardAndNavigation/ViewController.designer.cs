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
        UIKit.UITextField parameterField { get; set; }

        [Action ("NightSwitch_ValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void NightSwitch_ValueChanged (UIKit.UISwitch sender);

        [Action ("ParameterField_TextChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ParameterField_TextChanged (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (parameterField != null) {
                parameterField.Dispose ();
                parameterField = null;
            }
        }
    }
}