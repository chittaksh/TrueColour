using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using TrueColour.Resources;
using System.Windows.Media;
using TrueColour.Controls;
using System.IO.IsolatedStorage;

namespace TrueColour.GameTypes
{
    public partial class TapColor : PhoneApplicationPage
    {

        #region : Variable :

        TrueColour.Class.Basics ClassBasic = new TrueColour.Class.Basics();
        TrueColour.Class.Record ClassRecord = new TrueColour.Class.Record();
        TrueColour.Class.AdSetting Advertisement = new TrueColour.Class.AdSetting();
        string ColorName;
        int x = 5000;

        #endregion

        #region : Constructors :
        public TapColor()
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
                ColorRingOne.Visibility = System.Windows.Visibility.Visible;
                ColorRingTwo.Visibility = System.Windows.Visibility.Visible;
                ColorRingThree.Visibility = System.Windows.Visibility.Visible;
                ColorRingFour.Visibility = System.Windows.Visibility.Visible;
                TimeBarBack.Visibility = System.Windows.Visibility.Visible;
                TimeBar.Visibility = System.Windows.Visibility.Visible;
                txtTopScore.Visibility = System.Windows.Visibility.Visible;
                txtTopScore.Text = Convert.ToString(ClassRecord.GetFirstRecord("TapColor"));
                imgTopScore.Visibility = System.Windows.Visibility.Visible;
                txtCurrentScore.Visibility = System.Windows.Visibility.Visible;
                RoundReload();
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

        private void ColorRing_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                TheRing TheRing = sender as TheRing;

                if (ColorName == (ClassBasic.GetColorName((SolidColorBrush)TheRing.SegmentColor)))
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
                ColorRingOne.Visibility = System.Windows.Visibility.Collapsed;
                ColorRingTwo.Visibility = System.Windows.Visibility.Collapsed;
                ColorRingThree.Visibility = System.Windows.Visibility.Collapsed;
                ColorRingFour.Visibility = System.Windows.Visibility.Collapsed;
                TimeBarBack.Visibility = System.Windows.Visibility.Collapsed;
                TimeBar.Visibility = System.Windows.Visibility.Collapsed;
                txtTopScore.Visibility = System.Windows.Visibility.Collapsed;
                imgTopScore.Visibility = System.Windows.Visibility.Collapsed;
                txtCurrentScore.Visibility = System.Windows.Visibility.Collapsed;
                txtCount.Opacity = 1;
                txtCount.Text = txtCurrentScore.Text;
                ClassRecord.AddNewRecord("TapColor", Convert.ToInt32(txtCurrentScore.Text));
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
                RoundReload();
                x = ClassBasic.GetReducedTime(x);
                DoubleAnimationRing.Duration = new Duration(TimeSpan.FromMilliseconds(x));
                StoryboardRing.Begin();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void RoundReload()
        {
            try
            {
                int i = 0, w = 0, r = 0;
                List<string> RingList = GetRingList();
                Random rnd = new Random();
                for (i = 0; i < 4; i++)
                {
                    string ITEM = RingList[rnd.Next(0, RingList.Count - 1)];

                    switch (ITEM)
                    {
                        case "First":
                            if (w < 4 && r == 1)
                            {
                                string TempColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                                ColorRingOne.Content = AppResources.ResourceManager.GetString(TempColorName);
                            reselectone:
                                SolidColorBrush RandomColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(txtCurrentScore.Text.Trim())));
                            if (ClassBasic.GetColorName(RandomColor) == TempColorName)
                                {
                                    goto reselectone;
                                }
                                else
                                {
                                    ColorRingOne.SegmentColor = RandomColor;
                                }
                                w++;
                            }
                            else
                            {
                                ColorRingOne.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(50)));
                                ColorName = ClassBasic.GetColorName((SolidColorBrush)ColorRingOne.SegmentColor);
                                ColorRingOne.Content = AppResources.ResourceManager.GetString(ColorName); ;
                                r = 1;
                            }

                            break;

                        case "Second":
                            if (w < 4 && r == 1)
                            {
                                string TempColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                                ColorRingTwo.Content = AppResources.ResourceManager.GetString(TempColorName); 
                            reselectTwo:
                                SolidColorBrush RandomColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(txtCurrentScore.Text.Trim())));
                            if (ClassBasic.GetColorName(RandomColor) == TempColorName)
                                {
                                    goto reselectTwo;
                                }
                                else
                                {
                                    ColorRingTwo.SegmentColor = RandomColor;
                                }
                                w++;
                            }
                            else
                            {
                                ColorRingTwo.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(50)));
                                ColorName = ClassBasic.GetColorName((SolidColorBrush)ColorRingTwo.SegmentColor);
                                ColorRingTwo.Content = AppResources.ResourceManager.GetString(ColorName); 
                                r = 1;
                            }

                            break;

                        case "Third":
                            if (w < 4 && r == 1)
                            {
                                string TempColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                                ColorRingThree.Content = AppResources.ResourceManager.GetString(TempColorName); ;
                            reselectThree:
                                SolidColorBrush RandomColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(txtCurrentScore.Text.Trim())));
                            if (ClassBasic.GetColorName(RandomColor) == TempColorName)
                                {
                                    goto reselectThree;
                                }
                                else
                                {
                                    ColorRingThree.SegmentColor = RandomColor;
                                }
                                w++;
                            }
                            else
                            {
                                ColorRingThree.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(50)));
                                ColorName = ClassBasic.GetColorName((SolidColorBrush)ColorRingThree.SegmentColor);
                                ColorRingThree.Content = AppResources.ResourceManager.GetString(ColorName); 
                                r = 1;
                            }

                            break;

                        case "Fourth":
                            if (w < 4 && r == 1)
                            {
                                string TempColorName = ClassBasic.GetRandomColorName(Convert.ToInt32(txtCurrentScore.Text.Trim()));
                                ColorRingFour.Content = AppResources.ResourceManager.GetString(TempColorName); ;
                            reselectFour:
                                SolidColorBrush RandomColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(txtCurrentScore.Text.Trim())));
                            if (ClassBasic.GetColorName(RandomColor) == TempColorName)
                                {
                                    goto reselectFour;
                                }
                                else
                                {
                                    ColorRingFour.SegmentColor = RandomColor;
                                }
                                w++;
                            }
                            else
                            {
                                ColorRingFour.SegmentColor = new SolidColorBrush(ClassBasic.GetRandomColor(Convert.ToInt32(50)));
                                ColorName = ClassBasic.GetColorName((SolidColorBrush)ColorRingFour.SegmentColor);;
                                ColorRingFour.Content = AppResources.ResourceManager.GetString(ColorName); 
                                r = 1;
                            }

                            break;
                    }

                    RingList.Remove(ITEM);
                }
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
                    TapColorAd.Visibility = System.Windows.Visibility.Collapsed;
                    TapColorAd.IsEnabled = false;
                }
                else
                {
                    TapColorAd.Visibility = System.Windows.Visibility.Visible;
                    TapColorAd.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        #endregion

        #region : Private List :

        private List<string> GetRingList()
        {
            try
            {
                List<string> RingList = new List<string>();
                RingList.Add("First");
                RingList.Add("Second");
                RingList.Add("Third");
                RingList.Add("Fourth");

                return RingList;
            }
            catch (Exception)
            {
                throw;
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