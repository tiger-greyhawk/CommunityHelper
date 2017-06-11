using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using CommunityHelper.Container;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.WebService;
using ViewCommunityHelper;


namespace CommunityHelper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// Поддержка смены языков налету взята отсюда: https://habrahabr.ru/post/256193/
    /// </summary>
    public partial class App : Application
    {
        
        //RequestsResourcesManagmentClientContainer container;
        private static MainContainer _mainContainer;

        private static List<CultureInfo> m_Languages = new List<CultureInfo>();
        public static event EventHandler LanguageChanged;

        public static PlayerDto GetActivePlayer()
        {
            return _mainContainer.ConnectionProperties.ActivePlayer;
        }

        public static void UpdateConnectionProperties(ConnectionProperties con)
        {
            _mainContainer.UpdateConnectionProperties(con);
        }

/*        public static object MainContainer
        {
            get { return _mainContainer; }
            set { _mainContainer = (MainContainer)value; }
        }
*/
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

                _mainContainer = new MainContainer();
                _mainContainer.ResolveWindow().Show();
                
            }
        }

        /*protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //container.Close();
            //container.ResolveWindow().Close();
        }*/

        private void Application_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Language = CommunityHelper.Properties.Settings.Default.DefaultLanguage;
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            CommunityHelper.Properties.Settings.Default.DefaultLanguage = Language;
            CommunityHelper.Properties.Settings.Default.Save();
        }


        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            InitializeComponent();
            App.LanguageChanged += App_LanguageChanged;

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("en-US"));
            m_Languages.Add(new CultureInfo("ru-RU"));

            Language = CommunityHelper.Properties.Settings.Default.DefaultLanguage;
        }

        public static CultureInfo Language
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("pack://application:,,,/ViewCommunityHelper;component/Resources/lang.{0}.xaml", value.Name), UriKind.RelativeOrAbsolute);
                        //dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        //dict.Source = new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Resources/lang.ru-RU.xaml");//, UriKind.Relative);
                        break;
                    default:
                        //dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        dict.Source = new Uri(String.Format("pack://application:,,,/ViewCommunityHelper;component/Resources/lang.xaml"), UriKind.RelativeOrAbsolute);
                        break;
                }
                /*
                foreach (var b in App.Current.Resources.MergedDictionaries)
                {
                    var df = b.Source.OriginalString.StartsWith(@"pack://application:,,,/ViewCommunityHelper;component/Resources/lang.");
                }
                */
                //from b in Application.Current.Resources.MergedDictionaries where b.Source
                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                try
                {
                    ResourceDictionary oldDict = (from d in App.Current.Resources.MergedDictionaries
                                                  where d.Source != null && d.Source.OriginalString.Contains("Resources/lang.")
                                                  select d).First();
                    if (oldDict != null)
                    {
                        int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                        Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                        Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                    }
                    else
                    {
                        Application.Current.Resources.MergedDictionaries.Add(dict);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //throw;
                }



                //4. Вызываем евент для оповещения всех окон.
                LanguageChanged(Application.Current, new EventArgs());
            }
        }
    }
}
