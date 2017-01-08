
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
using WpfApplication1.Common;

namespace WpfApplication1
{
    public class AssessmentsGrid : Control
    {

        static AssessmentsGrid()
        {
            InitializeCommands();
        }

        public AssessmentsGrid()
        {
            filteredCollection = new ObservableCollection<Assessment>();
            if (EnterpriseFactory.Instance.Experts.Count > 0)
                SelectedExpert = EnterpriseFactory.Instance.Experts[0];
            Rates = new ObservableCollection<int>();
            for (int i = 1; i <= 10; i++)
                Rates.Add(i);
        }
        #region Properties
        public static DependencyProperty SelectedExpertProperty = DependencyProperty.Register("SelectedExpert", typeof(Expert), typeof(AssessmentsGrid), new PropertyMetadata(SelectedExpertChanged));

        private static void SelectedExpertChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AssessmentsGrid control = d as AssessmentsGrid;
            control.FilterCollection();
        }

        private void FilterCollection()
        {
            if (SelectedExpert!=null && SelectedExpert.ID>=0)
            {
                filteredCollection = new ObservableCollection<Assessment>();
                foreach (Assessment item in EnterpriseFactory.Instance.Assessments)
                {
                    if (item.ExpertID == SelectedExpert.ID)
                    {
                        filteredCollection.Add(item);
                    }
                }
            }
            else
            {
                foreach (Assessment item in EnterpriseFactory.Instance.Assessments)
                {
                    filteredCollection.Add(item);                    
                }
            }
            
        }

        public Expert SelectedExpert
        {
            get { return (Expert)GetValue(SelectedExpertProperty); }
            set { SetValue(SelectedExpertProperty, value); }
        }
        public static DependencyProperty SelectedAssessmentProperty = DependencyProperty.Register("SelectedAssessment", typeof(Assessment), typeof(AssessmentsGrid));
        public Assessment SelectedAssessment
        {
            get { return (Assessment)GetValue(SelectedAssessmentProperty); }
            set { SetValue(SelectedAssessmentProperty, value); }
        }
        public static DependencyProperty filteredCollectionProperty = DependencyProperty.Register("filteredCollection", typeof(ObservableCollection<Assessment>), typeof(AssessmentsGrid));
        public ObservableCollection<Assessment> filteredCollection
        {
            get { return (ObservableCollection<Assessment>)GetValue(filteredCollectionProperty); }
            set { SetValue(filteredCollectionProperty, value); }
        }
        public static DependencyProperty RatesProperty = DependencyProperty.Register("Rates", typeof(ObservableCollection<int>), typeof(AssessmentsGrid));
        public ObservableCollection<int> Rates
        {
            get { return (ObservableCollection<int>)GetValue(RatesProperty); }
            set { SetValue(RatesProperty, value); }
        }
        #endregion
        #region Commands
        private ICommand onAddAssessmentCommand = null;
        public ICommand AddAssessmentCommand
        {
            get
            {
                if (onAddAssessmentCommand == null)
                {
                    onAddAssessmentCommand = new ActionCommand(parameter => AddAssessmentMethod());
                }
                return onAddAssessmentCommand;
            }
        }
        private ICommand onSaveAllCommand = null;
        public ICommand SaveAllCommand
        {
            get
            {
                if (onSaveAllCommand == null)
                {
                    onSaveAllCommand = new ActionCommand(parameter => SaveAllInFile());
                }
                return onSaveAllCommand;
            }
        }
        private void AddAssessmentMethod()
        {
            EnterpriseFactory.Instance.Assessments.Add(new Assessment(Helper.FindFreeNumber<Assessment>(EnterpriseFactory.Instance.Assessments), SelectedExpert.ID, 1, 1, 1, 1, 1, 1));
            FilterCollection();
        }



