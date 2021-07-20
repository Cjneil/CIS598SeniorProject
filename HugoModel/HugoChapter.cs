using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{


    public class HugoChapter
    {
        public List<HugoSection> Sections { get; set; } 

        public string Title { get; }

        public DateTime Date { get; }

        public int Weight { get; }

        public string Pre { get; }

        public List<string> Header { get; }

        public string Path { get; }

        public HugoChapter(string pre, string title, int weight, string path)
        {
            Sections = new List<HugoSection>();
            Header = new List<string>();
            Pre = pre;
            Title = title;
            Weight = weight;
            Path = path;
            Date = DateTime.Now.ToLocalTime();
            Header.Add("+++");
            Header.Add(String.Format("title = " + "\"{0}\"", Title));
            Header.Add("date = " + Date.ToString("yyyy-MM-dd"));
            Header.Add("weight = " + Weight);
            Header.Add("chapter = true");
            Header.Add(String.Format("pre = " + "\"{0}\"", Pre));
            Header.Add("+++");
            Header.Add("### Welcome!");
            Header.Add("This page is the main page for " + Title);
        }
    }
}
