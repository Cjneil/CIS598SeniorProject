using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    public class HugoSection
    {
        /// <summary>
        /// Used to define prefix in left sidebar
        /// </summary>
        public string Pre { get; }

        /// <summary>
        /// Used to define title in left sidebar
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Used to sort sections within the chapter. Multiples of 5 to leave space for more pages if needed
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// May or may not be useful, but unique identifier from Codio format
        /// </summary>
        public string ID { get; }
        /// <summary>
        /// Path to the hugo file the section represents
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The text in the original Codio File that the HugoSection converted
        /// Probably should be in CodioSection... but ran out of time to change
        /// </summary>
        public List<string> CodioFile { get; set; }

        public List<string> IndexHeader { get; } = new List<string>();

        /// <summary>
        /// Represents a Hugo section aka page
        /// </summary>
        /// <param name="pre">prefix of the hugo section for display in the left margin</param>
        /// <param name="title">Title of the section</param>
        /// <param name="weight">Weight of the section for use in ordering. Multiples of 5 only. Lower value = displayed higher</param>
        /// <param name="id"></param>
        /// <param name="path"></param>
        public HugoSection(string pre, string title, int weight, string id, string path)
        {
            Pre = pre;
            Title = title;
            Weight = weight;
            ID = id;
            Path = path + ".md";
            IndexHeader.Add("---");
            IndexHeader.Add("title: \"" + Title + "\"");
            IndexHeader.Add("weight: " + Weight);
            IndexHeader.Add("pre: \"" + Pre + "\"");
            IndexHeader.Add("---");
        }
    }
}
