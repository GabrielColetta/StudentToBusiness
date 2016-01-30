using S2B.SQLite;
using S2B.ViewModels;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace S2B.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }
        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;

        private void CarregarPagina(object sender, RoutedEventArgs e)
        {
            using(var db = new Contexto())
            {
                ArmazenamentoRecente.DataContext = db.Armazenamentos.LastOrDefault();
            }
        }
    }
}
