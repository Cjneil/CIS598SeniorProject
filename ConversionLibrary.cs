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
            //TO-DO find some way to copy the ksucs-hugo-theme
            //using DirectoryCopy. File paths tested did not seem to work.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codioBook">The codio book object to convert to a hugo equivalent</param>
        /// <param name="path"> the target location to create the hugo book. This should be a hugo content directory</param>
        /// <returns></returns>
        public static HugoBook CodioToHugoBook(CodioBook codioBook, string path)
        {
            HugoBook hugo = new HugoBook(path);
            int chapterIndex = 5;
            foreach(CodioChapter chapter in codioBook.Chapters)
            {
                hugo.Chapters.Add(CodioToHugoChapter(chapter, chapterIndex, path));
                chapterIndex += 5;
            }
            return hugo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codioChapter"> The chapter to be converted</param>
        /// <param name="chapterIndex"> the weight of the chapter relative to other chapters, decides order</param>
        /// <param name="path"> path of the HugoBook that the HugoChapter will be part of</param>
        /// <returns></returns>
        public static HugoChapter CodioToHugoChapter(CodioChapter codioChapter, int chapterIndex, string path)
        {
            string[] splitTitle = codioChapter.Title.Split(' ');

            string pre = "<b>" + splitTitle[0] + " </b>";
            string title = splitTitle[1];

            HugoChapter hugoChapter = new HugoChapter(pre, title, chapterIndex, path + "\\" + codioChapter.Title.Replace('.', '-').Replace(' ', '-'));
            int sectionIndex = 5;
            foreach (CodioPage page in codioChapter.Pages)
            {
                //Note the fact that a Hugo "section" is basically equivalent to a Codio Page in this context
                //Codio Sections are simply what pages are referred to in the codio metadata
                hugoChapter.Sections.Add(CodioToHugoSection(page, sectionIndex, path));
                sectionIndex += 5;
            }
            return hugoChapter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codioPage"> The codio "page" aka section to be converted </param>
        /// <param name="weightIndex"> the weight of the section relative to others within the chapter, decides order</param>
        /// <param name="path"> path of the HugoChapter that the HugoChapter will be part of</param>
        /// <returns></returns>
        public static HugoSection CodioToHugoSection(CodioPage codioPage, int sectionIndex, string path)
        {
            string[] splitTitle = codioPage.Title.Split(new char[] { ' ' }, 2, StringSplitOptions.None);
            string[] splitDesignation = splitTitle[0].Split(new char[] { '.' }, 2, StringSplitOptions.None);
            string pre = "<b>" + splitTitle[0] + " </b>";
            string title = splitTitle[1];

            HugoSection hugoSection = new HugoSection(pre, title, sectionIndex, codioPage.Id, path + "\\" + codioPage.Title.Replace('.', '-').Replace(' ', '-'));
            
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
        public static void CopyImagesToHugo(Dictionary<string, string> imageMap, string source, string target)
        {
            List<string> codioImageDirectories = Directory.GetDirectories(source).ToList<string>();
            
            foreach (string directory in codioImageDirectories)
            {
                List<string> codioImages = Directory.GetFiles(directory).ToList<string>();
                foreach (string image in codioImages)
                {
                    string[] filePath = image.Split('\\');
                    string imageName = filePath[filePath.Length - 1];
                    File.Copy(image, target + "\\" + imageName);
                    string formattedCodioPath = FormatCodioImagePath(image);
                    string formattedHugoImagePath = FormatHugoImagePath(target + "\\" + imageName);
                }
            }
        }

        /// <summary>
        /// returns a string representing the path reformatted to one Codio Markdown will recognize ex: .guides/img/1/1.2.graph.png (from CC410 chapter 1, section 1.2) 
        /// </summary>
        /// <param name="path"> path to the image </param>
        /// <returns></returns>
        private static string FormatCodioImagePath(string path)
        {
            string shortened = path.Substring(path.IndexOf("\\.guides\\"));
            shortened.Replace('\\', '/');
            return path;
        }

        private static string FormatHugoImagePath(string path)
        {
            return path;
        }


        /// <summary>
        /// From https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        /// Used for copying ksucs-hugo-theme in CreateHugoFileStructure
        /// </summary>
        /// <param name="sourceDirName">Directory to copy</param>
        /// <param name="destDirName">Target directory, if does not exist it will create it</param>
        /// <param name="copySubDirs">whether to copy subdirectories</param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
