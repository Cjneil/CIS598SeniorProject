using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{


    public class HugoChapter
    {
        /// <summary>
        /// list of sections or pages contained within the section
        /// </summary>
        public List<HugoChildElement> Children { get; set; } = new List<HugoChildElement>();

        /// <summary>
        /// Title of the chapter
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Index of the chapter within a HugoBook. Always multiples of 5
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// Prefix for display in sidebar
        /// </summary>
        public string Pre { get; }

        /// <summary>
        /// Contains the lines needed for header creation for the chapter's index
        /// </summary>
        public List<string> IndexHeader { get; } = new List<string>();

        /// <summary>
        /// Path to the chapter
        /// </summary>
        public string Path { get; }

        public HugoChapter(string pre, string title, int weight, string path)
        {
            Pre = pre;
            Title = title;
            Weight = weight;
            Path = path;
            Date = DateTime.Now.ToLocalTime();
            IndexHeader.Add("+++");
            IndexHeader.Add(String.Format("title = " + "\"{0}\"", Title));
            IndexHeader.Add("date = " + Date.ToString("yyyy-MM-dd"));
            IndexHeader.Add("weight = " + Weight);
            IndexHeader.Add("chapter = true");
            IndexHeader.Add(String.Format("pre = " + "\"{0}\"", Pre));
            IndexHeader.Add("+++");
            IndexHeader.Add("### Welcome!");
            IndexHeader.Add("This page is the main page for " + Title);
        }
    }
}
