using System.Windows;
using CommunityHelper.Container;



namespace CommunityHelper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        //RequestsResourcesManagmentClientContainer container;
        private MainContainer mainContainer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool doShutDown = false;

            if (doShutDown)
            {
                Shutdown(1);
                return;
            }
            else
            {
                //var container =
                //container = new RequestsResourcesManagmentClientContainer();
                //container.ResolveWindow().Show();

                mainContainer = new MainContainer();
                mainContainer.ResolveWindow().Show();

            }
        }

        /*protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //container.Close();
            //container.ResolveWindow().Close();
        }*/
    }
}
