using Xamarin.Forms;

namespace myReddit.Helpers
{
    public sealed class OnPlatform2<T>
    {
        public OnPlatform2()
        {
            Android = default(T);
            iOS = default(T);
            WinPhone = default(T);
            Windows = default(T);
            Other = default(T);
        }

        public static T Android { get; set; }

        public static T iOS { get; set; }

        public static T WinPhone { get; set; }

        /// <summary>
        ///     The value to use for WinRT (Windows Phone 8.1 and Windows 8.1).
        /// </summary>
        public static T Windows { get; set; }

        /// <summary>
        ///     Currently unused.
        /// </summary>
        public static T Other { get; set; }

        public static implicit operator T(OnPlatform2<T> onPlatform)
        {
            switch (Device.OS)
            {
                case TargetPlatform.Android:
                    return Android;

                case TargetPlatform.iOS:
                    return iOS;

                case TargetPlatform.WinPhone:
                    return WinPhone;

                case TargetPlatform.Windows:
                    return Windows;

                default:
                    return Other;
            }
        }
    }
}