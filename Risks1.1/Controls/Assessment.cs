using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using WpfApplication1.Common;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    public class Assessment : ObjectBase
    {
        #region .ctors
        public Assessment(int id)
        {
            ID = id;
        }
        public Assessment(int id, int ExpID, int RiskId, int Prob, int InfProb, int Dang, int ExtRate, int ProtDegr)
        {
            ID = id;
            m_ExpertID = ExpID;
            m_RiskID = RiskId;
            m_Probability = Prob;
            m_InfluenceProb = InfProb;
            m_Danger = Dang;
            m_ExternalRate = ExtRate;
            m_ProtectionDegree = ProtDegr;
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
        private int m_ExpertID = 0;
        public int ExpertID
        {
            get { return m_ExpertID; }
            set
            {
                m_ExpertID = value;
                OnPropertyChanged("ExpertID");
            }
        }
        private int m_RiskID = 0;
        public int RiskID
        {
            get { return m_RiskID; }
            set
            {
                m_RiskID = value;
                OnPropertyChanged("ExpertID");
            }
        }
        private int m_Probability = 0;
        public int Probability
        {
            get { return m_Probability; }
            set
            {
                m_Probability = value;
                OnPropertyChanged("Probability");
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
        private int m_Danger = 0;
        public int Danger
        {
            get { return m_Danger; }
            set
            {
                m_Danger = value;
                OnPropertyChanged("Danger");
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
        private int m_ProtectionDegree = 0;
        public int ProtectionDegree
        {
            get { return m_ProtectionDegree; }
            set
            {
                m_ProtectionDegree = value;
                OnPropertyChanged("ProtectionDegree");
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
            Xml_Helper.AddValue(propertiesNode, "Expert", ExpertID);
            Xml_Helper.AddValue(propertiesNode, "Risk", RiskID);
            Xml_Helper.AddValue(propertiesNode, "Probability", Probability);
            Xml_Helper.AddValue(propertiesNode, "Influence", InfluenceProb);
            Xml_Helper.AddValue(propertiesNode, "Danger", Danger);
            Xml_Helper.AddValue(propertiesNode, "ExtRate", ExternalRate);
            Xml_Helper.AddValue(propertiesNode, "ProtDegr", ProtectionDegree);
        }
        public virtual void DeserializeProperties(XmlNode propertiesNode)
        {
            ID = Xml_Helper.GetValueInt(propertiesNode, "ID", 0).Value;
            ExpertID = Xml_Helper.GetValueInt(propertiesNode, "Expert", 0).Value;
            RiskID = Xml_Helper.GetValueInt(propertiesNode, "Risk", 0).Value;
            Probability = Xml_Helper.GetValueInt(propertiesNode, "Probability", 0).Value;
            InfluenceProb = Xml_Helper.GetValueInt(propertiesNode, "Influence", 0).Value;
            Danger = Xml_Helper.GetValueInt(propertiesNode, "Danger", 0).Value;
            ExternalRate = Xml_Helper.GetValueInt(propertiesNode, "ExtRate", 0).Value;
            ProtectionDegree = Xml_Helper.GetValueInt(propertiesNode, "ProtDegr", 0).Value;
        }
        #endregion


    }
}
