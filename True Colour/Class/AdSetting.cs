using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using System.Windows;
using System.Xml.Linq;
using Windows.Networking.Connectivity;

namespace TrueColour.Class
{
    class AdSetting
    {
        #region : Variable :

        IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
        string FileName = "AdSettings.xml";

        #endregion

        #region : Public Methods :

        public void IncrementLaunchCount()
        {
            try
            {
                XDocument xDoc = OpenDocument();
                int LaunchCount = Convert.ToInt32(xDoc.Root.Element("LaunchCounter").Value);
                int ReviewCounter = Convert.ToInt32(xDoc.Root.Element("ReviewCounter").Value);
                LaunchCount++;

                if (LaunchCount % 5 == 0 && ReviewCounter == 0)
                {
                    MessageBoxResult mm = MessageBox.Show("Thank you for choosing True Colour.\nWould you like to give some time to rate and review this application to help us improve. \nAlso you can go Ad free.", Resources.AppResources.ApplicationTitle, MessageBoxButton.OKCancel);
                    if (mm == MessageBoxResult.OK)
                    {
                        ReviewCounter = LaunchCount;
                        xDoc.Root.Element("LaunchCounter").Value = Convert.ToString(LaunchCount);
                        StartReview();
                    }
                    else
                    {
                        //Do Nothing
                    }
                }
                else
                {
                    if (LaunchCount == ReviewCounter + 25)
                    {
                        ReviewCounter = 0;
                    }
                }

                xDoc.Root.Element("LaunchCounter").Value = Convert.ToString(LaunchCount);
                xDoc.Root.Element("ReviewCounter").Value = Convert.ToString(ReviewCounter);
                SaveFile(xDoc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ReviewDone()
        {
            try
            {
                XDocument xDoc = OpenDocument();
                int ReviewCounter = Convert.ToInt32(xDoc.Root.Element("ReviewCounter").Value);

                if (ReviewCounter != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StartReview()
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                    NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
                    if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                    {
                        MessageBox.Show("No Internet Connection Available. please try again later.", Resources.AppResources.ApplicationTitle, MessageBoxButton.OK);
                    }
                    else
                    {
                        XDocument xDoc = OpenDocument();
                        xDoc.Root.Element("ReviewCounter").Value = xDoc.Root.Element("LaunchCounter").Value;
                        SaveFile(xDoc);
                        MarketplaceReviewTask rr = new MarketplaceReviewTask();
                        rr.Show();
                    }
                }
                else
                {
                    MessageBox.Show("No Internet Connection Available. please try again later.", Resources.AppResources.ApplicationTitle, MessageBoxButton.OK);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region : Private Methods :

        private bool CreateFile()
        {
            try
            {
                bool Created;

                XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

                XElement Root = new XElement(new XElement("AdSettings", xdoc.Root));

                List<string> SettingVariables = GetAdSettingVariables();

                foreach (string Variable in SettingVariables)
                {
                    Root.Add(new XElement(Variable, SettingVariables.IndexOf(Variable)));
                }
                xdoc.Add(Root);

                Created = SaveFile(xdoc);

                return Created;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool SaveFile(XDocument xSave)
        {
            FileStream stream = null;
            try
            {
                bool Saved = false;

                if (fileStorage.FileExists(FileName))
                {
                    stream = fileStorage.OpenFile(FileName, FileMode.Truncate);
                    stream.Close();
                    stream = fileStorage.OpenFile(FileName, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    stream = fileStorage.CreateFile(FileName);
                }

                xSave.Save(stream);
                Saved = true;

                return Saved;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        private XDocument OpenDocument()
        {
            FileStream stream = null;
            try
            {
                XDocument xdoc = new XDocument();
                if (fileStorage.FileExists(FileName))
                {
                    stream = fileStorage.OpenFile(FileName, FileMode.Open);
                    xdoc = XDocument.Load(stream);
                }
                else
                {
                    if (CreateFile())
                    {
                        stream = fileStorage.OpenFile(FileName, FileMode.Open);
                        xdoc = XDocument.Load(stream);
                    }
                }

                return xdoc;
            }
            catch (Exception)
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
                fileStorage.DeleteFile(FileName);
                return OpenDocument();
                //throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        private List<string> GetAdSettingVariables()
        {
            try
            {
                List<string> SettingVariables = new List<string>();
                SettingVariables.Add("ReviewCounter");
                SettingVariables.Add("LaunchCounter");

                return SettingVariables;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
