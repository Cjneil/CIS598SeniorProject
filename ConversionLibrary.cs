/* Author: Connor Neil
 * Purpose: Library of static methods for use in the conversion of a Codio textbook to a Hugo textbook format
 * Uses CodioModel and HugoModel classes
 */
using CodioToHugoConverter.CodioModel;
using CodioToHugoConverter.HugoModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodioToHugoConverter
{
    public static class ConversionLibrary
    {
        /// <summary>
        /// Validates that the Codio directory contains all necessary directories to pull information from.
        /// </summary>
        /// <param name="directoryPath">Path to the codio textbook directory</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool validateCodioDirectory(string directoryPath)
        {
            string contentPath = directoryPath;
            string imgPath = directoryPath + "\\.guides\\img";
            string codioPath = directoryPath + "\\.codio";
            return (Directory.Exists(contentPath) && Directory.Exists(imgPath) && File.Exists(codioPath));
        }

        /// <summary>
        /// Validates that the selected directory to create the Hugo Textbook in is empty
        /// </summary>
        /// <param name="directoryPath">Path to the empty directory</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool validateHugoDirectory(string directoryPath)
        {
            return (Directory.EnumerateDirectories(directoryPath).Count() == 0 && Directory.EnumerateFiles(directoryPath).Count() == 0);
        }

        /// <summary>
        /// Creates a Codio Book model from a Codio book.json file. This will fail if it is not properly formatted
        /// but if it wasn't properly formatted the Codio book itself would've been broken anyway so handling this
        /// is not within the scope of the project.
        /// </summary>
        /// <param name="path">Path </param>
        /// <returns></returns>
        public static CodioBook ConvertCodioBookJsonToObject(string path)
        {
            CodioBook book = null;
            using (StreamReader reader = new StreamReader(path +"\\.guides\\book.json"))
            {
                string json = reader.ReadToEnd();
                book = Newtonsoft.Json.JsonConvert.DeserializeObject<CodioBook>(json);
            }
            return book;
        }

        /// <summary>
        /// Creates a map of ID to path for Codio Pages
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, string> MapCodioMetadata(string path)
        {
            CodioMetadata metadata = null;
            Dictionary<string, string> map = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(path + "\\.guides\\metadata.json"))
            {
                string json = reader.ReadToEnd();
                metadata = Newtonsoft.Json.JsonConvert.DeserializeObject<CodioMetadata>(json);
            }
            if(metadata != null)
            {
                foreach(CodioMetadataSection section in metadata.Sections)
                {
                    map.Add(section.Id, section.ContentFile);
                }
            }
            return map;
        }

        /// <summary>
        /// Creates Hugo file structure at target directory. Target directory must be empty which is validated
        /// by controller in GUI selection stage using ValidateHugoDirectory. Please call that method first to check
        /// </summary>
        /// <param name="targetDirectory">The base directory that the hugo textbook will be created within. </param>
        public static void CreateHugoFileStructure(string targetDirectory)
        {
            Directory.CreateDirectory(targetDirectory + @"\content");
            Directory.CreateDirectory(targetDirectory + @"\layouts\partials");
            Directory.CreateDirectory(targetDirectory + @"\static\files");
            Directory.CreateDirectory(targetDirectory + @"\static\images");
            Directory.CreateDirectory(targetDirectory + @"\themes");
        }

        /// <summary>
        /// Copies images from their Codio directory 
        /// </summary>
        /// <param name="imageMap"> 
        /// Dictionary to hold references to initial Codio img link ex: .guides/img/1/1.2.graph.png (from CC410 chapter 1, section 1.2) 
        /// along with a reference to new location in format of 
        /// </param>
        /// <param name="path"> Folder to copy images to</param>
        public static void CopyImagesToHugo(string source, string target)
        {
            List<string> codioImageDirectories = Directory.GetDirectories(source + "\\.guides\\img").ToList<string>();
            
            foreach (string directory in codioImageDirectories)
            {
                FileInfo imageDirectory = new FileInfo(directory);
                string targetDirectory = target + "\\static\\images" + "\\" + imageDirectory.Name;
                Directory.CreateDirectory(targetDirectory);
                
                List<string> codioImages = Directory.GetFiles(directory).ToList<string>();
                foreach (string image in codioImages)
                {
                    string targetPath =  targetDirectory + "\\" + (new FileInfo(image).Name);
                    //The file really shouldn't exist if you validated the Hugo directory... but just in case.
                    if (!File.Exists(targetPath))
                    {
                        File.Copy(image, targetPath);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the Hugo model from the codio model of the textbook by calling CodioToHugoChapter on each CodioChapter
        /// Created chapters' data is stored as a list within HugoBook
        /// </summary>
        /// <param name="codioBook">The codio book object to convert to a hugo equivalent</param>
        /// <param name="hugoPath"> the target location to create the hugo book. This should be a hugo content directory</param>
        /// <returns>The Hugo textbook model</returns>
        public static HugoBook CodioToHugoBook(CodioBook codioBook, string codioPath, string hugoPath, Dictionary<string, string> IDMap)
        {
            string contentPath = hugoPath + "\\content";
            HugoBook hugo = new HugoBook(contentPath);
            int chapterIndex = 5;
            foreach (CodioChapter chapter in codioBook.Chapters)
            {
                hugo.Chapters.Add(CodioToHugoChapter(chapter, chapterIndex, codioPath, contentPath, IDMap));
                chapterIndex += 5;
            }
            return hugo;
        }


        /// <summary>
        /// Creates a Hugo chapter from Codio Chapter data. These are stored in a list within the HugoBook.
        /// This in turn calls CodioToHugoElement to create model elements for all children of the chapter
        /// which are then stored in the chapter itself.
        /// </summary>
        /// <param name="codioChapter"> The chapter to be converted</param>
        /// <param name="chapterIndex"> the weight of the chapter relative to other chapters, decides order</param>
        /// <param name="contentPath"> path of the HugoBook that the HugoChapter will be part of</param>
        /// <returns>The Hugo Chapter model that was created from Codio data</returns>
        public static HugoChapter CodioToHugoChapter(CodioChapter codioChapter, int chapterIndex, string codioPath, string contentPath, Dictionary<string, string> IDMap)
        {
            string pre = "<b>" + (chapterIndex / 5).ToString() + ". " + " </b>";

            string title = "";
            if (codioChapter.Title.Contains('.'))
            {
                title = codioChapter.Title.Substring(codioChapter.Title.LastIndexOf('.') + 1).Trim();
            }
            else title = codioChapter.Title;

            char[] invalidChars = new char[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*' }; //invalid chars for a Windows file path
            string fileName = (chapterIndex/5) + "-" + invalidChars.Aggregate(title, (c1, c2) => c1.Replace(c2, '-')); //replaces all invalid chars with a -
            string chapterPath = contentPath + "\\" + fileName;

            HugoChapter hugoChapter = new HugoChapter(pre, title, chapterIndex, chapterPath);

            int childIndex = 5;
            foreach (CodioChildElement element in codioChapter.Children)
            {
                //Note the fact that a Hugo "section" is basically equivalent to a Codio Page in this context
                //Codio Sections are simply what pages are referred to in the codio metadata
                //Note the fact that all quiz pages are excluded since Hugo does not by default have an equivalent.
                if (!element.Title.ToLower().Contains("quiz")) { 
                    hugoChapter.Children.Add(CodioToHugoElement(element, childIndex, codioPath, chapterPath, IDMap));
                    childIndex += 5;
                }
            }
            return hugoChapter;
        }

        /// <summary>
        /// Creates a Hugo model child element which can be either a section or a page. In the case of a section, will recursively call on children of the section
        /// </summary>
        /// <param name="codioElement">The child element which can be either a section or page depending on if pageID is present</param>
        /// <param name="elementIndex">The index the section will have, multiple of 5</param>
        /// <param name="codioPath">The path to the codio content directory</param>
        /// <param name="parentPath">the path to the chapter/section containing this element</param>
        /// <param name="IDMap">Map of PageIds to the filePath of the codio page itself (used to get which file is being converted)</param>
        /// <returns>The Hugo Section model created from the Codio Page</returns>
        public static HugoChildElement CodioToHugoElement(CodioChildElement codioElement, int elementIndex, string codioPath, string parentPath, Dictionary<string, string> IDMap)
        {
            
            string pre = (elementIndex/5).ToString() + ". ";
            string title = "";
            if (codioElement.Title.Contains('.'))
            {
                title = codioElement.Title.Substring(codioElement.Title.LastIndexOf('.') + 1).Trim();
            }
            else title = codioElement.Title;

            char[] invalidChars = new char[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*' }; //invalid chars for a Windows file
            string fileName = (elementIndex / 5) + "-" + invalidChars.Aggregate(title, (c1, c2) => c1.Replace(c2, '-'));
            string childPath = parentPath + "\\" + fileName;

            if(codioElement is CodioSection section)
            {
                HugoSection hugoSection = new HugoSection(pre, title, elementIndex, childPath);
                int childIndex = 5;
                foreach (CodioChildElement element in section.Children)
                {
                    //Note the fact that all quiz pages are excluded since Hugo does not by default have an equivalent.
                    if (!element.Title.ToLower().Contains("quiz"))
                    {
                        hugoSection.Children.Add(CodioToHugoElement(element, childIndex, codioPath, childPath, IDMap));
                        childIndex += 5;
                    }
                }
                return hugoSection;
            }
            else
            {
                CodioPage page = (CodioPage) codioElement;
                HugoPage hugoPage = new HugoPage(pre, title, elementIndex, page.PageId, childPath);
                //Retrieves the path to the codio page and iterates through to store lines of text in a List in the HugoSection Model
                if (IDMap.TryGetValue(page.PageId, out string filePath))
                {
                    filePath = filePath.Replace('/', '\\');
                    string completePath = codioPath + @"\" + filePath;
                    hugoPage.CodioFile = File.ReadAllLines(completePath).ToList();
                }
                return hugoPage;
            }
            
            
        }

        /// <summary>
        /// Assumes that CreateHugoFileStructure has already been called and
        /// a hugo file structure properly exists at the given path.
        /// </summary>
        /// <param name="book">The hugo book to create files based on</param>
        /// <param name="path">Path to base directory of the new hugo textbook</param>
        public static void CreateHugoFiles(HugoBook book)
        {
            string indexPath = book.ContentDirectoryPath + @"\_index.md";
            File.Create(indexPath).Close();
            using (StreamWriter writer = new StreamWriter(indexPath))
            {
                foreach(string line in book.IndexHeader)
                {
                    writer.WriteLine(line);
                }
                

            }
            
            foreach (HugoChapter chapter in book.Chapters)
            {
                CreateHugoChapter(chapter);
            }
        }

        /// <summary>
        /// Creates the actual directory for the hugo chapter and creates the index file within it based on chapter Header information
        /// </summary>
        /// <param name="chapter">The chapter to be created</param>
        private static void CreateHugoChapter(HugoChapter chapter)
        {
            Directory.CreateDirectory(chapter.Path);
            string indexPath = chapter.Path + @"\_index.md";
            File.Create(indexPath).Close();
            using (StreamWriter writer = new StreamWriter(indexPath))
            {
                foreach(string row in chapter.IndexHeader)
                {
                    writer.WriteLine(row);
                }
            }

            foreach(HugoChildElement element in chapter.Children)
            {
                CreateHugoElement(element);
            }
        }

        /// <summary>
        /// Creates a Hugo element. Behavior depends on whether it is a HugoSection or HugoPage
        /// In the case of a section, creates directory and recursively calls on children
        /// if a page instead, hands off creation of element to CreateHugoPage
        /// </summary>
        /// <param name="element">The Hugo model element to create in file system</param>
        private static void CreateHugoElement(HugoChildElement element)
        {
            if(element is HugoSection section)
            {
                Directory.CreateDirectory(section.Path);
                string indexPath = section.Path + @"\_index.md";
                File.Create(indexPath).Close();
                using (StreamWriter writer = new StreamWriter(indexPath))
                {
                    foreach (string row in section.Header)
                    {
                        writer.WriteLine(row);
                    }
                }
                foreach (HugoChildElement child in section.Children)
                {
                    CreateHugoElement(child);
                }
            }
            else if(element is HugoPage page)
            {
                CreateHugoPage(page);
            }
        }

        /// <summary>
        /// Creates the file for the given Hugo page and writes the Hugo markdown header before 
        /// converting line by line the Codio page that it will be the equivalent of.
        /// Notable potential issue is that if any line includes ||| but is NOT intended to be an info
        /// box of some type, it will create issues but not return an error. 
        /// Also notable is that citations after images
        /// </summary>
        /// <param name="page">The Hugo page to be created</param>
        private static void CreateHugoPage(HugoPage page)
        {
            File.Create(page.Path).Close();
            using (StreamWriter writer = new StreamWriter(page.Path))
            {
                string pagePath = page.Path.Substring((page.Path.LastIndexOf("content\\")));

                string[] splitPath = pagePath.Split(new string[] { "\\" }, System.StringSplitOptions.None);

                StringBuilder imageReplacementBuilder = new StringBuilder();
                for (int i = 0; i < splitPath.Length - 1; i++)
                {
                    imageReplacementBuilder.Append("../");
                }
                imageReplacementBuilder.Append("images/");

                //this string will replace any instances of .guides/img/ seen within Codio files with the Hugo equivalent relative path
                string imageReplacementString = imageReplacementBuilder.ToString();

                foreach (string row in page.Header)
                {
                    writer.WriteLine(row);
                }

                foreach (string line in page.CodioFile)
                {
                    string outputLine = line;

                    //Checks for an image reference and replaces the reference with a Hugo equivalent based on file depth 
                    if (outputLine.Contains(@".guides/img/"))
                    {
                        outputLine = outputLine.Replace(".guides/img/", imageReplacementString);
                    }

                    //all info boxes should end with a standard ||| line. This check occurs first such that it is not caught
                    //by the check for the line signalling the beginning of the box formatted ||| type
                    if (line.Trim().Equals("|||"))
                    {
                        string newLine = "{{% / notice %}}";
                        writer.WriteLine(newLine);
                    }
                    //assumes properly formatted starting line of ||| type but does handle additional space between
                    else if (line.Contains("||| "))
                    {
                        string[] split = line.Split(' ');
                        string newLine;
                        if (split[1].Contains("growthhack"))
                        {
                            newLine = "{{% notice tip %}}";
                        }
                        else if (split[1].Contains("xdiscipline"))
                        {
                            newLine = "{{% notice note %}}";
                        }
                        else
                            newLine = "{{% notice info %}}";

                        writer.WriteLine(newLine);
                    }
                    else
                        writer.WriteLine(outputLine);
                }
            }
        }

    }
}
