
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
using WpfApplication1.Common;

namespace WpfApplication1
{
    public class ResultGrid : Control
    {

        static ResultGrid()
        {
            InitializeCommands();
        }

        public ResultGrid()
        {
            filteredCollection = new ObservableCollection<Result>();
            //if (EnterpriseFactory.Instance.Experts.Count > 0)
            //    SelectedExpert = EnterpriseFactory.Instance.Experts[0];
        }
        #region Properties
        public static DependencyProperty SelectedInfluenceProperty = DependencyProperty.Register("SelectedInfluence", typeof(int), typeof(ResultGrid), new PropertyMetadata(SelectedInfluenceChanged));

        private static void SelectedInfluenceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ResultGrid control = d as ResultGrid;
            control.FilterCollection();
        }

        private void FilterCollection()
        {
            filteredCollection.Clear();
            foreach (Risk rsk in EnterpriseFactory.Instance.Risks)
            {
                int count = 0;
                int Importance = 0;
                int ExtRate = 0;
                int ProtRate = 0;
                int ProbabilityAverage = 0;
                foreach (Assessment obj in EnterpriseFactory.Instance.Assessments)
                {
                    if (obj.RiskID==rsk.ID)
                    {
                        count++;
                        Importance += obj.Probability * obj.Danger;
                        ExtRate += obj.ExternalRate;
                        ProtRate += obj.ProtectionDegree;
                        ProbabilityAverage += obj.InfluenceProb;
                    }
                }
                filteredCollection.Add(new Result(Helper.FindFreeNumber<Result>(filteredCollection), Importance / count, ExtRate / count, ProtRate / count, ProbabilityAverage / count));
            }
            //if (SelectedExpert!=null && SelectedExpert.ID>=0)
            //{
            //    filteredCollection = new ObservableCollection<Result>();
            //    foreach (Result item in EnterpriseFactory.Instance.Results)
            //    {
            //        if (item.ExpertID == SelectedExpert.ID)
            //        {
            //            filteredCollection.Add(item);
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (Result item in EnterpriseFactory.Instance.Results)
            //    {
            //        filteredCollection.Add(item);                    
            //    }
            //}
            
        }

        public int SelectedExpert
        {
            get { return (int)GetValue(SelectedInfluenceProperty); }
            set { SetValue(SelectedInfluenceProperty, value); }
        }
        public static DependencyProperty SelectedResultProperty = DependencyProperty.Register("SelectedResult", typeof(Result), typeof(ResultGrid));
        public Result SelectedResult
        {
            get { return (Result)GetValue(SelectedResultProperty); }
            set { SetValue(SelectedResultProperty, value); }
        }
        public static DependencyProperty filteredCollectionProperty = DependencyProperty.Register("filteredCollection", typeof(ObservableCollection<Result>), typeof(ResultGrid));
        public ObservableCollection<Result> filteredCollection
        {
            get { return (ObservableCollection<Result>)GetValue(filteredCollectionProperty); }
            set { SetValue(filteredCollectionProperty, value); }
        }
        #endregion
        #region Commands
        private ICommand onUpdateCommand = null;
        public ICommand UpdateCommand
        {
            get
            {
                if (onUpdateCommand == null)
                {
                    onUpdateCommand = new ActionCommand(parameter => UpdateMethod());
                }
                return onUpdateCommand;
            }
        }
        private ICommand onSaveAllCommand = null;
        public ICommand SaveAllCommand
        {
            get
            {
                if (onSaveAllCommand == null)
                {
                    //onSaveAllCommand = new ActionCommand(parameter => SaveAllInFile());
                }
                return onSaveAllCommand;
            }
        }
        private void UpdateMethod()
        {
            //EnterpriseFactory.Instance.Results.Add(new Result(Helper.FindFreeNumber<Result>(EnterpriseFactory.Instance.Results), SelectedExpert.ID, 1, 1, 1, 1, 1, 1));
            FilterCollection();
        }



        
        #endregion
        #region Methods
        //public static RoutedCommand AddNewResult { get; set; }
        //public static void OnAddNewResult(object sender, ExecutedRoutedEventArgs e)
        //{
        //    ResultGrid control = sender as ResultGrid;
        //    if (control == null) return;
        //    if (e.OriginalSource is DataGridCell)
        //    {
        //        try
        //        {

        //        }
        //        catch (InvalidCastException ex)
        //        {
        //            control.UpdateMethod();
        //        }

        //    }

        //}
        #region CommandMethods


        #endregion
        private static void InitializeCommands()
        {
            
        }


        //private void SaveAllInFile()
        //{
        //    XmlDocument xDoc = new XmlDocument();

        //    XmlNode GlobalNode = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        //    xDoc.AppendChild(GlobalNode);

        //    XmlNode rootNode = xDoc.CreateElement("AllData");
        //    if (EnterpriseFactory.Instance.Experts.Count != 0)
        //    {
        //        XmlElement experts = xDoc.CreateElement("Experts");
        //        rootNode.AppendChild(experts);
        //        foreach (Expert exp in EnterpriseFactory.Instance.Experts)
        //        {
        //            XmlElement expert = xDoc.CreateElement("Expert");
        //            experts.AppendChild(expert);
        //            exp.SerializeProperties(expert);
        //        }
        //    }
        //    else
        //    {
        //        //Show error
        //    }
        //    if (EnterpriseFactory.Instance.Risks.Count != 0)
        //    {
        //        XmlElement risks = xDoc.CreateElement("Risks");
        //        rootNode.AppendChild(risks);
        //        foreach (Risk rsk in EnterpriseFactory.Instance.Risks)
        //        {
        //            XmlElement risk = xDoc.CreateElement("Risk");
        //            risks.AppendChild(risk);
        //            rsk.SerializeProperties(risk);
        //        }
        //    }
        //    else
        //    {
        //        //Show error
        //    }
        //    if (EnterpriseFactory.Instance.Results.Count != 0)
        //    {
        //        XmlElement Results = xDoc.CreateElement("Results");
        //        rootNode.AppendChild(Results);
        //        foreach (Result ass in EnterpriseFactory.Instance.Results)
        //        {
        //            XmlElement Result = xDoc.CreateElement("Result");
        //            Results.AppendChild(Result);
        //            ass.SerializeProperties(Result);
        //        }
        //    }
        //    else
        //    {
        //        //Show error
        //    }
        //    xDoc.AppendChild(rootNode);
        //    xDoc.Save(System.IO.Path.Combine(EnterpriseFactory.AppDataDirectory, "Config.xml"));
        //}
        #endregion

    }
    public class RiskIDToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                Risk rsk = Helper.FindObjById<Risk>(EnterpriseFactory.Instance.Risks, (int)value);
                if (rsk != null) return rsk.Name;
                else return null;
            }
            else return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
