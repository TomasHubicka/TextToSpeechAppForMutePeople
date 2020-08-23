using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DialogApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private bool _canRead = true;
        public bool CanRead
        {
            get
            {
                return _canRead;
            }
            set
            {
                _canRead = value; NotifyPropertyChanged();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            read.Clicked += Read_Clicked;
            cancel.Clicked += Cancel_Clicked;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            if (cts?.IsCancellationRequested ?? true)
                return;
            CanRead = true;
            cts.Cancel();
        }

        CancellationTokenSource cts;
        private async void Read_Clicked(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            if (sayText.Text == null)
            {
                await DisplayAlert($"Hej!", "Zapomněl jsi text!", "OK");
            }
            else
            {
                
                var settings = new SpeechOptions()
                {
                    Volume = Convert.ToSingle(volume.Value),
                    Pitch = Convert.ToSingle(pitch.Value)
                };
                CanRead = false;
                await TextToSpeech.SpeakAsync(sayText.Text, settings, cancelToken: cts.Token);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
