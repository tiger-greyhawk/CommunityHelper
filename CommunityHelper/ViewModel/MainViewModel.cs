using ViewCommunityHelper.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RepositoryCommunityHelper.Repository;

namespace CommunityHelper.ViewModel
{
    public class MainViewModel
    {

        private readonly IRepository repository;
        private readonly IWindow window;

        private readonly ObservableCollection<RequestResourceViewModel> requestResourceVM;
        private readonly ObservableCollection<PlayerViewModel> playerVM;


        public MainViewModel(IRepository repository, IWindow window)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            this.repository = repository;
            this.window = window;

            this.requestResourceVM = new ObservableCollection<RequestResourceViewModel>();
            this.playerVM = new ObservableCollection<PlayerViewModel>();
            //requestResourceVM = repository.SelectAllRequestsResources();
            Refresh(null);
            //MessageBox.Show("", "");
        }


        public ObservableCollection<RequestResourceViewModel> RequestResourceVM
        {
            get { return this.requestResourceVM; }
        }

        public ObservableCollection<PlayerViewModel> PlayerVM
        {
            get { return this.playerVM; }
        }

        private void Refresh(object parameter)
        {
            /*TODO
             * Раскоментировать и реализовать маппер из DTO во ViewModel
             */
             
            this.requestResourceVM.Clear();
            //repository.GetAllRes();
            repository.RefreshRequestsResources();
            //foreach (var rR in this.repository.SelectAllRequestsResources())
            foreach (var rR in this.repository.GetRequestResourceDtos())
            {
                RequestResourceViewModel rrvm = new RequestResourceViewModel();
                rrvm.Timestamp = rR.Timestamp;
                rrvm.Id = rR.Id;
                this.requestResourceVM.Add(rrvm);
            }

            this.playerVM.Clear();
            //repository.SelectAllPlayers();
            repository.RefreshPlayers();
            /*
            foreach (var p in this.repository.SelectAllPlayers())
            {
                this.playerVM.Add(p);
            }
            */
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
