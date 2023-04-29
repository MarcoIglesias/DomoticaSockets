using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Domotica.ViewModels
{
    public class ConnectViewModel : BaseViewModel
    {
        public ConnectViewModel()
        {
            Title = "Conectar";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}