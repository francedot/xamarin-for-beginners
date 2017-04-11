using System;
using Xamarin.Forms;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        private bool _blinkOn;

        public MainPage()
        {
            InitializeComponent();

            calculateButton.SetDynamicResource(Button.TextColorProperty, "BlinkTextColor");
            calculateButton.SetDynamicResource(Button.BackgroundColorProperty, "BlinkBackgroundColor");
            calculateButton.Resources = new ResourceDictionary();
            calculateButton.Resources.Add("BlinkTextColor", Color.White);
            calculateButton.Resources.Add("BlinkBackgroundColor", Color.Black);

            Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
            {
                calculateButton.Resources["BlinkTextColor"] = _blinkOn ? Color.Black : Color.White;
                calculateButton.Resources["BlinkBackgroundColor"] = _blinkOn ? Color.White : Color.Black;
               
                _blinkOn = !_blinkOn;
                return true;
            });
        }

        private void CalculateButtonOnClicked(object sender, EventArgs eventArgs)
        {
            string text = billEntry.Text;

            double bill;
            if (double.TryParse(text, out bill))
            {
                var tip = bill * 0.15;
                var total = bill + tip;

                tipLabel.Text = tip.ToString("C");
                totalLabel.Text = total.ToString("C");
            }
        }
    }
}
