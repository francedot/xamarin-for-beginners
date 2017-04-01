using System;
using Xamarin.Forms;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        private readonly Entry _billEntry;
        private readonly Label _tipLabel;
        private readonly Label _totalLabel;

        public MainPage()
        {
            //InitializeComponent();

            var rootLayout = new StackLayout();

            _billEntry = new Entry()
            {
                Placeholder = "Enter Bill",
                PlaceholderColor = Color.Accent,
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center
            };
            var calculateButton = new Button()
            {
                Text = "CALCULATE",
                HorizontalOptions = LayoutOptions.Fill
            };
            calculateButton.Clicked += CalculateButtonOnClicked;

            rootLayout.Children.Add(_billEntry);
            rootLayout.Children.Add(calculateButton);

            var tipLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
            tipLayout.Children.Add(new Label
            {
                Text = "Tip",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });
            _tipLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            tipLayout.Children.Add(_tipLabel);
            rootLayout.Children.Add(tipLayout);

            var totalLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
            totalLayout.Children.Add(new Label
            {
                Text = "Total",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });
            _totalLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            totalLayout.Children.Add(_totalLabel);
            rootLayout.Children.Add(totalLayout);

            Device.OnPlatform(
                iOS: () =>
                {
                    rootLayout.Spacing = 4.0;
                    rootLayout.Padding = new Thickness(4, 28, 4, 4);
                },
                Android: () =>
                {
                    rootLayout.Spacing = 4.0;
                    rootLayout.Padding = new Thickness(2);
                },
                WinPhone: () => // UWP
                {
                    rootLayout.Spacing = 8.0;
                    rootLayout.Padding = new Thickness(4);
                    calculateButton.BackgroundColor = Color.Silver;
                });

            Content = rootLayout;
        }

        private void CalculateButtonOnClicked(object sender, EventArgs eventArgs)
        {
            string text = _billEntry.Text;

            double bill;
            if (double.TryParse(text, out bill))
            {
                var tip = bill * 0.15;
                var total = bill + tip;

                _tipLabel.Text = tip.ToString("C");
                _totalLabel.Text = total.ToString("C");
            }
        }
    }
}
