using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using WpfApplication1.Common;
using WpfApplication1.Controls;

namespace WpfApplication1
{
    public class Expert : ObjectBase
    {
        #region .ctors
        public Expert(int id)
        {
            ID = id;
        }
        public Expert(int id, String name)
        {
            ID = id;
            m_Name = name;
        }
        #endregion

        #region Properties



        private String m_Name;
        public String Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Binary
        public virtual void SerializeProperties(XmlNode propertiesNode)
        {
            var xDoc = propertiesNode.OwnerDocument;
            Xml_Helper.AddValue(propertiesNode, "ID", ID);
            Xml_Helper.AddValue(propertiesNode, "Name", Name);
        }
        public virtual void DeserializeProperties(XmlNode propertiesNode)
        {
            ID = Xml_Helper.GetValueInt(propertiesNode, "ID", 0).Value;
            Name = Xml_Helper.GetValueStr(propertiesNode, "Name");
        }
        #endregion



    }
}
