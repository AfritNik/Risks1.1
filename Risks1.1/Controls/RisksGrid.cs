
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApplication1.Common;

namespace WpfApplication1
{
    public class RisksGrid : Control
    {
        
        static RisksGrid()
        {            
            InitializeCommands();            
        }
        
        public RisksGrid()
        {
           
        }
        #region Properties
        public static DependencyProperty SelectedRiskProperty = DependencyProperty.Register("SelectedRisk", typeof(Risk), typeof(RisksGrid));
        public Risk SelectedRisk
        {
            get { return (Risk)GetValue(SelectedRiskProperty); }
            set { SetValue(SelectedRiskProperty, value); }
        }
        #endregion
        #region Commands
        private ICommand onAddRiskCommand = null;
        public ICommand AddRiskCommand
        {
            get
            {
                if (onAddRiskCommand == null)
                {
                    onAddRiskCommand = new ActionCommand(parameter => AddRiskMethod());
                }
                return onAddRiskCommand;
            }
        }
        private void AddRiskMethod()
        {
            EnterpriseFactory.Instance.Risks.Add(new Risk(Helper.FindFreeNumber<Risk>(EnterpriseFactory.Instance.Risks), "New Risk"));
        }
       
        
        
        private ICommand onDeleteRiskCommand = null;
        public ICommand DeleteRiskCommand
        {
            get
            {
                if (onDeleteRiskCommand == null)
                {
                    onDeleteRiskCommand = new ActionCommand(parameter =>
                    {
                        if (parameter == null)
                        {
                            MessageBoxWin.ShowDialog("Please Select Risk to delete");
                            return;
                        }
                        NS_MessageBoxResult result = MessageBoxWin.ShowDialog("Deleting Risk", "Sure? Last chance to refuse", NS_MessageBoxButtons.YesNo);
                        if (result == NS_MessageBoxResult.Yes)
                        {
                            Risk DelRsk = parameter as Risk;
                            EnterpriseFactory.Instance.Risks.Remove(DelRsk);
                        }

                    });
                }
                return onDeleteRiskCommand;
            }
        }

        #endregion
        #region Methods
        public static RoutedCommand AddNewRisk { get; set; }
        public static void OnAddNewRisk(object sender, ExecutedRoutedEventArgs e)
        {
            RisksGrid control = sender as RisksGrid;
            if (control == null) return;
            if (e.OriginalSource is DataGridCell)
            {
                try
                {
                   
                }
                catch (InvalidCastException ex)
                {
                    control.AddRiskMethod();
                }

            }

        }
        #region CommandMethods


        #endregion
        private static void InitializeCommands()
        {
            AddNewRisk = new RoutedCommand("AddNewRisk", typeof(RisksGrid));
            CommandManager.RegisterClassCommandBinding(typeof(RisksGrid),
                new CommandBinding(AddNewRisk, OnAddNewRisk));
        }

        #endregion

    }      
}
