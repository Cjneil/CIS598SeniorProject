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

        public string Path { get; }

        

        public HugoSection(string pre, string title, int weight, string id, string path)
        {
            Pre = pre;
            Title = title;
            Weight = weight;
            ID = id;
            Path = path;
        }
    }
}
