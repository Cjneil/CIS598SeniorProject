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
        private GUIObserver _guiObserver;

        /// <summary>
        /// represents the location of file directory for the Codio Textbook  to be converted
        /// </summary>
        private string _source;

        /// <summary>
        /// Represents the location of the target directory to create Hugo textbook in
        /// </summary>
        private string _target;

        /// <summary>
        /// The model for the Codio version of the textbook. Includes a lot of potentially unnecessary json fields
        /// </summary>
        private CodioBook _codioBook;

        /// <summary>
        /// The Model for the Hugo version of the textbook
        /// </summary>
        private HugoBook _hugoBook;

        /// <summary>
        /// Maps the Codio Section ID's to the file path within the codio directory
        /// ConversionLibrary contains MapCodioMetadata to provide the dictionary
        /// </summary>
        private Dictionary<string, string> _codioPathMap;

        /// <summary>
        /// Does what the name suggests. Controls the conversion
        /// </summary>
        public ConversionController()
        {
            _source = "";
            _target = "";
        }

        /// <summary>
        /// Handles validation of Source/Target selection and tells GUI to update
        /// depending on the result whether valid or invalid.
        /// </summary>
        /// <param name="state">Whether codio or hugo path was selected</param>
        /// <param name="path">The path to the Codio/Hugo directory respectively</param>
        public void handleViewFileSelection(SelectionState state, string path)
        {
            switch (state)
            {
                case SelectionState.Codio:
                    string contentPath = path;
                    string imgPath = path + "\\.guides\\img";
                    string codioPath = path + "\\.codio";
                    if (ConversionLibrary.validateCodioDirectory(path))
                    {
                        _source = path;
                        _guiObserver(ProgramState.ValidCodio, path);
                    }
                    else
                    {
                        _guiObserver(ProgramState.InvalidCodio, "Invalid Directory selected. Codio file structure not present.");
                    }
                    break;
                case SelectionState.Hugo:
                    if(ConversionLibrary.validateHugoDirectory(path))
                    {
                        _target = path;
                        _guiObserver(ProgramState.ValidHugo, path);
                    }
                    else
                    {
                        _guiObserver(ProgramState.InvalidHugo, "Invalid directory selected. Directory is not empty.");
                    }
                    break;
                default:
                    _guiObserver(ProgramState.DEFAULT, "");
                    break;

            }
        }

        /// <summary>
        /// Uses the ConversionLibrary to convert the codio textbook to Hugo format.
        /// Process converts codio book json to codio model objects and maps the metadata, 
        /// creates the hugo file structure in target, copies over the images,
        /// creates hugo model from the codio model, and then creates the new files within the hugo directory
        /// Any errors will provide a stack trace to the GUI.
        /// </summary>
        public void handleConvertTextbook()
        {
            try
            {
                _codioBook = ConversionLibrary.ConvertCodioBookJsonToObject(_source);
                _codioPathMap = ConversionLibrary.MapCodioMetadata(_source);
                ConversionLibrary.CreateHugoFileStructure(_target);
                ConversionLibrary.CopyImagesToHugo(_source, _target);
                _hugoBook = ConversionLibrary.CodioToHugoBook(_codioBook, _source, _target, _codioPathMap);
                ConversionLibrary.CreateHugoFiles(_hugoBook);
                _guiObserver(ProgramState.ConversionSuccess, "Hugo textbook successfully created from Codio source");
            }
            catch(Exception ex)
            {
                _source = null;
                _target = null;
                _codioBook = null;
                _hugoBook = null;
                _codioPathMap = null;
                _guiObserver(ProgramState.ConversionFailure, ex.ToString());
            }
        }

        /// <summary>
        /// Adds the observer to update GUI with
        /// </summary>
        /// <param name="observer">The delegate that is observing. Will only be updateGUI from FileSelection within this project</param>
        public void RegisterLoginObserver(GUIObserver observer)
        {
            _guiObserver = observer;
        }
    }
}
