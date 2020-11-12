using Proj.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Proj.Utils
{
    public class GetUser
    {
        private string _userUrl = "https://api.github.com/user";
        private string _credentials;
        private string _username;

        public GetUser(string credentials, string username)
        {
            _credentials = credentials;
            _username = username;
        }

        public User Run()
        {
            var jsonResponse = SendRequest();
            if (jsonResponse == null)
            {
                return null;
            }
            var user = ProcessJson(jsonResponse);
            return user;
        }

        private User ProcessJson(string jsonResponse)
        {
            var parser = JObject.Parse(jsonResponse);
            return new User
            {
                Name = parser["name"].ToString(),
                Company = parser["company"].ToString(),
                Email = parser["email"].ToString(),
                Location = parser["location"].ToString(),
                Img = parser["avatar_url"].ToString()
            };
        }

        private string SendRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_userUrl);
            request.Headers.Add("Authorization", "token " + _credentials);
            request.UserAgent = _username;
            request.Accept = "application/json";

            string jsonString = null;
            try
            {
                using WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            catch
            {
                return null;
            }

            return jsonString;
        }
    }
}