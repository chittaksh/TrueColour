using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.IO.IsolatedStorage;


namespace TrueColour.Class
{
    class Record
    {
        #region : Variable :

        IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
        string FileName = "Records.xml";

        #endregion

        #region : Public Methods :

        /// <summary>
        /// To get the best game record of a game type.
        /// </summary>
        /// <param name="strGroup"></param>
        /// <returns></returns>
        public int GetFirstRecord(string strGroup)
        {
            try
            {
                XDocument xDoc = OpenDocument();
                return Convert.ToInt32(xDoc.Root.Elements(strGroup).Elements("Score").First().Value);
                
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all the records of a certain game type.
        /// </summary>
        /// <param name="strGroup"></param>
        /// <returns></returns>
        public int[] GetAllRecords(string strGroup)
        {
            try
            {
                XDocument xDoc = OpenDocument();
                int[] Scores = new int[xDoc.Root.Elements(strGroup).Elements("Score").Nodes().Count()];
                int i = 0;

                foreach (XNode element in xDoc.Root.Elements(strGroup).Elements("Score").Nodes())
                {
                    Scores[i] = Convert.ToInt32(element.ToString());
                    i++;
                }

                return Scores;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To add a new record after completion of a round.
        /// </summary>
        /// <param name="strGroup"></param>
        /// <param name="intRecord"></param>
        /// <returns></returns>
        public bool AddNewRecord(string strGroup, int intRecord)
        {
            try
            {
                bool recorded = false;
                XDocument xDoc = OpenDocument();

                if (intRecord > Convert.ToInt32(xDoc.Root.Elements(strGroup).Elements("Score").Last().Value))
                {
                    foreach (XElement element in xDoc.Root.Elements(strGroup).Nodes())
                    {
                        int score = Convert.ToInt32(element.Value);
                        if (score < intRecord)
                        {
                            element.AddBeforeSelf(new XElement("Score", intRecord));
                            recorded = true;
                            if (xDoc.Root.Elements(strGroup).Nodes().Count() > 10)
                            {
                                xDoc.Root.Elements(strGroup).Elements("Score").Last().Remove();
                            }
                            SaveFile(xDoc);
                            break;
                        }
                    }
                }
                else
                {
                    recorded = true;
                }

                return recorded;
            }
            catch(Exception) 
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

                XDocument xdoc = new XDocument(new XDeclaration("1.0","utf-8","yes"));

                XElement Root = new XElement(new XElement("Records", xdoc.Root));

                List<string> GameTypes = GetGameTypes();

                foreach (string GameName in GameTypes)
                {
                    Root.Add(new XElement(GameName, new XElement("Score", "0")));
                }
                xdoc.Add(Root);

                Created = SaveFile(xdoc);

                return Created;
            }
            catch(Exception)
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
                    stream = fileStorage.OpenFile(FileName,FileMode.Append,FileAccess.Write);
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

        private List<string> GetGameTypes()
        {
            try
            {
                List<string> GameTypeList = new List<string>();
                GameTypeList.Add("Classic");
                GameTypeList.Add("Chrono");
                GameTypeList.Add("FindColor");
                GameTypeList.Add("TapColor");

                return GameTypeList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
