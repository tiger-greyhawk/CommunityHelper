using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ViewCommunityHelper.View.WindowXaml
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private bool _connectCheck = false;

        public bool ConnectCheck
        {
            get { return _connectCheck; }
            set { _connectCheck = value; }
        }

        public Main()
        {
            InitializeComponent();
        }

        private void ConnectImage_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_connectCheck)
            {
                
                //ConnectImage.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/connect.png"));
                //_connectCheck = false;
            }
            else
            {
                //ConnectImage.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/disconnect.png"));
                //_connectCheck = true;
            }
        }

/*        private void InitCommands()
        {
            CommandBinding bind = new CommandBinding(PresentationCommands.ShowGameFunctionalWindowCommand);

            // Присоединение обработчика событий
            bind.Executed += AddRRCommand_Executed;

            // Регистрация привязки
            this.CommandBindings.Add(bind);
        }*/

/*        private void AddRRCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            //MessageBox.Show("Команда запущена из " + e.Source.ToString());
        }*/

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //IRequestsResourcesManagmentClientContainer container;
            //container = new RequestsResourcesManagmentClientContainer();
            //container.ResolveMainWindow().Show();
        }


    }
}
