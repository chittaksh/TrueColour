using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TrueColour.Controls
{
    public partial class TheRing : UserControl
    {

        #region : Constructors :

        public TheRing()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region : Dependency Property :

        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(TheRing), new PropertyMetadata(00d, new PropertyChangedCallback(OnPercentageChanged)));

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(TheRing), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPropertyChanged)));

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColorProperty =
            DependencyProperty.Register("SegmentColor", typeof(Brush), typeof(TheRing), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), new PropertyChangedCallback(OnPropertyChanged)));

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(TheRing), new PropertyMetadata(25, new PropertyChangedCallback(OnPropertyChanged)));

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(TheRing), new PropertyMetadata(00d, new PropertyChangedCallback(OnPropertyChanged)));

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Brush), typeof(TheRing), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), new PropertyChangedCallback(OnPropertyChanged)));

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentColorProperty =
            DependencyProperty.Register("ContentColor", typeof(Brush), typeof(TheRing), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), new PropertyChangedCallback(OnPropertyChanged)));


        #endregion

        #region : Properties :

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public Brush SegmentColor
        {
            get { return (Brush)GetValue(SegmentColorProperty); }
            set { SetValue(SegmentColorProperty, value); }
        }

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public Brush FillColor
        {
            get { return (Brush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public Brush ContentColor
        {
            get { return (Brush)GetValue(ContentColorProperty); }
            set { SetValue(ContentColorProperty, value); }
        }

        #endregion

        #region : Private Methods :

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void drawSector()
        {
            try
            {
                txtColorName.FontSize = Radius / 2;
                txtColorName.Text = Content;

                Point startPoint = new Point(Radius, 0);
                Point endPoint = ComputeCartesianCoordinate(Angle, Radius);
                endPoint.X += Radius;
                endPoint.Y += Radius;

                Ring.Stroke = SegmentColor;
                Ring.Width = Radius * 2 + 10;
                Ring.Height = Radius * 2 + 10;
                Ring.Margin = new Thickness(10, 10, 0, 0);

                bool largeArc = Angle > 180.0;

                Size outerArcSize = new Size(Radius, Radius);

                pathFigure.StartPoint = startPoint;

                if (startPoint.X == Math.Round(endPoint.X) && startPoint.Y == Math.Round(endPoint.Y))
                    endPoint.X -= 0.01;

                arcSegment.Point = endPoint;
                arcSegment.Size = outerArcSize;
                arcSegment.IsLargeArc = largeArc;

                Ring.Fill = FillColor;

                if (ReadLocalValue(ContentColorProperty)==DependencyProperty.UnsetValue)
                {
                    txtColorName.Foreground = SegmentColor;
                }
                else
                {
                    txtColorName.Foreground = ContentColor;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Point ComputeCartesianCoordinate(double angle, double radius)
        {
            try
            {
                // convert to radians
                double angleRad = (Math.PI / 180.0) * (angle - 90);

                double x = radius * Math.Cos(angleRad);
                double y = radius * Math.Sin(angleRad);

                return new Point(x, y);
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
        private void TheRing_SizeChanged(object sender, SizeChangedEventArgs e)
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

        private static void OnPercentageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                TheRing circle = sender as TheRing;
                circle.Angle = (circle.Percentage * 360) / 100;
            }
            catch (Exception)
            { throw; }
        }

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                TheRing circle = sender as TheRing;
                circle.drawSector();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
