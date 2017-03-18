using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RepositoryCommunityHelper
{
    public class BaseClass
    {
        protected BaseClass()
        {

        }
        //public virtual string DisplayName { get; protected set; } 

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnDispose()
        {
        }

        public void Dispose()
        {
            OnDispose();
        }
    }
}
