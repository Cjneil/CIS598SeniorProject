using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{


    public class HugoSection : HugoChildElement
    {
        /// <summary>
        /// list of sections or pages contained within the section
        /// </summary>
        public List<HugoChildElement> Children { get; set; } = new List<HugoChildElement>();

        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime Date { get; }


        public HugoSection(string pre, string title, int weight, string path)
        {
            Pre = pre;
            Title = title;
            Weight = weight;
            Path = path;
            Date = DateTime.Now.ToLocalTime();
            Header.Add("+++");
            Header.Add(String.Format("title = " + "\"{0}\"", Title));
            Header.Add("date = " + Date.ToString("yyyy-MM-dd"));
            Header.Add("weight = " + Weight);
            Header.Add("chapter = false");
            Header.Add(String.Format("pre = " + "\"{0}\"", Pre));
            Header.Add("+++");
            Header.Add("### Welcome!");
            Header.Add("This page is the main page for " + Title);
        }
    }
}
