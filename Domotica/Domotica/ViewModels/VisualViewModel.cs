using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Domotica.ViewModels
{
    public class VisualViewModel : BaseViewModel
    {
        public VisualViewModel()
        {
            Title = "Visualización";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}