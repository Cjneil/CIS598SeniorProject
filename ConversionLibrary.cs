/* Author: Connor Neil
 * Purpose: Library of static methods for use in the conversion of a Codio textbook to a Hugo textbook format
 * Uses CodioModel and HugoModel classes
 * 
 */
using CodioToHugoConverter.CodioModel;
using CodioToHugoConverter.HugoModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter
{
    public static class ConversionLibrary
    {

        public static CodioBook ConvertCodioBookJsonToObject(string path)
        {
            CodioBook book = null;
            using (StreamReader reader = new StreamReader(@path))
            {
                string json = reader.ReadToEnd();
                book = Newtonsoft.Json.JsonConvert.DeserializeObject<CodioBook>(json);
            }
            return book;
        }

        /// <summary>
        /// Creates a map of ID to path for Codio Sections
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, string> MapCodioMetadata(string path)
        {
            CodioMetadata metadata = null;
            Dictionary<string, string> map = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                metadata = Newtonsoft.Json.JsonConvert.DeserializeObject<CodioMetadata>(json);
            }
            if(metadata != null)
            {
                foreach(CodioSection section in metadata.Sections)
                {
                    map.Add(section.Id, section.ContentFile);
                }
            }
            return map;
        }

        /// <summary>
        /// Creates Hugo file structure at target directory.
        /// </summary>
        /// <param name="targetDirectory"></param>
        public static void CreateHugoFileStructure(string targetDirectory)
        {
            Directory.CreateDirectory(targetDirectory + @"\content");
            Directory.CreateDirectory(targetDirectory + @"\layouts\partials");
            Directory.CreateDirectory(targetDirectory + @"\static\files");
            Directory.CreateDirectory(targetDirectory + @"\static\images");
            Directory.CreateDirectory(targetDirectory + @"\themes");
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codioBook">The codio book object to convert to a hugo equivalent</param>
        /// <param name="hugoPath"> the target location to create the hugo book. This should be a hugo content directory</param>
        /// <returns></returns>
        public static HugoBook CodioToHugoBook(CodioBook codioBook, string codioPath, string hugoPath, Dictionary<string, string> IDMap)
        {
            HugoBook hugo = new HugoBook(hugoPath);
            int chapterIndex = 5;
            foreach (CodioChapter chapter in codioBook.Chapters)
            {
                hugo.Chapters.Add(CodioToHugoChapter(chapter, chapterIndex, codioPath, hugoPath, IDMap));
                chapterIndex += 5;
            }
            return hugo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="codioChapter"> The chapter to be converted</param>
        /// <param name="chapterIndex"> the weight of the chapter relative to other chapters, decides order</param>
        /// <param name="bookPath"> path of the HugoBook that the HugoChapter will be part of</param>
        /// <returns></returns>
        public static HugoChapter CodioToHugoChapter(CodioChapter codioChapter, int chapterIndex, string codioPath, string bookPath, Dictionary<string, string> IDMap)
        {
            string[] splitTitle = codioChapter.Title.Split(' ');

            string pre = "<b>" + (chapterIndex / 5).ToString() + ". " + " </b>";
            string title = splitTitle[1];
            string chapterPath = bookPath + "\\" + codioChapter.Title.Replace('.', '-').Replace(' ', '-').Replace("--", "-");
            HugoChapter hugoChapter = new HugoChapter(pre, title, chapterIndex, chapterPath);
            int sectionIndex = 5;
            foreach (CodioPage page in codioChapter.Pages)
            {
                //Note the fact that a Hugo "section" is basically equivalent to a Codio Page in this context
                //Codio Sections are simply what pages are referred to in the codio metadata
                if (!page.Title.ToLower().Contains("quiz")) { 
                    hugoChapter.Sections.Add(CodioToHugoSection(page, sectionIndex, codioPath, chapterPath, IDMap));
                    sectionIndex += 5;
                }
            }
            return hugoChapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codioPage">The page that is being converted to a Hugo "Section" aka page</param>
        /// <param name="sectionIndex">The index the section will have, multiple of 5</param>
        /// <param name="codioPath">The path to the codio content directory</param>
        /// <param name="chapterPath">the path to the chapter containing this section</param>
        /// <param name="IDMap">Map of PageIds to the filePath of the codio page itself (used to get which file is being converted)</param>
        /// <returns></returns>
        public static HugoSection CodioToHugoSection(CodioPage codioPage, int sectionIndex, string codioPath, string chapterPath, Dictionary<string, string> IDMap)
        {
            string[] splitTitle = codioPage.Title.Split(new char[] { ' ' }, 2, StringSplitOptions.None);
            string pre = (sectionIndex/5).ToString() + ". ";
            string title = splitTitle[1];

            HugoSection hugoSection = new HugoSection(pre, title, sectionIndex, codioPage.PageId, chapterPath + "\\" + codioPage.Title.Replace('.', '-').Replace(' ', '-').Replace("--", "-"));
            string filePath = "";
            if (IDMap.TryGetValue(codioPage.PageId, out filePath)) {
                filePath = filePath.Replace('/', '\\');
                string completePath = codioPath + @"\" + filePath;
                hugoSection.CodioFile = File.ReadAllLines(completePath).ToList();
            }
            return hugoSection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageMap"> 
        /// Dictionary to hold references to initial Codio img link ex: .guides/img/1/1.2.graph.png (from CC410 chapter 1, section 1.2) 
        /// along with a reference to new location in format of 
        /// </param>
        /// <param name="path"> Folder to copy images to</param>
        public static void CopyImagesToHugo(string source, string target)
        {
            List<string> codioImageDirectories = Directory.GetDirectories(source).ToList<string>();
            
            foreach (string directory in codioImageDirectories)
            {
                FileInfo imageDirectory = new FileInfo(directory);
                string targetDirectory = target + "\\" + imageDirectory.Name;
                Directory.CreateDirectory(targetDirectory);
                
                List<string> codioImages = Directory.GetFiles(directory).ToList<string>();
                foreach (string image in codioImages)
                {
                    string targetPath =  targetDirectory + "\\" + (new FileInfo(image).Name);
                    File.Copy(image, targetPath);
                }
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
                writer.WriteLine("+++");
                writer.WriteLine("title = \"Homepage\"");
                writer.WriteLine("date = " + DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd"));
                writer.WriteLine("+++");
                writer.WriteLine("");
                writer.WriteLine("### Welcome!");
                writer.WriteLine("This page is meant to be edited to display course welcome info so if your instructor has not done so, tell them they should");
            }
            
            foreach (HugoChapter chapter in book.Chapters)
            {
                createHugoChapter(chapter);
            }
        }

        private static void createHugoChapter(HugoChapter chapter)
        {
            Directory.CreateDirectory(chapter.Path);
            string indexPath = chapter.Path + @"\_index.md";
            File.Create(indexPath).Close();
            using (StreamWriter writer = new StreamWriter(indexPath))
            {
                foreach(string row in chapter.Header)
                {
                    writer.WriteLine(row);
                }
            }

            foreach(HugoSection section in chapter.Sections)
            {
                createHugoSection(section);
            }
        }

        private static void createHugoSection(HugoSection section)
        {
                File.Create(section.Path).Close();
                using (StreamWriter writer = new StreamWriter(section.Path))
                {

                    foreach (string row in section.Header)
                    {
                        writer.WriteLine(row);
                    }
                    foreach (string line in section.CodioFile)
                    {
                        string outputLine = line;
                        if (outputLine.Contains(@".guides/img/"))
                        {
                            outputLine = outputLine.Replace(".guides/img/", "../../images/");
                        }

                        if (line.Trim().Equals("|||"))
                        {
                            string newLine = "{{% / notice %}}";
                            writer.WriteLine(newLine);
                        }
                        else if (line.Contains("|||"))
                        {
                            string[] split = line.Split(' ');
                            string newLine = "";
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
