using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace Domotica
{

    public class Server{

        IPHostEntry host;
        IPAddress ipAdress;
        IPEndPoint ipEndPoint;

        Socket socket_client;
        Socket socket_server;


        public Server(string ip, int port)
        {

            host = Dns.GetHostByAddress(ip);
            ipAdress = host.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAdress, port);

            // socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket_server = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket_server.Bind(ipEndPoint);
            socket_server.Listen(10);
        }

        public void Start()
        {
            byte[] buffer = new byte[1024];
            string message;

            socket_client = socket_server.Accept();

            socket_client.Receive(buffer);
            message = Encoding.ASCII.GetString(buffer);

            Console.WriteLine("Se recibio el mensaje: " + message);


        }


    }



    public class Client
    {
        IPHostEntry host;
        IPAddress ipAdress;
        IPEndPoint ipEndPoint;

        Socket socket_client;

        public Client(string ip, int port)
        {

            host = Dns.GetHostByName(ip);
            ipAdress = host.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAdress, port);

            // socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket_client = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        }

        public void Start()
        {
            socket_client.Connect(ipEndPoint);
        }

        public void Send(string msg)
        {
            byte[] byteMsg = Encoding.ASCII.GetBytes(msg);

            socket_client.Send(byteMsg);
            //Console.WriteLine("Mensaje enviado " + msg);
        }

        public string  Receive()
        {
            byte[] byteMsg = new byte[1024];
            socket_client.Receive(byteMsg);
            string msg = Encoding.ASCII.GetString(byteMsg);

            //Console.WriteLine("Mensaje enviado " + msg);
            return msg;
            
        }
         public void disconect()
        {
            SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
            socket_client.DisconnectAsync(socketAsyncEventArgs);

        }
    }
class mySocketConn
    {      


            
        IPHostEntry host;
        IPAddress ipAdress;
        IPEndPoint ipEndPoint;

        Socket socket_client;
        Socket socket_server;


        Socket socket_client_2;
        public mySocketConn(string ip, int port)
        {
            host = Dns.GetHostByName(ip);
            ipAdress = host.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAdress, port);

            Socket socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


        }

        async public void Server(string ip, int port)
        {

            host = Dns.GetHostByName(ip);
            ipAdress = host.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAdress, port);

            // socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket_server = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket_server.Bind(ipEndPoint);
            socket_server.Listen(10);
        }

        public void StartServer()
        {
            byte[] buffer = new byte[1024];
            string message;

            socket_client = socket_server.Accept();

            socket_client.Receive(buffer);
            message = Encoding.ASCII.GetString(buffer);

            Console.WriteLine("Se recibio el mensaje: " + message);


        }

        async public void Client(string ip, int port)
        {

            host = Dns.GetHostByName(ip);
            ipAdress = host.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAdress, port);

            // socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket_client_2 = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        }

        public void StartClient()
        {
            socket_client_2.Connect(ipEndPoint);
        }

        public void Send(string msg)
        {
            byte[] byteMsg = Encoding.ASCII.GetBytes(msg);

            socket_client_2.Send(byteMsg);
            Console.WriteLine("Mensaje enviado " + msg);
        }
        
    }
}
