﻿using System;
using System.Windows;

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
            this.wpfWindow.Close();
            //base.OnClosed(new EventArgs());
        }

        public virtual IWindow CreateChild(object viewModel)
        {
            var cw = new MainWindow();
            cw.Owner = this.wpfWindow;
            cw.DataContext = viewModel;
            WindowAdapter.ConfigureBehavior(cw);
            return new WindowAdapter(cw);
        }

        public virtual void Show()
        {
            this.wpfWindow.Show();
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

        private static void ConfigureBehavior(MainWindow cw)
        {
            cw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //cw.CommandBindings.Add(new CommandBinding(PresentationCommands.Accept, (sender, e) => cw.DialogResult = true));
        }
    }
}
