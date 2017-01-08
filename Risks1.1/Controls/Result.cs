using System.Xml;
using WpfApplication1.Common;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    public class Result : ObjectBase
    {
        #region .ctors
        public Result(int id)
        {
            ID = id;
        }
        public Result(int id, int Imp,  int ExtRate, int ProtRate, int InfProb)
        {
            ID = id;
            m_Importance = Imp;
            m_InfluenceProb = InfProb;
            m_ExternalRate = ExtRate;
            m_ProtectionRate = ProtRate;
        }
        #endregion

        #region Properties
        
        private bool m_IsEnabled = true;
        public bool IsEnabled
        {
            get { return m_IsEnabled; }
            set
            {
                m_IsEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
       
        private int m_Importance = 0;
        public int Importance
        {
            get { return m_Importance; }
            set
            {
                m_Importance = value;
                OnPropertyChanged("Importance");
            }
        }
        private int m_InfluenceProb = 0;
        public int InfluenceProb
        {
            get { return m_InfluenceProb; }
            set
            {
                m_InfluenceProb = value;
                OnPropertyChanged("Influence");
            }
        }
        
        private int m_ExternalRate = 0;
        public int ExternalRate
        {
            get { return m_ExternalRate; }
            set
            {
                m_ExternalRate = value;
                OnPropertyChanged("ExternalRate");
            }
        }
        private int m_ProtectionRate = 0;
        public int ProtectionRate
        {
            get { return m_ProtectionRate; }
            set
            {
                m_ProtectionRate = value;
                OnPropertyChanged("ProtectionRate");
            }
        }
        #endregion
        #region Methods
        #endregion

        #region Binary
        public virtual void SerializeProperties(XmlNode propertiesNode)
        {
            var xDoc = propertiesNode.OwnerDocument;
            Xml_Helper.AddValue(propertiesNode, "ID", ID);
            Xml_Helper.AddValue(propertiesNode, "Importance", Importance);
            Xml_Helper.AddValue(propertiesNode, "Influence", InfluenceProb);
            Xml_Helper.AddValue(propertiesNode, "ExtRate", ExternalRate);
            Xml_Helper.AddValue(propertiesNode, "ProtDegr", ProtectionRate);
        }
        public virtual void DeserializeProperties(XmlNode propertiesNode)
        {
            ID = Xml_Helper.GetValueInt(propertiesNode, "ID", 0).Value;
            Importance = Xml_Helper.GetValueInt(propertiesNode, "Importance", 0).Value;
            InfluenceProb = Xml_Helper.GetValueInt(propertiesNode, "Influence", 0).Value;
            ExternalRate = Xml_Helper.GetValueInt(propertiesNode, "ExtRate", 0).Value;
            ProtectionRate = Xml_Helper.GetValueInt(propertiesNode, "ProtDegr", 0).Value;
        }
        #endregion


    }
}
