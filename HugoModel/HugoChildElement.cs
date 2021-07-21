using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    public abstract class HugoChildElement
    {
        /// <summary>
        /// Prefix for display in sidebar
        /// </summary>
        public string Pre { get; protected set; }

        /// <summary>
        /// Title of the Element
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Index of the element within its parent element. Always multiples of 5
        /// </summary>
        public int Weight { get; protected set; }

        /// <summary>
        /// Path to the chapter
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Contains the lines needed for header creation for the element
        /// </summary>
        public List<string> Header { get; protected set; } = new List<string>();
    }
}
