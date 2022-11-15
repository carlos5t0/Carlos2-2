using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tarea2_2.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaF : ContentPage
    {
        public ListaF()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listaF.ItemsSource = await App.Basedb.listafirmas();
        }
    }
}