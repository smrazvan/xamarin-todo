using routedapp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace routedapp.Views
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