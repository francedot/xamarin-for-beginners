using Prism.Navigation;
using Xamarin.Forms;

namespace MyReddit.Views
{
    public partial class RootMasterDetailPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public RootMasterDetailPage()
        {
            InitializeComponent();
        }

        public  bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }
    }
}
