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

        public List<string> IndexLines { get; }

        public string Path { get; }

        public HugoChapter(string pre, string title, int weight, string path)
        {
            Sections = new List<HugoSection>();
            IndexLines = new List<string>();
            Pre = pre;
            Title = title;
            Weight = weight;
            Path = path;
            Date = DateTime.Now.ToLocalTime();
            IndexLines.Add("+++");
            IndexLines.Add(String.Format("title = " + "\"{0}\"", Title));
            IndexLines.Add("weight = " + Weight);
            IndexLines.Add("chapter = true");
            IndexLines.Add(String.Format("pre = " + "\"{0}\"", Pre));
            IndexLines.Add("+++");
            IndexLines.Add("\n" + "# " + Title);
        }
    }
}