        private ICommand onDeleteAssessmentCommand = null;
        public ICommand DeleteAssessmentCommand
        {
            get
            {
                if (onDeleteAssessmentCommand == null)
                {
                    onDeleteAssessmentCommand = new ActionCommand(parameter =>
                    {
                        if (parameter == null)
                        {
                            MessageBoxWin.ShowDialog("Please Select Assessment to delete");
                            return;
                        }
                        NS_MessageBoxResult result = MessageBoxWin.ShowDialog("Deleting Assessment", "Sure? Last chance to refuse", NS_MessageBoxButtons.YesNo);
                        if (result == NS_MessageBoxResult.Yes)
                        {
                            Assessment DelAss = parameter as Assessment;
                            EnterpriseFactory.Instance.Assessments.Remove(DelAss);
                            FilterCollection();
                        }

                    });
                }
                return onDeleteAssessmentCommand;
            }
        }

        #endregion
        #region Methods
        public static RoutedCommand AddNewAssessment { get; set; }
        public static void OnAddNewAssessment(object sender, ExecutedRoutedEventArgs e)
        {
            AssessmentsGrid control = sender as AssessmentsGrid;
            if (control == null) return;
            if (e.OriginalSource is DataGridCell)
            {
                try
                {

                }
                catch (InvalidCastException ex)
                {
                    control.AddAssessmentMethod();
                }

            }

        }
        #region CommandMethods


        #endregion
        private static void InitializeCommands()
        {
            AddNewAssessment = new RoutedCommand("AddNewAssessment", typeof(AssessmentsGrid));
            CommandManager.RegisterClassCommandBinding(typeof(AssessmentsGrid),
                new CommandBinding(AddNewAssessment, OnAddNewAssessment));
        }


        private void SaveAllInFile()
        {
            XmlDocument xDoc = new XmlDocument();

            XmlNode GlobalNode = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(GlobalNode);

            XmlNode rootNode = xDoc.CreateElement("AllData");
            if (EnterpriseFactory.Instance.Experts.Count != 0)
            {
                XmlElement experts = xDoc.CreateElement("Experts");
                rootNode.AppendChild(experts);
                foreach (Expert exp in EnterpriseFactory.Instance.Experts)
                {
                    XmlElement expert = xDoc.CreateElement("Expert");
                    experts.AppendChild(expert);
                    exp.SerializeProperties(expert);
                }
            }
            else
            {
                //Show error
            }
            if (EnterpriseFactory.Instance.Risks.Count != 0)
            {
                XmlElement risks = xDoc.CreateElement("Risks");
                rootNode.AppendChild(risks);
                foreach (Risk rsk in EnterpriseFactory.Instance.Risks)
                {
                    XmlElement risk = xDoc.CreateElement("Risk");
                    risks.AppendChild(risk);
                    rsk.SerializeProperties(risk);
                }
            }
            else
            {
                //Show error
            }
            if (EnterpriseFactory.Instance.Assessments.Count != 0)
            {
                XmlElement assessments = xDoc.CreateElement("Assessments");
                rootNode.AppendChild(assessments);
                foreach (Assessment ass in EnterpriseFactory.Instance.Assessments)
                {
                    XmlElement assessment = xDoc.CreateElement("Assessment");
                    assessments.AppendChild(assessment);
                    ass.SerializeProperties(assessment);
                }
            }
            else
            {
                //Show error
            }
            xDoc.AppendChild(rootNode);
            xDoc.Save(System.IO.Path.Combine(EnterpriseFactory.AppDataDirectory, "Config.xml"));
        }
        #endregion

    }

    public class ExpertIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return Helper.FindObjById<Expert>(EnterpriseFactory.Instance.Experts, (int)value);
            }
            else return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Expert)
            {
                return ((Expert)value).ID;
            }
            else return 0;
        }
    }
    public class RiskIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return Helper.FindObjById<Risk>(EnterpriseFactory.Instance.Risks, (int)value);
            }
            else return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Risk)
            {
                return ((Risk)value).ID;
            }
            else return 0;
        }
    }
    public class CollectionFilterConvert : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<Assessment> convertible = null;
            var result = value[0] as ObservableCollection<Assessment>;

            if (result != null)
            {
                convertible = new ObservableCollection<Assessment>();
                foreach (Assessment item in result)
                {
                    //if (item.ExpertID==SelectedEx)
                }
            }

            return convertible;
        }

        public object[] ConvertBack(
        object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
