using Domotica.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Domotica.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}