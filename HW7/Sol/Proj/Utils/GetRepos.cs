using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proj.Models;

namespace Proj.Utils
{
    public class GetRepos
    {
        private string _repoUrl = "https://api.github.com/user/repos";
        private string _credentials;
        private string _username;

        public GetRepos(string credentials, string username)
        {
            _credentials = credentials;
            _username = username;
        }

        public IEnumerable<Repo> Run()
        {
            var JsonResponse = SendRequest();
            var repos = new List<Repo>();
            if (JsonResponse == null)
            {
                return null;
            }
            foreach (var repo in ProcessJson(JsonResponse))
            {
                repos.Add(repo);
            }

            return repos;
        }

        private IEnumerable<Repo> ProcessJson(string jsonResponse)
        {
            var parser = JArray.Parse(jsonResponse);
            foreach (var repo in parser)
            {
                yield return new Repo()
                {
                    Name = repo["name"].ToString(),
                    OwnerAvatarUrl = repo["owner"]["avatar_url"].ToString(),
                    PushedAt = repo["pushed_at"].ToString(),
                    OwnerLogin = repo["owner"]["login"].ToString()
                };
            }
        }

        private string SendRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_repoUrl);
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