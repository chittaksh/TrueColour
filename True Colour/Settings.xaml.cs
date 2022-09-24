using System;
using Microsoft.Phone.Controls;
using Windows.ApplicationModel;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using TrueColour.Resources;

namespace TrueColour
{
    public partial class Settings : PhoneApplicationPage
    {
        #region : Variables :

        TrueColour.Class.AdSetting Advertisement = new TrueColour.Class.AdSetting(); 

        #endregion

        #region : Public Methods :

        public Settings()
        {
            try
            {
                InitializeComponent();
                DisplayVersion();
                ColorList();
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        String versionString(PackageVersion version)
        {
            try
            {
                return String.Format("{0}.{1}.{2}.{3}",
                                     version.Major, version.Minor, version.Build, version.Revision);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DisplayVersion()
        {
            try
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;

                txtTitle.Text = String.Format(AppResources.TitleName + ": " + AppResources.ApplicationTitle + "\n" +
                                              AppResources.TitlePublisher + ": " + AppResources.Publisher + "\n" +
                                              AppResources.TitleVersion + ": " + "{0}\n",
                                              versionString(packageId.Version));
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void ColorList()
        {
            try
            {
                Type colors = typeof(System.Windows.Media.Colors);
                int hi = 0;
                int wi = 0;
                foreach (var prop in colors.GetProperties())
                {
                    Ellipse Temp = new Ellipse { Width = 50, Height = 50 };
                    TextBlock ColorName = new TextBlock();

                    Temp.Fill = new SolidColorBrush((Color)prop.GetValue(null, null));
                    ColorName.Text = AppResources.ResourceManager.GetString(prop.Name);

                    hi = hi + 55;
                    wi = wi + 110;

                    Thickness margin = ColorName.Margin;
                    margin.Top = hi;
                    margin.Left = 80;
                    ColorName.Margin = margin;
                    ColorName.FontSize = 25;

                    Temp.Margin = new Thickness(-400, wi - 830, 0, 0);

                    TempGrid.Children.Add(Temp);
                    TempGrid.Children.Add(ColorName);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling(ex);
            }
        }

        private void btnReviewOption_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Advertisement.StartReview();
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