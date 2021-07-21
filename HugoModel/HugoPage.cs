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
        /// May or may not be useful, but unique identifier from Codio format
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// The text in the original Codio File that the HugoSection converted
        /// Probably should be in CodioSection... but ran out of time to change
        /// </summary>
        public List<string> CodioFile { get; set; }

        /// <summary>
        /// Represents a Hugo section aka page
        /// </summary>
        /// <param name="pre">prefix of the hugo section for display in the left margin</param>
        /// <param name="title">Title of the section</param>
        /// <param name="weight">Weight of the section for use in ordering. Multiples of 5 only. Lower value = displayed higher</param>
        /// <param name="id"></param>
        /// <param name="path"></param>
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
