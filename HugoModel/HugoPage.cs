using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    public class HugoPage : HugoChildElement
    {
        /// <summary>
        /// Probably not useful for pages, but unique identifier from Codio format
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// The text in the original Codio Page that the HugoPageconverted
        /// </summary>
        public List<string> CodioFile { get; set; }

        /// <summary>
        /// Represents a Hugo page
        /// </summary>
        /// <param name="pre">prefix of the hugo page for display in the left margin</param>
        /// <param name="title">Title of the page</param>
        /// <param name="weight">Weight of the section for use in ordering. Multiples of 5 only. Lower value = displayed higher</param>
        /// <param name="id"> unique identifier from Codio Metadata </param>
        /// <param name="path"> path to where this page will be created</param>
        public HugoPage(string pre, string title, int weight, string id, string path)
        {
            Pre = pre;
            Title = title;
            Weight = weight;
            Path = path + ".md";
            ID = id;
            Header.Add("---");
            Header.Add("title: \"" + Title + "\"");
            Header.Add("weight: " + Weight);
            Header.Add("pre: \"" + Pre + "\"");
            Header.Add("---");
        }
    }
}
