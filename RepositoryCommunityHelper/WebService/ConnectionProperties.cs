using System;
using System.Collections.ObjectModel;
using System.Net;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;


namespace RepositoryCommunityHelper.WebService
{
    public class ConnectionProperties :BaseMagic
    {
        private string _serverAddres;// = "192.168.10.25";
        private string _serverPort;// = "8080";
        private string _login;// = "test222";
        private string _password;// = "test";
        private string _subServer;// = "domination";
        public CookieCollection SCookieCollection { get; set; }
        //private static string server = "http://192.168.10.110:8080/backend/";
        //private static string server = "http://91.122.191.190:8181/backend/";
        //private static string server = "http://localhost:8080/backend/";
        //private static string server = "http://192.168.10.25:8080/backend/";
        //private static string server = "http://"+_serverAddres+":"+_serverPort+"/api/";
        private string _urlServer;// = "http://" + _serverAddres + ":" + _serverPort + "/" + _subServer + "/";
        //private static string server = "http://shold.tk:8181/backend/";
        //static HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "_login");
        private string _sCookies;
        public ServicePoint spSite;
        public string ConnectionState;
        private bool _connected = false;


        private PlayerDto _activePlayer;
        private User _activeUser;
        //private ObservableCollection<Player> MyPlayers;


        public PlayerDto ActivePlayer
        {
            get; set; }

        public User ActiveUser { get; set; }

        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; }
        }


        /*private string _pathToRest;
        public string PathToRest
        {
            get
            {
                switch (_pathToRest)
                {
                    case "player":
                        _pathToRest = "";
                        break;
                    case "resource":
                        _pathToRest = "";
                        break;
                    default:
                        _pathToRest = "";
                        break;
                }
                return _pathToRest;
            }
            set { _pathToRest = value; }
        }
*/

        public ConnectionProperties()
        {
            //_serverAddres = "192.168.10.25";
            _serverAddres = Properties.Settings.Default.ServerAddres;
            //_serverPort = "8080";
            _serverPort = Properties.Settings.Default.ServerPort;
            //_login = "test222";
            _login = Properties.Settings.Default.Login;
            //_password = "test";
            _password = Properties.Settings.Default.Password;
            //_subServer = "domination";
            _subServer = Properties.Settings.Default.SubServer;
            _urlServer = "http://" + _serverAddres + ":" + _serverPort + "/" + _subServer + "/";
            SCookieCollection = new CookieCollection();
            //this._sCookies = _sCookies;
            //Uri siteUri = new Uri(_urlServer);
            //spSite = ServicePointManager.FindServicePoint(siteUri);
            //spSite.MaxIdleTime = 600000;
        }

        public void Save(ConnectionProperties con)
        {
            Properties.Settings.Default.ServerAddres = con.ServerAddres;
            Properties.Settings.Default.ServerPort = con.ServerPort;
            Properties.Settings.Default.SubServer = con.SubServer;
            Properties.Settings.Default.Login = con.Login;
            Properties.Settings.Default.Password = con.Password;
            Properties.Settings.Default.Save();
            ServerAddres = con.ServerAddres;
            ServerPort = con.ServerPort;
            SubServer = con.SubServer;
            Login = con.Login;
            Password = con.Password;
            UrlServer = "http://" + _serverAddres + ":" + _serverPort + "/" + _subServer + "/";
        }

        public string ServerAddres
        {
            get
            {
                return _serverAddres;
            }

            set
            {
                _serverAddres = value;
            }
        }

        public string ServerPort
        {
            get
            {
                return _serverPort;
            }

            set
            {
                _serverPort = value;
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }

            set
            {
                _login = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public string SubServer
        {
            get
            {
                return _subServer;
            }

            set
            {
                _subServer = value;
            }
        }

        public string UrlServer
        {
            get
            {
                return _urlServer;
            }

            set
            {
                _urlServer = value;
            }
        }

        public string SCookies
        {
            get
            {
                return _sCookies;
            }

            set
            {
                _sCookies = value;
            }
        }
    }
}
