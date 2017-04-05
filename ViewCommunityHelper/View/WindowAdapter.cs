using System;
using System.Windows;
using System.Windows.Input;

namespace ViewCommunityHelper.View
{
    public class WindowAdapter : Window, IWindow
    {
        private readonly Window wpfWindow;

        public WindowAdapter(Window wpfWindow)
        {
            if (wpfWindow == null)
            {
                throw new ArgumentNullException("window");
            }

            this.wpfWindow = wpfWindow;
            
        }

        #region IWindow Members

        public virtual void Close()
        {
            
            //this.wpfWindow.Close();
            base.Close();
            base.OnClosed(EventArgs.Empty);
            //App.Current.Shutdown();
        }

        public virtual IWindow CreateChildByViewModel(object viewModel, Window window)
        {
            window.Owner = this.wpfWindow;
            window.DataContext = viewModel;
            //WindowAdapter.ConfigureBehaviorByVM(window);
            return new WindowAdapter(window);
        }

        public virtual IWindow CreateChild(object viewModel)
        {
            var cw = new RequestResourceEditorWindow();
            cw.Owner = this.wpfWindow;
            cw.DataContext = viewModel;
            WindowAdapter.ConfigureBehavior(cw);
            return new WindowAdapter(cw);
        }

        public virtual void Show()
        {
            this.wpfWindow.Show();
            //wpfWindow.Topmost = false;

            //WpfWindow.Activate();
        }

        public virtual bool? ShowDialog()
        {
            return this.wpfWindow.ShowDialog();
        }

        #endregion

        protected Window WpfWindow
        {
            get { return this.wpfWindow; }
        }

        private static void ConfigureBehavior(RequestResourceEditorWindow cw)
        {
            cw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            cw.CommandBindings.Add(new CommandBinding(PresentationCommands.Accept, (sender, e) => cw.DialogResult = true));
            //cw.CommandBindings.Add(new CommandBinding(PresentationCommands.ShowGameFunctionalWindowCommand, (sender, e) => cw.DialogResult = false));
            //cw.CommandBindings.Add(new CommandBinding(new RoutedCommand("Accept", typeof(RoutedCommand)),
            //    (sender, e) => cw.DialogResult = true));
            // Закоментированная строка не сработает, ибо мы в xaml-коде обращаемся к статик. А Уровень ViewModel там недоступен ))
        }

        private static void ConfigureBehaviorByVM(Window window)
        {
            
        }
    }
}
