using Newtonsoft.Json.Linq;
using Proj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Utils
{
    public class GetCommits
    {
        private string _commitUrl;
        private string _username;
        private string _credentials;

        public GetCommits(string repoName, string username, string credentials)
        {
            _commitUrl = $"https://api.github.com/repos/{username}/{repoName}/commits";
            _username = username;
            _credentials = credentials;
        }

        public IEnumerable<Commit> Run()
        {
            var JsonResponse = SendRequest();
            var commits = new List<Commit>();
            if (JsonResponse == null)
            {
                return null;
            }
            foreach (var commit in ProcessJson(JsonResponse))
            {
                commits.Add(commit);
            }

            return commits;
        }

        private IEnumerable<Commit> ProcessJson(string jsonResponse)
        {
            var parser = JArray.Parse(jsonResponse);
            foreach (var commit in parser)
            {
                yield return new Commit()
                {
                    Sha = commit["sha"].ToString(),
                    AuthorName = commit["commit"]["author"]["name"].ToString(),
                    ShaUrl = commit["commit"]["url"].ToString(),
                    AuthorAvatarUrl = commit["author"]["avatar_url"].ToString(),
                    CommitMessage = commit["commit"]["message"].ToString()
                };
            }
        }

        private string SendRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_commitUrl);
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