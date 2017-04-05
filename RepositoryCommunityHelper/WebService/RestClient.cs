using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryCommunityHelper.WebService
{
    public class RestClient :IDisposable, IService
    {
        private readonly ConnectionProperties _connectionProperties;
        //public delegate string ResponseCompleted(string url);
        /* private PlayerService playerService;

         public PlayerService PlayerService
         {
             get { return playerService; }
         }
         */
        /*public ResourceService ResourceService
        {
            get { return _resourceService; }
        }

        private ResourceService _resourceService;
        */
        public RestClient(ConnectionProperties connectionProperties)
        {

            _connectionProperties = connectionProperties;
            //playerService = new PlayerService(_connectionProperties);
            //_resourceService = resourceService;
        }


        public string DoGet(string url)
        {
            //HttpWebRequest request =
            //(HttpWebRequest) WebRequest.Create(this._connectionProperties.UrlServer + "player/" + "?_type=json");
             HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(this._connectionProperties.UrlServer + url + "/?_type=json");
            
                
            
            
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(_connectionProperties.SCookieCollection);
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            //if (!string.IsNullOrEmpty(_connectionProperties.SCookies))
            //    request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);

            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //System.Net.ServicePointManager.Expect100Continue = false;
            _connectionProperties.SCookieCollection.Add(response.Cookies);
            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Player>));
            //List<Player> players = ((List<Player>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()))));
            response.Close();
            return reader.ReadToEnd();
        }

        public Task<string> DoGetAsync(string url)
        {
            return Task.Factory.StartNew(() =>
            {
                //Thread.Sleep(1000);
                //System.Threading.Thread.Sleep(10000);
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(this._connectionProperties.UrlServer + url + "/?_type=json");
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(_connectionProperties.SCookieCollection);
                request.ContentType = "application/json;charset=UTF-8";
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                //if (!string.IsNullOrEmpty(_connectionProperties.SCookies))
                //    request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);

                string result = "";
                request.AllowAutoRedirect = false;
                try
                {
                    var response = (HttpWebResponse)request.GetResponse();
                    //request.BeginGetResponse(new AsyncCallback(OnResponse), request);
                    //System.Net.ServicePointManager.Expect100Continue = false;
                    _connectionProperties.SCookieCollection.Add(response.Cookies);
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Player>));
                    //List<Player> players = ((List<Player>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd()))));
                    result = reader.ReadToEnd();
                    response.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = null;
                    //throw;
                }
                
                return result;
            });
        }

        public ConnectionProperties Create()
        {
            return this._connectionProperties;
        }

        public RestClient CreateRequest()
        {
            return this;
        }

        public void Dispose()
        {

            OnDispose();
        }

        protected virtual void OnDispose()
        {
            //throw new NotImplementedException("Dispose");
        }

        protected virtual void Dispose(bool disposing)
        {
            
        }
    }
}
