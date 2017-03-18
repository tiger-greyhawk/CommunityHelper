using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ViewCommunityHelper.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //public MainViewModel mainModel;

        public MainWindow()
        {

            InitializeComponent();
            //ResVM = new ResourcesViewModel();
            //mainModel = new MainViewModel();

            //RequestsResources.DataContext = ResVM;
            //ResVM.ResourceModelInstance[0].PlayerNick = "Tiger2";
            //DataContext = "{Binding Path=ResVM}"
        }



        /*        public Window ResolveWindow()
                {
                    Window window = new Window();
                    return window;
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
                */
    }
}
