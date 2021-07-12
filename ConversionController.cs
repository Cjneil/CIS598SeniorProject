using CodioToHugoConverter.CodioModel;
using CodioToHugoConverter.HugoModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodioToHugoConverter
{
    public class ConversionController
    {
        /// <summary>
        /// represents the location of file directory for the Codio Textbook  to be converted
        /// </summary>
        private string _source;

        /// <summary>
        /// Represents the location of the target directory to create Hugo textbook in
        /// </summary>
        private string _target;

        private CodioBook _codioBook;
        private HugoBook _hugoBook;

        private Dictionary<string, string> _imagePathMap;

        public ConversionController()
        {
            _source = "";
            _target = "";
            _imagePathMap = new Dictionary<string, string>();
        }

        public string handleViewFileSelection(string state, string path)
        {
            switch (state)
            {
                case "CodioFileSelected":
                    string contentPath = @"C:\Users\Owner\Documents\Classes\CIS 598\cc410-textbook-master\.guides\content";
                    string imgPath = path + "\\.guides\\img";
                    string codioPath = path + "\\.codio";
                    if (Directory.Exists(contentPath) && Directory.Exists(imgPath) && File.Exists(codioPath))
                    {
                        _source = path;
                        return path;
                    }
                    else
                    {
                        return "Invalid Directory selected. Codio file structure not present.";
                    }
                case "HugoTargetSelected":
                    if(Directory.EnumerateFiles(@path).Count() == 0)
                    {
                        _target = path;
                        return path;
                    }
                    else
                    {
                        return "Invalid directory selected. Directory is not empty.";
                    }
                default:
                    return "Default Case. This should not be reached.";

            }
        }

        public void handleConvertTextbook()
        {
            _codioBook = ConversionLibrary.ConvertCodioBookJsonToObject(_source + @"\.guides\book.json");
            ConversionLibrary.CreateHugoFileStructure(_target);
            ConversionLibrary.CopyImagesToHugo(_imagePathMap, _source + "\\.guides\\img", _target + "\\static\\images");
            _hugoBook = ConversionLibrary.CodioToHugoBook(_codioBook, _target + "\\content");
        }
    }
}
