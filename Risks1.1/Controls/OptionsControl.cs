//using NS.AppFramework.UICore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1.Controls
{
 
    public class OptionsControl : Control
    {
        static OptionsControl()
        {
            InitializeCommands();
            //ThemeManager.LoadTheme(typeof(OptionsControl), ViewType.GlobalControls);
        }

        public OptionsControl()
        {
            ItemsCollection = new ObservableCollection<OptionsSelectorItem>();
        }

        public OptionsControl(List<OptionsSelectorItem> menuItems)
        {
            ItemsCollection = new ObservableCollection<OptionsSelectorItem>();
            foreach (OptionsSelectorItem item in menuItems)
            {
                ItemsCollection.Add(item);
            }

            if (ItemsCollection.Count > 0)
                SelectedMenuItem = ItemsCollection[0];
        }

        public static DependencyProperty ItemsCollectionProperty = DependencyProperty.Register("ItemsCollection", typeof(ObservableCollection<OptionsSelectorItem>), typeof(OptionsControl));

        public ObservableCollection<OptionsSelectorItem> ItemsCollection
        {
            get { return (ObservableCollection<OptionsSelectorItem>)GetValue(ItemsCollectionProperty); }
            set { SetValue(ItemsCollectionProperty, value); }
        }

        public static DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register("SelectedMenuItem", typeof(OptionsSelectorItem), typeof(OptionsControl), new PropertyMetadata(MenuSelectedItemChanged));

        public OptionsSelectorItem SelectedMenuItem
        {
            get { return (OptionsSelectorItem)GetValue(SelectedMenuItemProperty); }
            set { SetValue(SelectedMenuItemProperty, value); }
        }

        public static RoutedCommand CloseCommand { get; set; }
        public static void OnCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            OptionsControl control = sender as OptionsControl;
            if (control == null) return;
            
            //EnterpriseFactory.FindWindow(control).Close();
        }
        private static void InitializeCommands()
        {
            CloseCommand = new RoutedCommand("CloseCommand", typeof(OptionsControl));
            CommandManager.RegisterClassCommandBinding(typeof(OptionsControl),
                new CommandBinding(CloseCommand, OnCloseCommand));

        }

        #region ?
        private static void MenuSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OptionsControl control = d as OptionsControl;
            if (e.NewValue == null || control == null) return;


        }
        #endregion
    }
    public class MenuSelectorItem : DependencyObject
    {
        public MenuSelectorItem(string header, FrameworkElement content)
        {
            Header = header;
            Content = content;
        }

        #region Properties
        public static DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(MenuSelectorItem));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(FrameworkElement), typeof(MenuSelectorItem));

        public FrameworkElement Content
        {
            get { return (FrameworkElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(MenuSelectorItem));

        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(MenuSelectorItem), new PropertyMetadata(20.0));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(MenuSelectorItem), new PropertyMetadata(20.0));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        #endregion
    }
}
