using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Domotica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnectPage : ContentPage
	{
        
        string IPaddress = "";
		public ConnectPage()
		{
			InitializeComponent();
            if (Application.Current.Properties.ContainsKey("IP"))
            {
                IPaddress = Application.Current.Properties["IP"].ToString();
            }


            // Server s = new Server("localhost", 4044);
            //s.Start();  
        }

        async void ShowVerifyConnectionBtn()
        {
            VerifyConnectionBtn.Opacity = 0;
            VerifyConnectionBtn.Scale = 0.7;
            VerifyConnectionBtn.IsVisible = true;
            VerifyConnectionBtn.FadeTo(1, 170);
            await VerifyConnectionBtn.ScaleTo(1, 170);
        }

        async void HideVerifyConnectionBtn()
        {
            VerifyConnectionBtn.Opacity = 1;
            VerifyConnectionBtn.Scale = 1;
            VerifyConnectionBtn.FadeTo(0, 170);
            await VerifyConnectionBtn.ScaleTo(0.7, 170);
            VerifyConnectionBtn.IsVisible = false;
        }



        void IPTB_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string Text = e.NewTextValue;
            string oldText = (e.OldTextValue != null) ? e.OldTextValue : "";
            bool RevertChanges = true;
            if (Text.Length <= 16)
            {

                string newChar = (oldText != "") ? e.NewTextValue.Replace(oldText, "") : e.NewTextValue;
                var isNumeric = long.TryParse(newChar.Replace(".", ""), out long number); // pasa la IP a numeros sin puntos
                if (isNumeric || newChar == "." || newChar == "")
                {
                    RevertChanges = false;

                    string[] Blocks = Text.Split('.');

                    if (Blocks.Length == 4 && Blocks[0] != "" && Blocks[1] != "" && Blocks[2] != "" && Blocks[3] != "")
                    {
                        if (!VerifyConnectionBtn.IsVisible)
                        {
                            ShowVerifyConnectionBtn();
                            //ShowSaveIp();
                        }

                    }
                    else
                    {
                        if (VerifyConnectionBtn.Opacity == 1)
                        {
                            HideVerifyConnectionBtn();
                            //HideSaveIp();
                        }
                    }
                }

            }

            if (RevertChanges)
            {
                Entry Sender = sender as Entry;
                if (Sender.Text != "") //Evita caer en un bucle infinito
                {
                    Sender.Text = oldText;
                }
            }
        }
        /*
        void SaveIpBtn_Clicked(System.Object sender, System.EventArgs e)  //guardarlo con nombre
        {

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string nombre_archivo = "/IPs.txt";
            string ruta_completa = path + nombre_archivo;

            if (File.Exists(ruta_completa))
            {
                using (StreamWriter escritor = new StreamWriter(ruta_completa, true))
                {
                    string nombre = Save_IP.Text;
                    //rosIPAddress = IPTB.Text;
                    string IP_value = rosIPAddress;
                    //DisplayAlert("IP", IP_value, "GUARDADA");
                    string nombre_IP_value = nombre + " " + IP_value;
                    //  DisplayAlert("Guardado", nombre_IP_value, "OK");
                    escritor.WriteLine(nombre_IP_value);
                    Task.Run(() => PickerSetup(600));// para IP_Page
                    HideSaveIpBtn();
                }

            }
            else
            {

                DisplayAlert(ruta_completa, "Fichero de texto de IPs no encontrado", "OK");

            }
        }*/

        async void VerifyConnectionBtn_Clicked(System.Object sender, System.EventArgs e)  //conectarse a la IP escrita
        {
            try {
                Application.Current.Properties["IP"] = IPTB.Text;
                IPaddress = Application.Current.Properties["IP"].ToString();
                
                Client c = new Client(IPaddress, 4044);
                c.Start();
                c.Send("Holitaa");
                c.disconect();
                DisplayAlert("Conexión","Conexión realizada con éxito","Ok");
            }
            catch (Exception Exc)
            {
            }
        }


    }
}