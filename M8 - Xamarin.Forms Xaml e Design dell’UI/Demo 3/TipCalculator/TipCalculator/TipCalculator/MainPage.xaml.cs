using System;
using Xamarin.Forms;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
