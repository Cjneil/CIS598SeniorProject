using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    public class HugoBook
    {
        /// <summary>
        /// List of Top-Level chapters within the textbook
        /// </summary>
        public List<HugoChapter> Chapters { get; set; } = new List<HugoChapter>();

        /// <summary>
        /// The path to the content folder of the book itself
        /// </summary>
        public string ContentDirectoryPath { get; }

        /// <summary>
        /// The header for the _index.md for the top-level
        /// </summary>
        public List<string> IndexHeader { get; } = new List<string>();

        /// <summary>
        /// Creates a Hugo book at a given content path and creates the header for later writing to _index.md 
        /// </summary>
        /// <param name="path">The /content/ file that the textbook structure should reside in</param>
        public HugoBook(string path)
        {
            ContentDirectoryPath = path;
            IndexHeader.Add("+++");
            IndexHeader.Add("title = \"Homepage\"");
            IndexHeader.Add("date = " + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd"));
            IndexHeader.Add("+++");
            IndexHeader.Add("");
            IndexHeader.Add("### Welcome!");
            IndexHeader.Add("This page is meant to be edited to display course welcome info so if your instructor has not done so, tell them they should");
        }

    }
}
