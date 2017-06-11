using System;
using System.ComponentModel;
using System.Windows.Input;
using RepositoryCommunityHelper.DTO;

namespace CommunityHelper.ViewModel.Internal
{
    public class UpdateCommand : ICommand
    {
        private const int ARE_EQUAL = 0;
        private const int NONE_SELECTED = -1;
        private PlayerViewModelCollection _vm;

        public UpdateCommand(PlayerViewModelCollection viewModel)
        {
            _vm = viewModel;
            _vm.PropertyChanged += vm_PropertyChanged;
        }

        private void vm_PropertyChanged(object sender,
            PropertyChangedEventArgs e)
        {
            if (string.Compare(e.PropertyName,
                               PlayerViewModelCollection.
                               SELECTED_PROJECT_PROPERRTY_NAME)
                == ARE_EQUAL)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_vm.SelectedPlayer == null)
                return false;
            return ((PlayerDto)_vm.SelectedPlayer).Id
                   > NONE_SELECTED;
        }

        public event EventHandler CanExecuteChanged
            = delegate { };

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
            //_vm.UpdatePlayer();
        }
    }
}