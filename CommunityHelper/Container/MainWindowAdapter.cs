using System;
using System.Windows;
using System.Windows.Input;
using ViewCommunityHelper.View;
using CommunityHelper.ViewModel;
using RepositoryCommunityHelper.WebService;

namespace CommunityHelper.Container
{
    public class MainWindowAdapter : WindowAdapter
    {
        private readonly IViewModelFactory vmFactory;
        private bool initialized;
        public Window _WpfWindow;
        //private ConnectionProperties _connectionProperties;
        //public Auth Auth;
        

        public MainWindowAdapter(Window wpfWindow, IViewModelFactory viewModelFactory)//, ConnectionProperties con)
            : base(wpfWindow)
        {
            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }
            //_connectionProperties = con;
            this._WpfWindow = wpfWindow;
            this.vmFactory = viewModelFactory;
            //this.Auth = auth;
            //EnsureInitialized();
        }

        /*public void UpdateConnectionProp(ConnectionProperties con)
        {
            _connectionProperties = con;
        }*/

        #region IWindow Members

        public override void Close()
        {
            //this.EnsureInitialized();
            base.Close();
            initialized = false;
        }

        public override IWindow CreateChild(object viewModel)
        {
            this.EnsureInitialized();
            return base.CreateChild(viewModel);
        }

        public override void Show()
        {
            this.EnsureInitialized();
            //this.ShowActivated = false;
            base.Show();
            //WpfWindow.Activate();

        }

        public override bool? ShowDialog()
        {
            this.EnsureInitialized();
            return base.ShowDialog();
        }

        #endregion

        //private void DeclareKeyBindings(MainWindowViewModel vm)
        //{
        //    this.WpfWindow.InputBindings.Add(new KeyBinding(vm.RefreshCommand, new KeyGesture(Key.F5)));
        //    this.WpfWindow.InputBindings.Add(new KeyBinding(vm.InsertProductCommand, new KeyGesture(Key.Insert)));
        //    this.WpfWindow.InputBindings.Add(new KeyBinding(vm.EditProductCommand, new KeyGesture(Key.Enter)));
        //    this.WpfWindow.InputBindings.Add(new KeyBinding(vm.DeleteProductCommand, new KeyGesture(Key.Delete)));
        //}

        private void EnsureInitialized()
        {
            if (this.initialized)
            {
                
                return;
            }

            var vm = this.vmFactory.Create(this);
            this.WpfWindow.DataContext = vm;
            //this.DeclareKeyBindings(vm);

            /*var myCommandBinding = new CommandBinding(
                    PresentationCommands.ShowGameFunctionalWindow,
                    vm.AddRR_Executed,
                    vm.AddRR_CanExecute);*/
            //CommandBinding bind = new CommandBinding(PresentationCommands.ShowGameFunctionalWindow);

            // Присоединение обработчика событий

            // Регистрация привязки
            //CommandBindings.Add(bind);
            WpfWindow.CommandBindings.Add(new CommandBinding(PresentationCommands.ShowGameFunctionalWindowCommand, vm.ShowGameFunctionalWindow ));
            WpfWindow.CommandBindings.Add(new CommandBinding(PresentationCommands.ShowFactionsWindowCommand, vm.ShowFactionsWindow));
            WpfWindow.CommandBindings.Add(new CommandBinding(PresentationCommands.Exit, vm.Exit));
            //WpfWindow.CommandBindings.Add(new CommandBinding(PresentationCommands.Connect, vm.Connect));
            //CommandBindings.Add(bind);
            //CommandManager.RegisterClassCommandBinding(typeof(PresentationCommands), bind);
            this.initialized = true;
        }
    }
}
