using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using WpfApplication1.Controls;

namespace WpfApplication1.Common
{
    public static class Helper
    {
        public static T FindParent<T>(FrameworkElement control)
          where T : FrameworkElement
        {
            if (control == null)
                return null;

            Type type;
            DependencyObject dpParent = control;
            do
            {
                dpParent = VisualTreeHelper.GetParent(dpParent);
                if (dpParent == null)
                    return null;
                type = dpParent.GetType();
            } while (!type.IsSubclassOf(typeof(T)) && type != typeof(T));

            return dpParent as T;
        }
        public static int FindFreeNumber<T>(ObservableCollection<T> collection) where T : ObjectBase
        {
            List<int> ints = new List<int>();
            
            foreach (T obj in collection)
            {                
                ints.Add(obj.ID);
            }
            int counter = ints.Count() > 0 ? ints.First() : -1;

            while (counter < int.MaxValue)
            {
                if (!ints.Contains(++counter)) return counter;
            }

            return 0;
        }

        public static T FindObjById<T>(ObservableCollection<T> collection,  int id) where T : ObjectBase
        {
            foreach (T obj in collection)
            {
                if (obj.ID == id) return obj;
            }
            return null;
        }
        public static void LoadFromFile()
        {
            String fileName = System.IO.Path.Combine(System.IO.Path.Combine(EnterpriseFactory.AppDataDirectory, "Config.xml"));
            if (File.Exists(fileName))
            {
                XmlDocument data = new XmlDocument();
                data.Load(fileName);

                XmlNode rootNode = data.SelectSingleNode("AllData");
                if (rootNode != null && rootNode is XmlElement)
                {
                    try
                    {
                        foreach (XmlNode child in rootNode.SelectSingleNode("Experts"))
                        {
                            Expert exp = new Expert(0);
                            exp.DeserializeProperties(child);
                            EnterpriseFactory.Instance.Experts.Add(exp);
                        }
                        foreach (XmlNode child in rootNode.SelectSingleNode("Risks"))
                        {
                            Risk rsk = new Risk(0);
                            rsk.DeserializeProperties(child);
                            EnterpriseFactory.Instance.Risks.Add(rsk);
                        }
                        foreach (XmlNode child in rootNode.SelectSingleNode("Assessments"))
                        {
                            Assessment ass = new Assessment(0);
                            ass.DeserializeProperties(child);
                            EnterpriseFactory.Instance.Assessments.Add(ass);
                        }

                    }
                    catch
                    { }
                    
                    //foreach (XmlNode tagNode in rootNode.SelectNodes("Tag"))
                    //{
                    //    if (!(tagNode is XmlElement))
                    //        continue;
                    //    ScadaDeviceUpdateQuery query = new ScadaDeviceUpdateQuery();
                    //    query.ScadaGuid = Shared.Helpers.Xml_Helper.GetValueGuid(tagNode, "Guid").Value;
                    //    query.DeserializeProperties(tagNode);

                    //    Add(query);
                    //}
                }
            }
        }
    }
}
