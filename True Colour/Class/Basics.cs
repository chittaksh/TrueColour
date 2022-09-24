using System;
using System.Collections.Generic;
using System.Windows.Media;
using TrueColour.Resources;


namespace TrueColour.Class
{
    class Basics
    {
        #region : Variables :

        Random rnd = new Random();
        int x = 4;

        #endregion
        
        #region : Public Methods :

        /// <summary>
        /// This code is used to get Color from it's String Name.
        /// </summary>
        /// <param name="ColorName"></param>
        /// <returns></returns>
        public Color GetColor(String ColorName)
        {
            try
            {
                Type colors = typeof(System.Windows.Media.Colors);
                foreach (var prop in colors.GetProperties())
                {
                    if (prop.Name == ColorName)
                        return ((System.Windows.Media.Color)prop.GetValue(null, null));
                }
                throw new Exception("No color found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to retrive the Color Name from Color.
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        public string GetColorName(SolidColorBrush Brush)
        {
            try
            {
                Type colors = typeof(System.Windows.Media.Colors);
                foreach (var prop in colors.GetProperties())
                {
                    if ((System.Windows.Media.Color)prop.GetValue(null, null) == Brush.Color)
                        return prop.Name;
                }
                throw new Exception("No color name found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Code to get Random Color Name from List.
        /// </summary>
        /// <param name="MaxColor"></param>
        /// <returns></returns>
        public String GetRandomColorName(int MaxColor)
        {
            try
            {
                List<string> ColorList = GetColorList();

                if (MaxColor / 5 >= 3)
                    x = MaxColor / 5;

                if (MaxColor / 5 >= 8)
                    x = ColorList.Count - 1;

                return ColorList[rnd.Next(0, x)];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Code to Get Random Color from List.
        /// </summary>
        /// <param name="MaxColor"></param>
        /// <returns></returns>
        public Color GetRandomColor(int MaxColor)
        {
            try
            {
                List<string> ColorList = GetColorList();

                if (MaxColor / 5 >= 3)
                    x = MaxColor / 5;

                if (MaxColor / 5 >= 8)
                    x = ColorList.Count-1;

                return GetColor(ColorList[rnd.Next(0, x)]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Code to Return Round Case.
        /// </summary>
        /// <returns></returns>
        public string GetRoundCase()
        {
            try
            {
                return GetSwitchCases()[rnd.Next(0,2)];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the reduced  time for the Game Round.
        /// </summary>
        /// <param name="OldTime"></param>
        /// <returns></returns>
        public int GetReducedTime(int intOldTime)
        {
            try
            {
                int NewTime = intOldTime - ((intOldTime * 5) / 100);
                return NewTime;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region : Private Lists :

        private List<string> GetColorList()
        {
            try
            {
                List<string> ListColor = new List<string>();
                ListColor.Add("Red");
                ListColor.Add("Blue");
                ListColor.Add("Green");
                ListColor.Add("Yellow");
                ListColor.Add("Brown");
                ListColor.Add("Orange");
                ListColor.Add("Purple");
                ListColor.Add("Pink");

                return ListColor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<string> GetSwitchCases()
        {
            try
            {
                List<string> ListSwitch = new List<string>();
                ListSwitch.Add("Right");
                ListSwitch.Add("Wrong");

                return ListSwitch;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
