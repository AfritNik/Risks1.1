using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication1.Controls
{
    public class OptionsSelectorItem : DependencyObject
    {
        public OptionsSelectorItem(string header, FrameworkElement content)
        {
            Header = header;
            Content = content;
        }

        #region Properties
        public static DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(OptionsSelectorItem));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(FrameworkElement), typeof(OptionsSelectorItem));

        public FrameworkElement Content
        {
            get { return (FrameworkElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(OptionsSelectorItem));

        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(OptionsSelectorItem), new PropertyMetadata(20.0));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(OptionsSelectorItem), new PropertyMetadata(20.0));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        #endregion
    }
}
