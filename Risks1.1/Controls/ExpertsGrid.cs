
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApplication1.Common;

namespace WpfApplication1
{
    public class ExpertsGrid : Control
    {
        
        static ExpertsGrid()
        {            
            InitializeCommands();            
        }
        
        public ExpertsGrid()
        {
           
        }
        #region Properties
        public static DependencyProperty SelectedExpertProperty = DependencyProperty.Register("SelectedExpert", typeof(Expert), typeof(ExpertsGrid));
        public Expert SelectedExpert
        {
            get { return (Expert)GetValue(SelectedExpertProperty); }
            set { SetValue(SelectedExpertProperty, value); }
        }
        #endregion
        #region Commands
        private ICommand onAddExpertCommand = null;
        public ICommand AddExpertCommand
        {
            get
            {
                if (onAddExpertCommand == null)
                {
                    onAddExpertCommand = new ActionCommand(parameter => AddExpertMethod());
                }
                return onAddExpertCommand;
            }
        }
        private void AddExpertMethod()
        {
            EnterpriseFactory.Instance.Experts.Add(new Expert(Helper.FindFreeNumber<Expert>(EnterpriseFactory.Instance.Experts), "New Name"));
        }
     
        private ICommand onEditExpertCommand = null;
        public ICommand EditExpertCommand
        {
            get
            {
                if (onEditExpertCommand == null)
                {
                    onEditExpertCommand = new ActionCommand(parameter =>
                    {
                        
                        if (parameter==null || this.SelectedExpert==null)
                        {
                            MessageBoxWin.ShowDialog("Please Select Expert to edit");
                            return;
                        }
                        Window wnd = new Window();
                        wnd.Title = "Editing tag";
                        wnd.MaxHeight = 550;
                        wnd.MinHeight = 550;
                        wnd.MaxWidth = 300;
                        wnd.MinWidth = 300;
                        wnd.Height = 550;
                        wnd.Width = 300;
                        //ExpertSettingsControl cl = new ExpertSettingsControl(SelectedPLC, false);
                        ////cl.SelectedPLC = new Objects.Modbus.SCADA.ModbusConfig();
                        //wnd.Content = cl;
                        //wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        //wnd.Owner = Window.GetWindow(this);
                        wnd.ShowDialog();
                    });
                }
                return onEditExpertCommand;
            }
        }
        
        private ICommand onDeleteExpertCommand = null;
        public ICommand DeleteExpertCommand
        {
            get
            {
                if (onDeleteExpertCommand == null)
                {
                    onDeleteExpertCommand = new ActionCommand(parameter =>
                    {
                        if (parameter == null)
                        {
                            MessageBoxWin.ShowDialog("Please Select Expert to delete");
                            return;
                        }
                        NS_MessageBoxResult result = MessageBoxWin.ShowDialog("Deleting Expert", "Sure? Last chance to refuse", NS_MessageBoxButtons.YesNo);
                        if (result == NS_MessageBoxResult.Yes)
                        {
                            Expert DelExp = parameter as Expert;
                            EnterpriseFactory.Instance.Experts.Remove(DelExp);
                        }

                    });
                }
                return onDeleteExpertCommand;
            }
        }

        #endregion
        #region Methods
        public static RoutedCommand AddNewExpert { get; set; }
        public static void OnAddNewExpert(object sender, ExecutedRoutedEventArgs e)
        {
            ExpertsGrid control = sender as ExpertsGrid;
            if (control == null) return;
            if (e.OriginalSource is DataGridCell)
            {
                try
                {
                   
                }
                catch (InvalidCastException ex)
                {
                    control.AddExpertMethod();
                }

            }

        }
        #region CommandMethods


        #endregion
        private static void InitializeCommands()
        {
            AddNewExpert = new RoutedCommand("AddNewExpert", typeof(ExpertsGrid));
            CommandManager.RegisterClassCommandBinding(typeof(ExpertsGrid),
                new CommandBinding(AddNewExpert, OnAddNewExpert));
        }

        #endregion

    }      
}
