using static System.Net.Mime.MediaTypeNames;

namespace PocSharedFile
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
