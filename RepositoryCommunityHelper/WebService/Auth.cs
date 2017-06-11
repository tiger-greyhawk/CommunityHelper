using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;


namespace RepositoryCommunityHelper.WebService
{
    public class Auth
    {
        private ConnectionProperties _connectionProperties;
        private HttpWebRequest _request;
        private User _authenticatedUser;


        public User AuthenticatedUser
        {
            get { return _connectionProperties.ActiveUser; }
            set { _connectionProperties.ActiveUser = value; }
        }

        public Auth(ConnectionProperties connectionProperties)
        {
            this._connectionProperties = connectionProperties;
        }

/*        public Task<string> DoAuthAsync()
        {
            return Task.Factory.StartNew(() =>
                {
                    return DoAuth();
                }
            );
        }*/

        public ConnectionProperties ConnectionProperties
        {
            get { return _connectionProperties; }
        }

        public HttpWebRequest SetBasicAuthHeader(HttpWebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            return request;
        }

        public void UnAuth()
        {
            _request.Abort();
        }

        public string DoAuthNotWorkingAsync()
        {
            WebClient webClient = new WebClient();
            //webClient.
            return null;
        }

        /// <summary>
        /// Никакой это не Async. Мы блокируем вызовом Result основной поток. Асинхронность реализовывать надо во вьюмоделях через Dispatcher.
        /// На самом деле только проверка на правильность логина и пароля. Авторизация Basic - каждый раз шлется логин:пароль
        /// </summary>
        /// <returns></returns>
        public Task<string> DoAuthAsyncNew()
        {
            string server = _connectionProperties.UrlServer;
            return Task.Factory.StartNew(() =>
            {
                string result = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server + "user/me?_type=json");
                SetBasicAuthHeader(request, _connectionProperties.Login, _connectionProperties.Password);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    var response = request.GetResponse();
                    Stream data = (Stream)response.GetResponseStream();
                    StreamReader reader = new StreamReader(data, Encoding.UTF8);
                    result = reader.ReadToEnd();
                    ConnectionProperties.Connected = true;
                    response.Close();
                    data.Close();
                    reader.Close();
                }
                catch (WebException e)
                {
                    if (((HttpWebResponse) e.Response).StatusCode == HttpStatusCode.Unauthorized)
                        result = "401";
                    ConnectionProperties.Connected = false;
                    Console.WriteLine(e);
                    //throw;
                }
                
                return result;
            });
        }


        
        public Task<string> DoAuthAsyncTest()
        {
            string server = _connectionProperties.UrlServer;
            return Task.Factory.StartNew(() =>
            {
                string result = "";

                var tempEmail = _connectionProperties.Login;
                var tempPass = _connectionProperties.Password;

                HttpWebRequest tokenRequest = (HttpWebRequest) WebRequest.Create(server + "login");
                tokenRequest.CookieContainer = new CookieContainer();
                tokenRequest.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);
                tokenRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
                tokenRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                tokenRequest.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                tokenRequest.ContentType = "application/x-www-form-urlencoded";
                tokenRequest.Method = "POST";
                //string token = ((HttpWebResponse) tokenRequest.GetResponse()).Cookies["csrftoken"].ToString().Split('=')[1];
                //if (!String.IsNullOrEmpty(_connectionProperties.SCookies)) tokenRequest.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);

                String postData = ("username=" + _connectionProperties.Login + "&password=" + _connectionProperties.Password + "&submit=Login");
                Encoding encoding = Encoding.UTF8;
                byte[] postDataByte = encoding.GetBytes(postData);

                Stream st = tokenRequest.GetRequestStream();
                st.Write(postDataByte, 0, postDataByte.Length);
                st.Close();
                
                //tokenRequest.Address = new Uri(server + "login");
                HttpWebResponse response = (HttpWebResponse)tokenRequest.GetResponse();



                //string token = ((HttpWebResponse)tokenRequest.GetResponse()).Cookies["Set-Cookie"].ToString().Split('=')[1];
                //string token = response.Cookies["SetCookie"].ToString();
                //string token = response.Headers["Set-Cookie"].ToString();
                //string cookies = ((HttpWebResponse)tokenRequest.GetResponse()).Cookies["Set-Cookie"].ToString().Split('=')[1];
                
                HttpWebRequest loginRequest = (HttpWebRequest) WebRequest.Create(server + "user/me");
                //loginRequest.CookieContainer = tokenRequest.CookieContainer;
                var cache = new CredentialCache();
                cache.Add(new Uri(server + "login"), "Digest", new NetworkCredential(tempEmail, tempPass));
                loginRequest.Credentials = cache;
                loginRequest.PreAuthenticate = true;

                loginRequest.Method = "GET";
                loginRequest.CookieContainer = new CookieContainer();
                //loginRequest.CookieContainer.Add(new Cookie("csrftoken", token, "/", "carkit.kg"));

                /*String postData = ("username=" + _connectionProperties.Login + "&password=" + _connectionProperties.Password + "&submit=Login");
                Encoding encoding = Encoding.UTF8;
                byte[] postDataByte = encoding.GetBytes(postData);*/

                //byte[] data = Encoding.UTF8.GetBytes("username=" + _connectionProperties.Login + "&password=" + _connectionProperties.Password + "&submit=Login");
                //loginRequest.ContentLength = data.Length;
                loginRequest.CookieContainer = tokenRequest.CookieContainer;
                //loginRequest.Timeout = 3000;
                //loginRequest.Headers.Add("Authorization", "Basic " + System.Convert.ToBase64String( System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tempEmail + ":" + tempPass)));
                //loginRequest.GetRequestStream().Write(data, 0, data.Length);
                //Debug.Log(loginRequest.Headers.ToString());
                // вот здесь выкидывает request timed out
                loginRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                loginRequest.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
                loginRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse authResponse = (HttpWebResponse) loginRequest.GetResponse();
                //Debug.Log(authResponse.ResponseUri);
                return result;
            });
        }

        public string DoAuth()
        {
            string server = _connectionProperties.UrlServer;
            ///*
            _request = (HttpWebRequest)WebRequest.Create(server + "login");
            _request.ContentType = "application/x-www-form-urlencoded";
            _request.Method = "GET";
            _request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";

            _request.PreAuthenticate = true;
            //*/
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            String postData = ("username=" + _connectionProperties.Login + "&password=" + _connectionProperties.Password + "&submit=Login");
            Encoding encoding = Encoding.UTF8;
            byte[] postDataByte = encoding.GetBytes(postData);

            String authInfo = ("username=" + _connectionProperties.Login + "&password=" + _connectionProperties.Password + "&submit=Login");
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            _request.Headers["Authorization"] = "Basic " + authInfo;

            System.Net.ServicePointManager.Expect100Continue = false;
            ///*
            _request.AllowAutoRedirect = false;
            
            HttpWebResponse response = (HttpWebResponse)_request.GetResponse();


            
            _connectionProperties.SCookieCollection.Add(response.Cookies);
            _connectionProperties.SCookies = String.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
            //_connectionProperties.ConnectionState = String.IsNullOrEmpty(response.Headers["Connection"]) ? "" : response.Headers["Connection"];
            response.Close();
            response = null;
            _request = null;
            //*/
            _request = (HttpWebRequest)WebRequest.Create(server + "login");
            if (!String.IsNullOrEmpty(_connectionProperties.SCookies)) _request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);




            _request.PreAuthenticate = true;
            var credentials = new NetworkCredential(_connectionProperties.Login, _connectionProperties.Password);
            _request.Credentials = credentials;
            _request.CookieContainer = new CookieContainer();
            //request.AllowAutoRedirect = false;
            //request.AllowAutoRedirect = false;
            _request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            _request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            _request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            //request.Headers.Add("Accept-Encoding", "gzip, deflate");
            _request.ContentType = "application/x-www-form-urlencoded";

            //request.ContentType = "application/json;charset=UTF-8";
            _request.Method = "POST";
            _request.ContentLength = postDataByte.Length;
            
            Stream st = _request.GetRequestStream();
            st.Write(postDataByte, 0, postDataByte.Length);
            st.Close();
            //st.Write(postDataByte, 0, postDataByte.Length);
            HttpWebResponse response2 = (HttpWebResponse)_request.GetResponse();
            
            /*try
            {
                 response2 = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }*/
            /*
            _connectionProperties.SCookieCollection = response2.Cookies;
            _connectionProperties.SCookies = String.IsNullOrEmpty(response2.Headers["Set-Cookie"]) ? "" : response2.Headers["Set-Cookie"];


            request = (HttpWebRequest)WebRequest.Create(response2.ResponseUri);
            request.CookieContainer = new CookieContainer();
            if (!String.IsNullOrEmpty(_connectionProperties.SCookies)) request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);
            request.Method = "POST";
            request.ContentLength = postDataByte.Length;
            Stream st2 = request.GetRequestStream();
            st2.Write(postDataByte, 0, postDataByte.Length);
            st2.Close();
            */

            _connectionProperties.SCookieCollection = response2.Cookies;
            _connectionProperties.SCookies = String.IsNullOrEmpty(response2.Headers["Set-Cookie"]) ? "" : response2.Headers["Set-Cookie"];
            //_connectionProperties.ConnectionState = String.IsNullOrEmpty(response.Headers["Connection"]) ? "" : response.Headers["Connection"];
            _connectionProperties.ConnectionState = String.IsNullOrEmpty(_request.Connection) ? "" : _request.Connection;


            Stream data = (Stream)response2.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            string temp = reader.ReadToEnd();
            //st.Close();
            response2.Close();
            //if (_connectionProperties.SCookieCollection.Count > 0) _connectionProperties.Connected = true;
            //else _connectionProperties.Connected = false;
            ///*TODO херовая заглушка-проверка на авторизацию. Пока так, а в дальнейшем надо делать нормальный редирект на сервере: http://www.baeldung.com/spring_redirect_after_login 
            /// 
            //if (temp == "") _connectionProperties.Connected = true;  
            ConverterJson converterJson = new ConverterJson();
            User user = new User();
            try
            {
                user = converterJson.ConvertJsonToUser(temp);
            }
            catch (Exception e)
            {
                
            }
            if (user.Username != null) _connectionProperties.Connected = true;
            
            return temp;
        }


        public string GetMe()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_connectionProperties.UrlServer + "user/me?_type=json");
            _request = (HttpWebRequest)WebRequest.Create(_connectionProperties.UrlServer + "user/me?_type=json");
            _request.ContentType = "application/json;charset=UTF-8";
            _request.Method = "GET";
            _request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(_connectionProperties.SCookies)) _request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);
            //request.CookieContainer.Add(cookieCollection);


            _request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                //System.Net.ServicePointManager.Expect100Continue = false;

                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
                //return (User)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd())));
                return temp;
            }
            catch
            {
                return null;
            }
        }

        public string GetMeBasicAuth()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_connectionProperties.UrlServer + "user/me?_type=json");
            _request = (HttpWebRequest)WebRequest.Create(_connectionProperties.UrlServer + "user/me?_type=json");
            SetBasicAuthHeader(_request, _connectionProperties.Login, _connectionProperties.Password);
            _request.Method = "GET";
            _request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(_connectionProperties.SCookies)) _request.Headers.Add(HttpRequestHeader.Cookie, _connectionProperties.SCookies);
            //request.CookieContainer.Add(cookieCollection);


            _request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                //System.Net.ServicePointManager.Expect100Continue = false;

                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
                //return (User)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd())));
                return temp;
            }
            catch
            {
                return null;
            }
        }
    }
}
