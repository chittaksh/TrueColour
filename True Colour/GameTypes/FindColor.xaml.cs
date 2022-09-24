using System;
using System.Windows;
using Microsoft.Phone.Controls;
using TrueColour.Resources;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace TrueColour.GameTypes
{
    public partial class FindColor : PhoneApplicationPage
    {

        #region : Variable :

        TrueColour.Class.Basics ClassBasic = new TrueColour.Class.Basics();
        TrueColour.Class.Record ClassRecord = new TrueColour.Class.Record();
        TrueColour.Class.AdSetting Advertisement = new TrueColour.Class.AdSetting();
        string ColorName;
        int x = 5000;

        #endregion

        #region : Constructors :
        public FindColor()
        {
            try
            {
                InitializeComponent();
                CountDown.Begin();
                ShowAdd();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        #endregion

        #region : Private Events :

        private void CountDown_Completed(object sender, EventArgs e)
        {
            try
            {
                txtCount.Visibility = System.Windows.Visibility.Collapsed;
                ColorRing.Visibility = System.Windows.Visibility.Visible;
                txtTopScore.Visibility = System.Windows.Visibility.Visible;
                gridColors.Visibility = System.Windows.Visibility.Visible;
                txtTopScore.Text = Convert.ToString(ClassRecord.GetFirstRecord("FindColor"));
                imgTopScore.Visibility = System.Windows.Visibility.Visible;
                txtCurrentScore.Visibility = System.Windows.Visibility.Visible;
                ColorRing.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(50)));
            reselectone:
                ColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(50));
                if (ClassBasic.GetColorName((SolidColorBrush)ColorRing.SegmentColor) == ColorName)
                {
                    goto reselectone;
                }
                else
                {
                    ColorRing.Content = AppResources.ResourceManager.GetString(ColorName);
                }
                DoubleAnimationRing.Duration = new Duration(TimeSpan.FromMilliseconds(x));
                StoryboardRing.Begin();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void ColorCircle_Completed(object sender, EventArgs e)
        {
            try
            {
                RoundComplete();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void Ellipse_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Ellipse ellipse = sender as Ellipse;

                if ((ClassBasic.GetColorName((SolidColorBrush)ellipse.Fill)) == (ColorRing.Content))
                {
                    AddScore();
                }
                else
                {
                    RoundComplete();
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        #endregion

        #region : Private Methods :

        private void RoundComplete()
        {
            try
            {
                StoryboardRing.Stop();
                txtCount.Visibility = System.Windows.Visibility.Visible;
                ColorRing.Visibility = System.Windows.Visibility.Collapsed;
                gridColors.Visibility = System.Windows.Visibility.Collapsed;
                txtTopScore.Visibility = System.Windows.Visibility.Collapsed;
                imgTopScore.Visibility = System.Windows.Visibility.Collapsed;
                txtCurrentScore.Visibility = System.Windows.Visibility.Collapsed;
                txtCount.Opacity = 1;
                txtCount.Text = txtCurrentScore.Text;
                ClassRecord.AddNewRecord("FindColor", Convert.ToInt32(txtCurrentScore.Text));
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void AddScore()
        {
            try
            {
                txtCurrentScore.Text = Convert.ToString(Convert.ToInt32(txtCurrentScore.Text) + 1);
                StoryboardRing.Stop();
                ColorRing.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(50)));
            reselectone:
                ColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(50));
                if (ClassBasic.GetColorName((SolidColorBrush)ColorRing.SegmentColor) == ColorName)
                {
                    goto reselectone;
                }
                else
                {
                    ColorRing.Content = AppResources.ResourceManager.GetString(ColorName);
                }
                x = ClassBasic.GetReducedTime(x);
                DoubleAnimationRing.Duration = new Duration(TimeSpan.FromMilliseconds(x));
                StoryboardRing.Begin();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void ShowAdd()
        {
            try
            {
                if (Advertisement.ReviewDone())
                {
                    FindColorAd.Visibility = System.Windows.Visibility.Collapsed;
                    FindColorAd.IsEnabled = false;
                }
                else
                {
                    FindColorAd.Visibility = System.Windows.Visibility.Visible;
                    FindColorAd.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        #endregion

        #region : Error Handling :

        private void ErrorHandling(Exception ex)
        {
            //MessageBoxResult result = MessageBox.Show(AppResources.ErrorMessage, AppResources.ApplicationTitle, MessageBoxButton.OKCancel);
            MessageBoxResult result = MessageBox.Show(ex.Message, AppResources.ApplicationTitle, MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                //Do Something.
                Application.Current.Terminate();
            }
            else
            {
                Application.Current.Terminate();
            }

        }

        #endregion

    }
}