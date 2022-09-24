using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using TrueColour.Resources;

namespace TrueColour
{
    public partial class Records : PhoneApplicationPage
    {
        #region : Variable :

        TrueColour.Class.Record ClassRecord = new TrueColour.Class.Record();

        #endregion

        #region : Constructor :

        public Records()
        {
            InitializeComponent();
        }

        #endregion

        #region : Protected Events :

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);

                string strGameType = string.Empty;
                int[] Records;

                if (NavigationContext.QueryString.TryGetValue(AppResources.TitleGameType, out strGameType))
                {
                    Records = ClassRecord.GetAllRecords(strGameType);
                }
                else
                {
                    Records = new Int32[0];
                }

                switch (strGameType)
                {
                    case "Classic":
                        txtGameType.Text = AppResources.TitleClassic;
                        break;

                    case "Chrono":
                        txtGameType.Text = AppResources.TitleChrono;
                        break;

                    case "FindColor":
                        txtGameType.Text = AppResources.TitleFindColor;
                        break;

                    case "TapColor":
                        txtGameType.Text = AppResources.TitleTapColor;
                        break;

                    default:
                        txtGameType.Text = string.Empty;
                        break;
                }

                if (Records.Length > 0)
                {
                    txtTopScore.Text = Convert.ToString(Records[0]);
                }

                if (Records.Length > 1)
                {
                    for (int i = 1; i < Records.Count(); i++)
                    {
                        txtOtherScore.Text += "#" + (i + 1) + "\t\t\t" + Records[i] + "\n";

                    }
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