
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            Series = new ObservableCollection<SeriesData>();
            ObservableCollection<SeriesElement> InfluenceProb = new ObservableCollection<SeriesElement>();
            foreach (Result res in filteredCollection)
            {
                Risk rsk = Helper.FindObjById<Risk>(EnterpriseFactory.Instance.Risks, res.ID);
                if (rsk != null) 
                    InfluenceProb.Add(new SeriesElement() { Category = rsk.Name, Number = res.InfluenceProb });
            } 
            Series.Add(new SeriesData() { SeriesDisplayName = "Risks", Items = InfluenceProb });
            
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
        public static DependencyProperty SeriesProperty = DependencyProperty.Register("Series", typeof(ObservableCollection<SeriesData>), typeof(ResultGrid));
        public ObservableCollection<SeriesData> Series
        {
            get { return (ObservableCollection<SeriesData>)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
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
    public class SeriesData
    {
        public string SeriesDisplayName { get; set; }

        public string SeriesDescription { get; set; }

        public ObservableCollection<SeriesElement> Items { get; set; }
    }
    public class SeriesElement : INotifyPropertyChanged
    {
        public string Category { get; set; }

        private float _number = 0;
        public float Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Number"));
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
