using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Controls
{
    public class ObjectBase: INotifyPropertyChanged
    {
        private int m_ID = 0;
        public int ID
        {
            get { return m_ID; }
            set
            {
                m_ID = value;
                OnPropertyChanged("ID");
            }
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
