using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    /// <summary>
    /// Abstract class representing child elements of HugoChapters. Only HugoPage/HugoSection are implemented. Other possibilities will not be handled
    /// </summary>
    public abstract class HugoChildElement
    {
        /// <summary>
        /// Prefix for display in sidebar ex. 1., 2., 3., etc
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
        /// Path to the element
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// Contains the lines needed for header creation for the element
        /// </summary>
        public List<string> Header { get; protected set; } = new List<string>();
    }
}
