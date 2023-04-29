using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Domotica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualPage : ContentPage
    {

        string IPaddress = "";
        Client myclient;
        public VisualPage()
        {
            InitializeComponent();
            
            if (Application.Current.Properties.ContainsKey("IP"))
            {
                IPaddress = Application.Current.Properties["IP"].ToString();
               // obtainInfo();
            }
            else
            {
                IPaddress = "192.168.72.1";
                //DisplayAlert("Conexión fallida","Por favor conéctese a un sistema","ok");
            }
            //init();
            

            // Server s = new Server("localhost", 4044);
            //s.Start();  
        }
        public VisualPage(Client c)
        {
            InitializeComponent();
            myclient = c;

            if (Application.Current.Properties.ContainsKey("IP"))
            {
                IPaddress = Application.Current.Properties["IP"].ToString();
                //init(); // obtainInfo(myclient);
                obtainInfo();
            }
            /*else
            {
                //DisplayAlert("Conexión fallida","Por favor conéctese a un sistema","ok");
            }*/



            // Server s = new Server("localhost", 4044);
            //s.Start();  
        }
        public void init()
        {
            try
            {                
                Client c = new Client(IPaddress, 4044);
                c.Start();
                c.Send("init");
                myclient = c;
                DisplayAlert("Mandado", "holii", "OK");
                obtainInfo();
            }
            catch(Exception ex) {
                DisplayAlert("ERROR", ex.ToString(), "OK");
            }
            
        }
        public void obtainInfo()
        {
            try
            {
                Client c = new Client(IPaddress, 4044);
                c.Start();
                c.Send("init");
                string msg = c.Receive();
                string[] data = msg.Split(new string[] { " " }, StringSplitOptions.None);
                // DisplayAlert("Mensaje",msg, "OK");
                //DisplayAlert("Conexión", "Conexión realizada con éxito", "Ok");
                TempLabel.Text = data[0] + "ºC";
                HumLabel.Text = data[1] + "%";

                
                if (data[2] == "1")
                {
                    Ventilador.IsToggled= true;
                    Ventilador.ThumbColor= Color.Green;
                }
                else
                {
                    Ventilador.IsToggled = false;
                    Ventilador.ThumbColor = Color.Red;
                }


                if (data[3] == "1")
                {
                    LuzSalon.IsToggled = true;
                    LuzSalon.ThumbColor = Color.Green;
                }
                else
                {
                    LuzSalon.IsToggled = false;
                    LuzSalon.ThumbColor = Color.Red;
                }


                if (data[4] == "1")
                {
                    LuzCocina.IsToggled = true;
                    LuzCocina.ThumbColor = Color.Green;
                }
                else
                {
                    LuzCocina.IsToggled = false;
                    LuzCocina.ThumbColor = Color.Red;
                }


                if (data[5] == "1")
                {
                    LuzCuarto.IsToggled = true;
                    LuzCuarto.ThumbColor = Color.Green;
                }
                else
                {
                    LuzCuarto.IsToggled = false;
                    LuzCuarto.ThumbColor = Color.Red;
                }

                if (data[6] == "1")
                {
                    LuzGaraje.IsToggled = true;
                    LuzGaraje.ThumbColor = Color.Green;
                }
                else
                {
                    LuzGaraje.IsToggled = false;
                    LuzGaraje.ThumbColor = Color.Red;
                }

                if (data[7] == "1")
                {
                    PuertaGaraje.IsToggled = true;
                    PuertaGaraje.ThumbColor = Color.Green;
                }
                else
                {
                    PuertaGaraje.IsToggled = false;
                    PuertaGaraje.ThumbColor = Color.Red;
                }

            }
            catch (Exception Exc)
            {
                DisplayAlert("Conexión fallida", "No se ha podido realizar conexión, compruebe la dirección IP", "Ok");
            }

            //Thread.Sleep(5000);
            //obtainInfo();
        }

        void BtnClicked(System.Object sender, System.EventArgs e)  //conectarse a la IP escrita
        {
            if (Application.Current.Properties.ContainsKey("IP"))
            {
                //IPaddress = Application.Current.Properties["IP"].ToString();
                 obtainInfo();
            }
            else
            {
                //obtainInfo(myclient);
                DisplayAlert("Conexión fallida", "Por favor conéctese a un sistema" + IPaddress, "ok");
            }
           // obtainInfo();
        }


        /* //SOCKETS FUNCIONAL
            try
            {
                Client c = new Client(IPaddress, 4044);
                c.Start();
                c.Send("Holitaa");
                string msg= c.Receive();
                string[] data= msg.Split(new string[] { " " }, StringSplitOptions.None);
                // DisplayAlert("Mensaje",msg, "OK");
                //DisplayAlert("Conexión", "Conexión realizada con éxito", "Ok");
                TemperatureGrid.Text = data[0] + "ºC";
                HumidityGrid.Text = data[1] + "%";
            }
            catch (Exception Exc)
            {
                DisplayAlert("Conexión fallida", "No se ha podido realizar conexión, compruebe la dirección IP", "Ok");
            }*/
    }
}