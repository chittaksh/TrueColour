using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Media;
using TrueColour.Resources;
using System.IO.IsolatedStorage;

namespace TrueColour.GameTypes
{
    public partial class Classic : PhoneApplicationPage
    {
        #region : Variable :

        TrueColour.Class.Basics ClassBasic = new TrueColour.Class.Basics();
        TrueColour.Class.Record ClassRecord = new TrueColour.Class.Record();
        TrueColour.Class.AdSetting Advertisement = new TrueColour.Class.AdSetting();
        int x = 5000;
        string ColorName;

        #endregion

        #region : Constructor :

        public Classic()
        {
            try
            {
                InitializeComponent();
                ShowAdd();
                CountDown.Begin();
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
                IBRight.Visibility = System.Windows.Visibility.Visible;
                IBWrong.Visibility = System.Windows.Visibility.Visible;
                txtTopScore.Visibility = System.Windows.Visibility.Visible;
                imgTopScore.Visibility = System.Windows.Visibility.Visible;
                txtCurrentScore.Visibility = System.Windows.Visibility.Visible;
                ColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                ColorRing.Content = AppResources.ResourceManager.GetString(ColorName);
                ColorRing.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(txtCurrentScore.Text.Trim())));
                txtTopScore.Text = Convert.ToString(ClassRecord.GetFirstRecord("Classic"));
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

        private void IBWrong_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                SolidColorBrush Ring = ColorRing.SegmentColor as SolidColorBrush;

                if (ClassBasic.GetColor(ColorName) != Ring.Color)
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

        private void IBRight_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                SolidColorBrush Ring = ColorRing.SegmentColor as SolidColorBrush;

                if (ClassBasic.GetColor(ColorName) == Ring.Color)
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
                IBRight.Visibility = System.Windows.Visibility.Collapsed;
                IBWrong.Visibility = System.Windows.Visibility.Collapsed;
                txtTopScore.Visibility = System.Windows.Visibility.Collapsed;
                imgTopScore.Visibility = System.Windows.Visibility.Collapsed;
                txtCurrentScore.Visibility = System.Windows.Visibility.Collapsed;
                txtCount.Opacity = 1;
                txtCount.Text = txtCurrentScore.Text;
                ClassRecord.AddNewRecord("Classic", Convert.ToInt32(txtCurrentScore.Text));
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

                switch (ClassBasic.GetRoundCase())
                {
                    case "Right":
                        ColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                        ColorRing.Content = AppResources.ResourceManager.GetString(ColorName);
                        ColorRing.SegmentColor = new SolidColorBrush(ClassBasic.GetColor(ColorName));
                        break;

                    case "Wrong":
                        ColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                        ColorRing.Content = AppResources.ResourceManager.GetString(ColorName);
                        ColorRing.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(txtCurrentScore.Text.Trim())));
                        break;
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
                    ClassicPageAd.Visibility = System.Windows.Visibility.Collapsed;
                    ClassicPageAd.IsEnabled = false;
                }
                else
                {
                    ClassicPageAd.Visibility = System.Windows.Visibility.Visible;
                    ClassicPageAd.IsEnabled = true;
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
            MessageBoxResult result = MessageBox.Show(ex.Message,AppResources.ApplicationTitle, MessageBoxButton.OKCancel );

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