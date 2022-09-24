using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using TrueColour.Resources;
using TrueColour.Controls;
//using Microsoft.Live;
//using Microsoft.Live.Controls;
using System.Windows.Media;

namespace TrueColour
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region : Variables : 

        TrueColour.Class.AdSetting Advertisement = new TrueColour.Class.AdSetting();
        //private LiveAuthClient auth;
        //private LiveConnectClient client;
        //private LiveConnectSession session;

        #endregion

        #region : Constructors :
        public MainPage()
        {
            try
            {
                InitializeComponent();
                Advertisement.IncrementLaunchCount();
                RotateCircle.Begin();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        #endregion

        #region : Private Events :

        private void txtPlay_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                if (sender is TextBlock)
                {
                    txtStatus.Text = "Play";
                    txtPlay.Text = AppResources.ApplicationTitle;
                    txtPlay.FontSize = 40;
                }
                else
                {
                    txtStatus.Text = "Records";
                    txtPlay.Text = AppResources.TitlePlay;
                    txtPlay.FontSize = 75;
                }

                MenuOpen();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }

        }

        private void Menu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                switch (txtStatus.Text)
                {
                    case "Play":
                        NavigationService.Navigate(new Uri("/GameTypes/" + (sender as TheRing).Name + ".xaml", UriKind.Relative));
                        break;

                    case "Records":
                        NavigationService.Navigate(new Uri("/Records.xaml?"+AppResources.TitleGameType+"=" + (sender as TheRing).Name, UriKind.Relative));
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void ImageButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                ImageButton Button = sender as ImageButton;
                switch (Button.Name)
                {
                    case "Live":
                            //auth = new LiveAuthClient("0000000048136D62");
                            //auth.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(Live_LoginCompleted);
                            //auth.LoginAsync(new string[] { "wl.signin", "wl.basic" });
  
                        break;

                    case "Settings":
                        NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        //void Live_LoginCompleted(object sender, LoginCompletedEventArgs e)
        //{
        //    if (e.Status == LiveConnectSessionStatus.Connected)
        //    {
        //        session = e.Session;
        //        client = new LiveConnectClient(session);
        //        Live.Background = new SolidColorBrush(Colors.Green);
        //    }
        //    else
        //    {
        //        Live.Background = new SolidColorBrush(Colors.Red);
        //    }
        //}

        #endregion

        #region : Private Methods :

        private void MenuOpen()
        {
            try
            {
                GridSelectGame.Visibility = System.Windows.Visibility.Visible;
                MenuAnimations.Begin();
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
            MessageBoxResult result = MessageBox.Show(AppResources.ErrorMessage, AppResources.ApplicationTitle, MessageBoxButton.OKCancel);

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