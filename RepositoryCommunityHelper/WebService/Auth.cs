using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCommunityHelper.WebService
{
    public class Auth
    {
        private ConnectionProperties connectionProperties;
        private HttpWebRequest request;

        public Auth(ConnectionProperties connectionProperties)
        {
            this.connectionProperties = connectionProperties;
        }

/*        public Task<string> DoAuthAsync()
        {
            return Task.Factory.StartNew(() =>
                {
                    return DoAuth();
                }
            );
        }*/

        public void UnAuth()
        {
            request.Abort();
        }

        public string DoAuth()
        {
            string server = connectionProperties.UrlServer;
            request = (HttpWebRequest)WebRequest.Create(server + "login");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";

            request.PreAuthenticate = true;

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            String postData = ("username=" + connectionProperties.Login + "&password=" + connectionProperties.Password + "&submit=Login");
            Encoding encoding = Encoding.UTF8;
            byte[] postDataByte = encoding.GetBytes(postData);




            request.AllowAutoRedirect = false;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            System.Net.ServicePointManager.Expect100Continue = false;
            connectionProperties.SCookieCollection.Add(response.Cookies);
            connectionProperties.SCookies = String.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
            connectionProperties.ConnectionState = String.IsNullOrEmpty(response.Headers["Connection"])
                ? ""
                : response.Headers["Connection"];
            response.Close();
            request = null;
            request = (HttpWebRequest)WebRequest.Create(server + "login");
            if (!String.IsNullOrEmpty(connectionProperties.SCookies)) request.Headers.Add(HttpRequestHeader.Cookie, connectionProperties.SCookies);

            var credentials = new NetworkCredential(connectionProperties.Login, connectionProperties.Password);
            request.Credentials = credentials;
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.ContentLength = postDataByte.Length;

            Stream st = request.GetRequestStream();
            st.Write(postDataByte, 0, postDataByte.Length);
            //st.Write(postDataByte, 0, postDataByte.Length);
            response = (HttpWebResponse)request.GetResponse();

            connectionProperties.SCookieCollection = response.Cookies;
            connectionProperties.SCookies = String.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
            connectionProperties.ConnectionState = String.IsNullOrEmpty(response.Headers["Connection"])
                ? ""
                : response.Headers["Connection"];


            Stream data = (Stream)response.GetResponseStream();
            StreamReader reader = new StreamReader(data, Encoding.UTF8);
            string temp = reader.ReadToEnd();
            st.Close();
            return temp;
        }

        public string GetMe()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(connectionProperties.UrlServer + "player/me?_type=json");
            request.ContentType = "application/json;charset=UTF-8";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0";
            if (!String.IsNullOrEmpty(connectionProperties.SCookies)) request.Headers.Add(HttpRequestHeader.Cookie, connectionProperties.SCookies);
            //request.CookieContainer.Add(cookieCollection);


            request.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //System.Net.ServicePointManager.Expect100Continue = false;

                Stream data = (Stream)response.GetResponseStream();
                StreamReader reader = new StreamReader(data, Encoding.UTF8);
                string temp = reader.ReadToEnd();
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Player));
                //return (Player)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd())));
                return temp;
            }
            catch
            {
                return null;
            }
        }
    }
}
