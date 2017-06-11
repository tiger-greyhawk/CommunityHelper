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
        private ConnectionProperties _connectionProperties;
        private Auth _auth;
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
        //public RestClient(ConnectionProperties connectionProperties)
        public RestClient(Auth auth)
        {
            _auth = auth;
            _connectionProperties = auth.ConnectionProperties;
            //_connectionProperties = connectionProperties;
            //playerService = new PlayerService(_connectionProperties);
            //_resourceService = resourceService;
        }

        public void DoAuth()
        {
            if (!_connectionProperties.Connected)
            {
                _auth.DoAuth();
            }

        }

        public ConnectionProperties ConnectionProperties
        {
            get { return _connectionProperties; }
        }

        public void Update(ConnectionProperties con)
        {
            _connectionProperties = con;
        }
        /*public ConnectionProperties ConnectionProperties
        {
            get { return _connectionProperties; }
        }*/

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

        public HttpWebRequest PrepareRequest(HttpWebRequest request)
        {
            //request.CookieContainer = new CookieContainer();
            //request.CookieContainer.Add(ConnectionProperties.SCookieCollection);
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            return request;
        }

        public HttpWebRequest PrepareBasicAuth(HttpWebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            return request;
        }

/*        public static async Task<JObject> GetJsonAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var jsonString = await client.GetStringAsync(uri);
                return JObject.Parse(jsonString);
            }
        }*/

        public string DoGetAsync(string url)
        {
            return Task.Factory.StartNew(() =>
            {
                string result = "";
                if (ConnectionProperties.Connected)
                    
                    using (this)

                    {
                        //Thread.Sleep(1000);
                        //System.Threading.Thread.Sleep(10000);
                        HttpWebRequest request =  
                           (HttpWebRequest) WebRequest.Create(this.ConnectionProperties.UrlServer + url + "/?_type=json");
                        //request.CookieContainer = new CookieContainer();
                        //request.CookieContainer.Add(ConnectionProperties.SCookieCollection);
                        //request.ContentType = "application/json;charset=UTF-8";
                        //request.Method = "GET";
                        //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                        //if (!string.IsNullOrEmpty(_connectionProperties.SCookies))
                        //    request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);
                        request = PrepareRequest(request);
                        request = PrepareBasicAuth(request, ConnectionProperties.Login, ConnectionProperties.Password);
                    
                        //request.AllowAutoRedirect = false;
                        try
                        {
                            var response = (HttpWebResponse) request.GetResponse();
                            //request.BeginGetResponse(new AsyncCallback(OnResponse), request);
                            System.Net.ServicePointManager.Expect100Continue = false;
                            ConnectionProperties.SCookieCollection.Add(response.Cookies);
                            Stream data = (Stream) response.GetResponseStream();
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
                    }
                return result;
            }).Result;
        }



        public string DoPostAsync(string json, string url)
        {
            //return Task.Factory.StartNew(() =>
            //{
                string result = "";
                if (ConnectionProperties.Connected)
                {
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                    HttpWebRequest request =
                        (HttpWebRequest) WebRequest.Create(this._connectionProperties.UrlServer + url + "?_type=json");
                    //request.ContentType = "application/json;charset=UTF-8";
                    
                    //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                    //request.CookieContainer = new CookieContainer();
                    //request.CookieContainer.Add(_connectionProperties.SCookieCollection);

                    //request.AllowAutoRedirect = false;

                    request = PrepareRequest(request);
                    request = PrepareBasicAuth(request, ConnectionProperties.Login, ConnectionProperties.Password);
                    request.Method = "POST";
                    byte[] body = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = body.Length;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(body, 0, body.Length);
                        stream.Close();
                    }
                    try
                    {
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            Stream data = (Stream)response.GetResponseStream();

                            StreamReader reader = new StreamReader(data, Encoding.UTF8);
                            result = reader.ReadToEnd();
                        }
                    }
                    catch (WebException e)
                    {
                        if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotModified)
                            result = "304";
                    //Console.WriteLine(e);
                    //throw;
                    }
                    /*using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                    {
                        Stream data = (Stream) response.GetResponseStream();
                        
                        StreamReader reader = new StreamReader(data, Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }*/
                }
                return result;
            //});
        }

        public string DoPutAsync(string json, string url)
        {
            //return Task.Factory.StartNew(() =>
            //{
            string result = "";
            if (ConnectionProperties.Connected)
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(this._connectionProperties.UrlServer + url + "?_type=json");
                //request.ContentType = "application/json;charset=UTF-8";

                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                //request.CookieContainer = new CookieContainer();
                //request.CookieContainer.Add(_connectionProperties.SCookieCollection);

                //request.AllowAutoRedirect = false;

                request = PrepareRequest(request);
                request = PrepareBasicAuth(request, ConnectionProperties.Login, ConnectionProperties.Password);
                request.Method = "PUT";
                byte[] body = Encoding.UTF8.GetBytes(json);
                request.ContentLength = body.Length;
                System.Net.ServicePointManager.Expect100Continue = false;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(body, 0, body.Length);
                    stream.Close();
                }
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Stream data = (Stream)response.GetResponseStream();

                        StreamReader reader = new StreamReader(data, Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }
                }
                catch (WebException e)
                {
                    if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotModified)
                        result = "304";
                    //Console.WriteLine(e);
                    //throw;
                }
                /*using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    Stream data = (Stream) response.GetResponseStream();

                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    result = reader.ReadToEnd();
                }*/
            }
            return result;
            //});
        }

        public string DoDeleteAsync(string json, string url)
        {
            //return Task.Factory.StartNew(() =>
            //{
            string result = "";
            if (ConnectionProperties.Connected)
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(this._connectionProperties.UrlServer + url + "?_type=json");
                //request.ContentType = "application/json;charset=UTF-8";

                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                //request.CookieContainer = new CookieContainer();
                //request.CookieContainer.Add(_connectionProperties.SCookieCollection);

                //request.AllowAutoRedirect = false;

                request = PrepareRequest(request);
                request = PrepareBasicAuth(request, ConnectionProperties.Login, ConnectionProperties.Password);
                request.Method = "DELETE";
                byte[] body = Encoding.UTF8.GetBytes(json);
                request.ContentLength = body.Length;
                System.Net.ServicePointManager.Expect100Continue = false;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(body, 0, body.Length);
                    stream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream data = (Stream)response.GetResponseStream();

                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    result = reader.ReadToEnd();
                }
            }
            return result;
            //});
        }

        public string RegisterNewUserAsync(string json, string url)
        {
            //return Task.Factory.StartNew(() =>
            {
                string result = "";
                //if (ConnectionProperties.Connected)
                {
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "pattern/pattern_photo/" + id+"?_type=json");
                    HttpWebRequest request =
                        (HttpWebRequest)WebRequest.Create(this._connectionProperties.UrlServer + url + "?_type=json");
                    request.ContentType = "application/json;charset=UTF-8";
                    request.Method = "POST";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(_connectionProperties.SCookieCollection);

                    request.AllowAutoRedirect = false;
                    byte[] body = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = body.Length;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(body, 0, body.Length);
                        stream.Close();
                    }
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Stream data = (Stream)response.GetResponseStream();
                        StreamReader reader = new StreamReader(data, Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }
                }
                return result;
            }//);
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
