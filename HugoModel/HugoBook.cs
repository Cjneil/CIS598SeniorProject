using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    public class HugoBook
    {
        public List<HugoChapter> Chapters { get; set; } = new List<HugoChapter>();

        public string ContentDirectoryPath { get; }

        public List<string> IndexHeader { get; } = new List<string>();

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
