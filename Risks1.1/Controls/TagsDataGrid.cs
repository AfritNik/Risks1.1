using Microsoft.Win32;
using NS.Controls.CustomControls;
using NS.Enterprise.Objects.Modbus.SCADA;
using NS.Enterprise.WidgetFactory.Common.ICommands;
using NS.Enterprise.WidgetFactory.Common.Utils;
using NS.Enterprise.WidgetFactory.UI.Dialogs.MessageBox;
using NS.Enterprise.WidgetFactory.UI.Widgets.Telemetry.ScadaWidgets.TagWindows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace NS.Enterprise.WidgetFactory.UI.Widgets.Telemetry.Common.Grids
{
    public class TagsDataGrid : Control
    {
        
        static TagsDataGrid()
        {
            
            InitializeCommands();
            ThemeManager.LoadTheme(typeof(TagsDataGrid), ViewType.ScadaGrids);
        }
        
        public TagsDataGrid()
        {
            //this.Width = 950;
            //this.Height = 300;
            //EnterpriseFactory.Instance.WidgetsCollection.Add(this);
            //AppFramework.UICore.Behaviors.EditBehavior.SetLMBPressEnterEditTime(this, new TimeSpan(0));
        }
        #region Properties
        public static DependencyProperty SelectedQueryProperty = DependencyProperty.Register("SelectedQuery", typeof(ScadaDeviceUpdateQuery), typeof(TagsDataGrid));

        //private static void SelectedQueryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    TagsDataGrid control = d as TagsDataGrid;
        //    if (control == null) return;
        //    control.IsScadaPropertiesOpen = true;
        //    if (control is TagsDataGrid || e.NewValue == null)
        //    {
        //        if (control.IsScadaPropertiesOpen == false) control.IsScadaPropertiesOpen = true;
        //        ReInitializeControl(control, (ScadaDeviceUpdateQuery)e.NewValue);
        //    }

        //}
        public ScadaDeviceUpdateQuery SelectedQuery
        {
            get { return (ScadaDeviceUpdateQuery)GetValue(SelectedQueryProperty); }
            set { SetValue(SelectedQueryProperty, value); }
        }
        #endregion
        #region Commands
        private ICommand onAddTagCommand = null;
        public ICommand AddTagCommand
        {
            get
            {
                if (onAddTagCommand == null)
                {
                    onAddTagCommand = new ActionCommand(parameter => AddTagMethod());
                }
                return onAddTagCommand;
            }
        }
        private void AddTagMethod()
        {
            CustomWindow wnd = new CustomWindow();
            //CustomWindow wnd = new CustomWindow();
            TagSettingsControl cl = new TagSettingsControl();
            wnd.Content = cl;
            wnd.Title = "Add New Tag";
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.Owner = Window.GetWindow(this);
            wnd.MinHeight = 260;
            wnd.MaxHeight = 380;
            wnd.MinWidth = 360;
            wnd.MaxWidth = 680;
            wnd.ShowDialog();
        }
        private ICommand onAddRangeTagsCommand = null;
        public ICommand AddRangeTagsCommand
        {
            get
            {
                if (onAddRangeTagsCommand == null)
                {
                    onAddRangeTagsCommand = new ActionCommand(parameter =>
                    {
                         CustomWindow wnd = new CustomWindow();

                        TagRangeSettingsControl cl = new TagRangeSettingsControl();
                        //cl.SelectedQuery = new Objects.Modbus.SCADA.ScadaDeviceUpdateQuery();
                        wnd.Content = cl;
                        //wnd.Name = "Add new tag";
                        wnd.Title = "Add Range";
                        wnd.MinHeight = 260;
                        wnd.MaxHeight = 380;
                        wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        wnd.Owner = Window.GetWindow(this);
                        wnd.ShowDialog();
                    });
                }
                return onAddRangeTagsCommand;
            }
        }
        private ICommand onEditTagCommand = null;
        public ICommand EditTagCommand
        {
            get
            {
                if (onEditTagCommand == null)
                {
                    onEditTagCommand = new ActionCommand(parameter =>
                    {
                        
                        if (parameter==null || this.SelectedQuery==null)
                        {
                            MessageBoxWin.ShowDialog("Please Select Tag to edit");
                            return;
                        }
                         CustomWindow wnd = new CustomWindow();
                        wnd.Title = "Editing Tag";
                        wnd.MinHeight = 260;
                        wnd.MaxHeight = 380;
                        TagSettingsControl cl = new TagSettingsControl(SelectedQuery, false);
                        //cl.SelectedQuery = new Objects.Modbus.SCADA.ScadaDeviceUpdateQuery();
                        wnd.Content = cl;
                        wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        wnd.Owner = Window.GetWindow(this);
                        wnd.ShowDialog();
                    });
                }
                return onEditTagCommand;
            }
        }
        private ICommand onImportCommand = null;
        public ICommand OnImportCommand
        {
            get
            {
                if (onImportCommand == null)
                {
                    onImportCommand = new ActionCommand(parameter =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "CSV files (*.csv)|*.csv";

                        if (ofd.ShowDialog() != true)
                            return;

                        CsvLoader loader = new CsvLoader(CsvFormatType.ScadaTags, ofd.FileName);
                        loader.ImportData();
                    });
                }
                return onImportCommand;
            }
        }
        private ICommand onExportCommand = null;
        public ICommand OnExportCommand
        {
            get
            {
                if (onExportCommand == null)
                {
                    onExportCommand = new ActionCommand(parameter =>
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "CSV files (*.csv)|*.csv";

                        if (sfd.ShowDialog() != true)
                            return;

                        CsvLoader loader = new CsvLoader(CsvFormatType.ScadaTags, sfd.FileName);
                        loader.ExportData();
                    });
                }
                return onExportCommand;
            }
        }
        //private ICommand onEnabledTagCommand = null;
        //public ICommand EnableTagCommand
        //{
        //    get
        //    {
        //        if (onEnabledTagCommand == null)
        //        {
        //            onEnabledTagCommand = new ActionCommand(parameter =>
        //            {

        //                if (parameter == null || this.SelectedQuery == null)
        //                {
        //                    MessageBoxWin.ShowDialog("Please Select Tag to edit");
        //                    return;
        //                }
        //                Controls.CustomControls.CustomWindow wnd = new Controls.CustomControls.CustomWindow();
        //                wnd.Title = "Editing tag";
        //                wnd.MaxHeight = 550;
        //                wnd.MinHeight = 550;
        //                wnd.MaxWidth = 300;
        //                wnd.MinWidth = 300;
        //                wnd.Height = 550;
        //                wnd.Width = 300;
        //                TagSettingsControl cl = new TagSettingsControl(SelectedQuery, false);
        //                cl.SelectedQuery = new Objects.Modbus.SCADA.ScadaDeviceUpdateQuery();
        //                wnd.Content = cl;
        //                wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //                wnd.Owner = Window.GetWindow(this);
        //                wnd.ShowDialog();
        //            });
        //        }
        //        return onEnabledTagCommand;
        //    }
        //}
        private ICommand onDeleteTagCommand = null;
        public ICommand DeleteTagCommand
        {
            get
            {
                if (onDeleteTagCommand == null)
                {
                    onDeleteTagCommand = new ActionCommand(parameter =>
                    {
                        if (parameter == null)
                        {
                            MessageBoxWin.ShowDialog("Please Select Tag to delete");
                            return;
                        }
                        NS_MessageBoxResult result = MessageBoxWin.ShowDialog("Deleting Tag", "Sure? Last chance to refuse", NS_MessageBoxButtons.YesNo);
                        if (result==NS_MessageBoxResult.Yes)
                        {
                            ServerManager.Instance.DeleteScadaQuery((ScadaDeviceUpdateQuery)parameter);
                        }
                        // Controls.CustomControls.CustomWindow wnd = new Controls.CustomControls.CustomWindow();
                        //wnd.Width = 160;
                        //wnd.Height = 250;
                        //TagSettingsControl cl = new TagSettingsControl((ScadaDeviceUpdateQuery)parameter);
                        ////cl.SelectedQuery = new Objects.Modbus.SCADA.ScadaDeviceUpdateQuery();
                        //wnd.Content = cl;
                        //wnd.Show();
                    });
                }
                return onDeleteTagCommand;
            }
        }

        #endregion
        #region Methods

        #region CommandMethods

        public static RoutedCommand AddNewQuery { get; set; }
        public static void OnAddNewQuery(object sender, ExecutedRoutedEventArgs e)
        {
            TagsDataGrid control = sender as TagsDataGrid;
            if (control == null) return;
            if (e.OriginalSource is DataGridCell)
            {
                try
                {
                    ScadaDeviceUpdateQuery query = (ScadaDeviceUpdateQuery)((DataGridCell)e.OriginalSource).DataContext;
                    CustomWindow wnd = new CustomWindow();
                    query.Name += "_Copy";
                    TagSettingsControl cl = new TagSettingsControl(query, true);
                    wnd.Content = cl;
                    wnd.Title = "Add New Tag";
                    wnd.MinHeight = 260;
                    wnd.MaxHeight = 380;
                    wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    wnd.Owner = Window.GetWindow(control);
                    wnd.ShowDialog();
                }
                catch (InvalidCastException ex)
                {
                    control.AddTagMethod();
                }

            }

        }
        public static RoutedCommand EnableTag { get; set; }
        public static void OnEnableTag(object sender, ExecutedRoutedEventArgs e)
        {
            TagsDataGrid control = sender as TagsDataGrid;
            if (control == null) return;

            if (control.SelectedQuery != null)
            {
                control.SelectedQuery.IsEnabled = (bool)((ToggleButton)e.OriginalSource).IsChecked;
                List<ScadaDeviceUpdateQuery> quieries = new List<ScadaDeviceUpdateQuery>();
                quieries.Add(control.SelectedQuery);
                ServerManager.Instance.AddUpdateScadaQuery(quieries);
            }
        }
        public static RoutedCommand ShowTresholds { get; set; }
        public static void OnShowTresholds(object sender, ExecutedRoutedEventArgs e)
        {
            TagsDataGrid control = sender as TagsDataGrid;
            if (control == null) return;

            if (control.SelectedQuery != null)
            {
                CustomWindow wnd = new CustomWindow();
                wnd.Title = "Thresholds";
                wnd.MinHeight = 260;
                wnd.MaxHeight = 380;
                TagThresholdsGrid cl = new TagThresholdsGrid(control.SelectedQuery.Tresholds);
                //TagSettingsControl cl = new TagSettingsControl(SelectedQuery, false);
                //cl.SelectedQuery = new Objects.Modbus.SCADA.ScadaDeviceUpdateQuery();
                wnd.Content = cl;
                wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                wnd.Owner = Window.GetWindow(control);
                wnd.ShowDialog();
            }

        }
        public static RoutedCommand ChangeQuery { get; set; }
        public static void OnChangeQuery(object sender, ExecutedRoutedEventArgs e)
        {
            //Check all grid and send on Update
        }
        public static RoutedCommand Exit { get; set; }
        public static void OnExit(object sender, ExecutedRoutedEventArgs e)
        {
            TagThresholdsGrid control = sender as TagThresholdsGrid;
            Window parentWindow = Window.GetWindow(control);
            parentWindow.Close();

        }
        #endregion
        private static void InitializeCommands()
        {
            EnableTag = new RoutedCommand("EnableTag", typeof(TagsDataGrid));
            CommandManager.RegisterClassCommandBinding(typeof(TagsDataGrid),
                new CommandBinding(EnableTag, OnEnableTag));
            AddNewQuery = new RoutedCommand("AddNewQuery", typeof(TagsDataGrid));
            CommandManager.RegisterClassCommandBinding(typeof(TagsDataGrid),
                new CommandBinding(AddNewQuery, OnAddNewQuery));
            ShowTresholds = new RoutedCommand("ShowTresholds", typeof(TagsDataGrid));
            CommandManager.RegisterClassCommandBinding(typeof(TagsDataGrid),
                new CommandBinding(ShowTresholds, OnShowTresholds));
            
            ChangeQuery = new RoutedCommand("ChangeQuery", typeof(TagsDataGrid));
            CommandManager.RegisterClassCommandBinding(typeof(TagsDataGrid),
                new CommandBinding(ChangeQuery, OnChangeQuery));
            Exit = new RoutedCommand("Exit", typeof(TagsDataGrid));
            CommandManager.RegisterClassCommandBinding(typeof(TagsDataGrid),
                new CommandBinding(Exit, OnExit));
        }

        #endregion

    }
    

    public class ModbusConfigConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Guid)
            {
                foreach (PLCobject conf in EnterpriseFactory.Instance.PLCobjects)
                {
                    if (conf.Id == (Guid)value)
                        return conf;
                }
                return null;
            }
            else return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is PLCobject)
            {
                return ((PLCobject)value).Id;
            }
            else return Guid.Empty;
        }
    }
}
