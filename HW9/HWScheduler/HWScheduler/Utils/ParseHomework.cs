using HWScheduler.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWScheduler.Utils
{
    public class ParseHomework
    {
        public Homework GetHomework(string hw, out List<string> ids)
        {
            var data = JObject.Parse(hw);
            var newHw = new Homework
            {
                ClassId = int.Parse(data["Class"].ToString()),
                Precedence = int.Parse(data["Priority"].ToString()),
                Duedate = DateTime.Parse(data["DueDate"].ToString()),
                Title = data["Title"].ToString(),
                Description = data["Note"].ToString()
            };

            ids = data["Tags"].ToString()
                            .Replace('[', ' ')
                            .Replace(']', ' ').Split(',').ToList();


            return newHw;
        }
    }
}
