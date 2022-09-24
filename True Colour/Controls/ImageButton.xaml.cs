using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TrueColour.Controls
{
    public partial class ImageButton : UserControl
    {
        #region : Constructor :
        
        public ImageButton()
        {
            InitializeComponent();
        }

        #endregion 

        #region : Dependency Property :

        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri", typeof(string), typeof(ImageButton), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPropertyChanged)));

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(ImageButton), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), new PropertyChangedCallback(OnPropertyChanged)));

        #endregion

        #region : Properties :

        public string ImageUri
        {
            get { return (string)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        #endregion

        #region : Private Events :

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                ImageButton ImageButton = sender as ImageButton;
                BitmapImage bitmapImage = new BitmapImage();
                if (ImageButton.ImageUri != string.Empty)
                {
                    Uri ImageUri = new Uri(ImageButton.ImageUri, UriKind.Relative);
                    bitmapImage.UriSource = ImageUri;
                    ImageButton.ImgImage.Source = bitmapImage;
                }
                ImageButton.EllBackground.Fill = ImageButton.BackgroundColor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Size changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                this.Height = this.Width;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
