using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App5.Class;
using System.IO;

namespace App5
{
    public partial class MainPage : ContentPage
    {
        static Stream streamCopy;
        
        public MainPage()
        {
            InitializeComponent();
        }
        async void btnPicture_Clicked(object sender, EventArgs e)
        {
            var useCam = ((Button)sender).Text.Contains("Cámara");
            var file = await ServiceImage.TakePicture(useCam);

            Results.Children.Clear();
            lblResult.Text = "---";

            imgPicture.Source = ImageSource.FromStream(() => 
            {
                var stream = file.GetStream();
                var streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                file.Dispose();
                return stream;
            });
        }

        async void btnAnalysisEmotions_Clicked(object sender, EventArgs e)
        {
            
            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);
                var emotions = await ServiceEmotions.GetEmotions(streamCopy);

                if (emotions != null)
                {
                    lblResult.Text = "---Análisis de Emociones---";
                    DrawResults(emotions);
                }
                else lblResult.Text = "---No se detectó una cara---";
            }
            else lblResult.Text = "---No has seleccionado una imagen---";
            
        }
        
        void DrawResults(Dictionary<string, float> emotions)
        {
            Results.Children.Clear();

            foreach (var emotion in emotions)
            {
                Label lblEmotion = new Label()
                {
                    Text = emotion.Key,
                    TextColor = Color.Blue,
                    WidthRequest = 90
                };

                BoxView box = new BoxView()
                {
                    Color = Color.Lime,
                    WidthRequest = 150 * emotion.Value,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblPercent = new Label()
                {
                    Text = emotion.Value.ToString("P4"),
                    TextColor = Color.Maroon
                };

                StackLayout stack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };

                stack.Children.Add(lblEmotion);
                stack.Children.Add(box);
                stack.Children.Add(lblPercent);

                Results.Children.Add(stack);
            }
        }
    }
}