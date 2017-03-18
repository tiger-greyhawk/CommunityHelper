using System.Collections.ObjectModel;
using System.ComponentModel;
using RepositoryCommunityHelper.Entity;

namespace CommunityHelper.ViewModel
{
    public class RequestsResourcesViewModel
    {
        private ObservableCollection<RequestResource> _resourceModelInstance;

        public RequestsResourcesViewModel()
        {
            _resourceModelInstance = new ObservableCollection<RequestResource>();
            //ResourceModelInstance.Add(new RequestResource() {Timestamp = 0, PlayerNick="Tiger" });
            //ResourceModelInstance.Add(new RequestResource() { Timestamp = 0, PlayerNick = "new Player" });
            //ResourceModelInstance.Add(new RequestResource() { Timestamp = 0, PlayerNick = "Player New" });
        }

        public ObservableCollection<RequestResource> ResourceModelInstance
        {
            get
            {
                return _resourceModelInstance;
            }

            set
            {
                _resourceModelInstance = value;
                OnPropertyChanged("ResourceModelInstance");
            }
        }

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Methods

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
