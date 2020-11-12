using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Proj.Models;
using Proj.Utils;

namespace Proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly string _username;
        private readonly string _credentials;

        public HomeController(ILogger<HomeController> logger, IConfiguration conf)
        {
            _logger = logger;
            _config = conf;
            _credentials = _config["Github:PAT"];
            _username = _config["Github:Username"];
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/User")]
        public IActionResult GetUser()
        {
            var user = new GetUser(_credentials, _username);
            return Json(user.Run());
        }

        [HttpGet]
        [Route("/api/Repos")]
        public IActionResult GetRepos()
        {
            var repos = new GetRepos(_credentials, _username);
            return Json(repos.Run());
        }

        [HttpGet]
        [Route("/api/Commits")]
        public IActionResult GetCommits(string repo)
        {
            var commits = new GetCommits(repo, _username, _credentials);
            return Json(commits.Run());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}