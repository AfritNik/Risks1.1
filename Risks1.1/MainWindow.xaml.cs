using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Common;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Assessment> assessments = new ObservableCollection<Assessment>();
        public MainWindow()
        {
            XamlResourceLoader.Instance.LoadResource(XamlResourceLoader.Instance.RisksThemeFiles, App.Current.Resources);

            //assessments.Add(new Assessment(1, 1, 1, 1, 1, 1, 1, 1));
            Helper.LoadFromFile();
            InitializeComponent();
            ExpertsGrid experts = new ExpertsGrid();
            RisksGrid risks = new RisksGrid();
            AssessmentsGrid asm = new AssessmentsGrid();
            
            //EnterpriseFactory.Instance.Experts.Add(new Expert(1, "Nik"));
            //EnterpriseFactory.Instance.Risks.Add(new Risk(1, "NesterovExam"));
            //EnterpriseFactory.Instance.Assessments.Add(new Assessment(1, 1, 1, 1, 1, 1, 1, 1));
            OptControl.ItemsCollection.Add(new OptionsSelectorItem("Experts", experts));
            OptControl.ItemsCollection.Add(new OptionsSelectorItem("Risks", risks));
            OptControl.ItemsCollection.Add(new OptionsSelectorItem("Assessments", asm));
            OptControl.ItemsCollection.Add(new OptionsSelectorItem("Charts", asm));
            OptControl.SelectedMenuItem = OptControl.ItemsCollection[0];
        }

    }
}
