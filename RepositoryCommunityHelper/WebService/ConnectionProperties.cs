using System;
using System.Net;


namespace RepositoryCommunityHelper.WebService
{
    public class ConnectionProperties
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
            _serverAddres = "192.168.10.110";
            _serverPort = "8080";
            _login = "test222";
            _password = "test";
            _subServer = "domination";
            _urlServer = "http://" + _serverAddres + ":" + _serverPort + "/" + _subServer + "/";
            SCookieCollection = new CookieCollection();
            //this._sCookies = _sCookies;
            //Uri siteUri = new Uri(_urlServer);
            //spSite = ServicePointManager.FindServicePoint(siteUri);
            //spSite.MaxIdleTime = 600000;
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
