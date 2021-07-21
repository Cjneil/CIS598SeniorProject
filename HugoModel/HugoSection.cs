using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.HugoModel
{
    /// <summary>
    /// Hugo Equivalent to a CodioSection (not CodioMetadataSection)
    /// </summary>
    public class HugoSection : HugoChildElement
    {
        /// <summary>
        /// list of sections or pages contained within the section. Can be empty
        /// </summary>
        public List<HugoChildElement> Children { get; set; } = new List<HugoChildElement>();

        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Represents a Hugo Section. Can be nested within a chapter or within another section.
        /// </summary>
        /// <param name="pre">Prefix for use in header to display on sidebar of site</param>
        /// <param name="title">Title of the section</param>
        /// <param name="weight">index of the section relative to other elements within parent element</param>
        /// <param name="path">Path where the section will be created</param>
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
