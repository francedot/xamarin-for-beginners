using System.ComponentModel;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using Renderer.Controls;
using Renderer.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(FloatingLabelEntry), typeof(FloatingLabelEntryRenderer))]
namespace Renderer.Droid.Renderers
{
    public class FloatingLabelEntryRenderer : ViewRenderer<FloatingLabelEntry, TextInputLayout>, ITextWatcher
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FloatingLabelEntry> e)
        {
            base.OnElementChanged(e);

            var textInputLayout = new TextInputLayout(Context);
            var editText = new EditText(Context);
            textInputLayout.AddView(editText);

            textInputLayout.Focusable = true;
            textInputLayout.HintEnabled = true;
            textInputLayout.HintAnimationEnabled = true;
            textInputLayout.EditText.ShowSoftInputOnFocus = true;

            textInputLayout.EditText.AddTextChangedListener(this);
            textInputLayout.EditText.ImeOptions = ImeAction.Done;

            textInputLayout.EditText.Text = Element.Text;
            textInputLayout.EditText.InputType = Element.Keyboard.ToInputType();
            textInputLayout.Hint = Element.Placeholder;

            textInputLayout.EditText.SetTextColor(Element.TextColor == Color.Default
                ? Color.Black.ToAndroid()
                : Element.TextColor.ToAndroid());

            textInputLayout.EditText.SetHintTextColor(Element.PlaceholderColor == Color.Default
                ? Color.Black.ToAndroid()
                : Element.PlaceholderColor.ToAndroid());

            textInputLayout.HintEnabled = true;

            SetNativeControl(textInputLayout);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
            {
                return;
            }

            var formsEntry = (Entry) sender;

            // Wire up needed property change notifications
            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                Control.EditText.Text = formsEntry.Text;
            }
            else if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                Control.Hint = formsEntry.Placeholder;
            }
            else if (e.PropertyName == InputView.KeyboardProperty.PropertyName)
            {
                Control.EditText.InputType = Element.Keyboard.ToInputType();
            }
        }

        #region ITextWatcher

        public virtual void AfterTextChanged(IEditable s)
        {
        }

        public virtual void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
        }

        public virtual void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            Element.SetValue(Entry.TextProperty, s.ToString());
        }

        #endregion
    }
}