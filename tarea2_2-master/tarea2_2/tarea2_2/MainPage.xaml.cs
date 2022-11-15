using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tarea2_2.models;
using SignaturePad.Forms;
using Xamarin.Essentials;
using System.IO;
using Xamarin.Forms;
using tarea2_2.views;
namespace tarea2_2
{
    public partial class MainPage : ContentPage
    {
        byte[] ImageBytes;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            var ima0 = await Permissions.RequestAsync<Permissions.StorageRead>();
            var ima1 = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (ima0 != PermissionStatus.Granted || ima1 != PermissionStatus.Granted)
            {
                return;
            }
            try
            {
                //obtenemos la firma
                var image = await pad.GetImageStreamAsync(SignatureImageFormat.Png);

                //Guardamos localmente en el dispositivo
                SaveToDevice(image);

                //Pasamos la firma a imagen a base 64
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                string base64Val = Convert.ToBase64String(data);
                ImageBytes = Convert.FromBase64String(base64Val);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No hay ninguna firma", "Ok");
            }
            modelfirma firmamo = new modelfirma();
            firmamo.nombre = txtnombre.Text;
            firmamo.descripcion = txtdescripcion.Text;
            firmamo.firma = ImageBytes;


            if (String.IsNullOrEmpty(txtnombre.Text) || String.IsNullOrEmpty(txtdescripcion.Text))
            {
                await DisplayAlert("Aviso", "Campos vacios", "Ok");

            }
            else
            {
                try
                {

                    await App.Basedb.Guadar(firmamo);
                    await DisplayAlert("Aviso", "Firma guardada", "Ok");
                    txtnombre.Text = "";
                    txtdescripcion.Text = "";
                    pad.Clear();
                }
                catch
                {
                    await DisplayAlert("Advertencia", " Error al guardar firma", "Ok");
                }


            }

        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaF());
        }




        private void SaveToDevice(Stream img)
        {
            try
            {
                var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString(), "Firmas");

                if (!Directory.Exists(filename))
                {
                    Directory.CreateDirectory(filename);
                }

                string nameFile = DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                filename = Path.Combine(filename, nameFile);

                var mStream = (MemoryStream)img;
                Byte[] bytes = mStream.ToArray();
                File.WriteAllBytes(filename, bytes);

                DisplayAlert("Notificación", "Firma guardada: " + filename, "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
        }



    }
}