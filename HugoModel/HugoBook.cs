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

        public HugoBook(string path)
        {
            ContentDirectoryPath = path;
        }

    }
}
