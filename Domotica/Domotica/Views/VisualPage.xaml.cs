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

using System.Net;
using System.Net.Sockets;


namespace Domotica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualPage : ContentPage
    {

        string IPaddress = "";
        //Client myclient;

        //SOCKET INFO

        IPHostEntry host;
        IPAddress ipAdress;
        IPEndPoint ipEndPoint;

        Socket socket_client;
        int port = 4044; //4044
        public VisualPage()
        {
            InitializeComponent();
            
            if (Application.Current.Properties.ContainsKey("IP"))
            {
                IPaddress = Application.Current.Properties["IP"].ToString();
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


        }

        public void obtainInfo2(string msg)
        {
            //TempLabel.Text = msg + "ºC";
            //HumLabel.Text = msg + "%";
            string[] data = msg.Split(new string[] { " " }, StringSplitOptions.None);
            TempLabel.Text = data[0] + "ºC";
            HumLabel.Text = data[1] + "%";


            if (data[2] == "1")
            {
                Ventilador.IsToggled = true;
                Ventilador.ThumbColor = Color.Green;
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

            //Thread.Sleep(1000);
           /* Task.Delay(10000).Wait();
            string newData = getData();
            obtainInfo2(newData);*/
        }

        void BtnClicked(System.Object sender, System.EventArgs e)  //conectarse a la IP escrita
        {
            //python

            /* host = "localhost" # "192.168.1.61" # sys.argv[1]
            port =  4044 #int(sys.argv[2])
            
            mySocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            mySocket.connect((host,port))
            message1 = input("First num: ")
            mySocket.send(message1.encode())
            #message2 = input("Second num: ")
            #mySocket.send(message2.encode())
            
            data = mySocket.recv(1024).decode()
            print ('Received from server: ' + data)ç

            */
            if (Application.Current.Properties.ContainsKey("IP"))
            {

                host = Dns.GetHostByName(IPaddress);
                ipAdress = host.AddressList[0];
                ipEndPoint = new IPEndPoint(ipAdress, port);

                // socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket_client = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket_client.Connect(ipEndPoint);


                byte[] byteMsgSend = Encoding.ASCII.GetBytes("holiBoton");

                socket_client.Send(byteMsgSend);


                byte[] byteMsgRec = new byte[1024];
                socket_client.Receive(byteMsgRec);
                string msg = Encoding.ASCII.GetString(byteMsgRec);

                obtainInfo2(msg);
            }
            else
            {
                
                DisplayAlert("Conexión fallida", "Por favor conéctese a un sistema" + IPaddress, "ok");
            }
        }

        string getData()  
        {
            try
            {
                host = Dns.GetHostByName(IPaddress);
                ipAdress = host.AddressList[0];
                ipEndPoint = new IPEndPoint(ipAdress, port);

                // socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket_client = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket_client.Connect(ipEndPoint);


                byte[] byteMsgSend = Encoding.ASCII.GetBytes("holiBoton");

                socket_client.Send(byteMsgSend);


                byte[] byteMsgRec = new byte[1024];
                socket_client.Receive(byteMsgRec);
                string msg = Encoding.ASCII.GetString(byteMsgRec);

                return msg;
            }
            catch
            {
                return null;
            }
           
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