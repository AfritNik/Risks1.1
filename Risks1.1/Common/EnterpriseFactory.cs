using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1.Common
{
    public class EnterpriseFactory : DependencyObject
    {
        static EnterpriseFactory()
        {
            Instance = new EnterpriseFactory();
        }
        public static EnterpriseFactory Instance
        { get; private set; }

        public EnterpriseFactory()
        {
            Experts = new ObservableCollection<Expert>();
            Assessments = new ObservableCollection<Assessment>();
            Risks = new ObservableCollection<Risk>();
            
        }

        public static DependencyProperty ExpertsProperty = DependencyProperty.Register("Experts", typeof(ObservableCollection<Expert>), typeof(EnterpriseFactory));
        public ObservableCollection<Expert> Experts
        {
            get { return (ObservableCollection<Expert>)GetValue(ExpertsProperty); }
            set { SetValue(ExpertsProperty, value); }
        }

        public static DependencyProperty AssessmentsProperty = DependencyProperty.Register("Assessments", typeof(ObservableCollection<Assessment>), typeof(EnterpriseFactory));
        public ObservableCollection<Assessment> Assessments
        {
            get { return (ObservableCollection<Assessment>)GetValue(AssessmentsProperty); }
            set { SetValue(AssessmentsProperty, value); }
        }
        public static DependencyProperty RisksProperty = DependencyProperty.Register("Risks", typeof(ObservableCollection<Risk>), typeof(EnterpriseFactory));
        public ObservableCollection<Risk> Risks
        {
            get { return (ObservableCollection<Risk>)GetValue(RisksProperty); }
            set { SetValue(RisksProperty, value); }
        }

        private static string m_appDataDirectory = null;
        public static string AppDataDirectory
        {
            get
            {
                if (!string.IsNullOrEmpty(m_appDataDirectory))
                    return m_appDataDirectory;

                string dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "Nik's Production" + "\\" + "Risks");
                try
                {
                    if (!Directory.Exists(dataDirectory))
                        Directory.CreateDirectory(dataDirectory);

                    m_appDataDirectory = dataDirectory;
                    return m_appDataDirectory;
                }
                catch (Exception ex)
                {
                    //Write to log
                }

                return string.Empty;
            }
        }
    }
}