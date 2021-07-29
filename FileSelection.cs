using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodioToHugoConverter
{
    /// <summary>
    /// States that the GUI can be in dependent on which button is clicked to send to controller
    /// </summary>
    public enum SelectionState
    {
        Codio,
        Hugo,
        DEFAULT
    }

    /// <summary>
    /// States that the controller can possibly provide to the UpdateGUI method
    /// </summary>
    public enum ProgramState
    {
        InvalidCodio,
        InvalidHugo,
        ValidCodio,
        ValidHugo,
        ConversionSuccess,
        ConversionFailure,
        DEFAULT
    }

    public partial class FileSelection : Form
    {
        /// <summary>
        /// Delegate for controller's handler of a file selection event
        /// </summary>
        private FileSelectionDelegate _fileSelectionHandler;
        /// <summary>
        /// delegate for controller's handler of a conversion event
        /// </summary>
        private ConvertTextbookDelegate _textbookConversionHandler;
        /// <summary>
        /// stores whether the current Hugo target is a valid one. 
        /// Used for logic of enabling/disabling buttons
        /// </summary>
        private bool _validHugoTarget;
        /// <summary>
        /// stores whether the current Codio source is a valid Codio textbook. 
        /// Used for logic of enabling/disabling buttons
        /// </summary>
        private bool _validCodioSource;

        public FileSelection(FileSelectionDelegate fileSelectionHandler, ConvertTextbookDelegate textbookHandler)
        {
            InitializeComponent();
            _fileSelectionHandler = fileSelectionHandler;
            _textbookConversionHandler = textbookHandler;
        }

        /// <summary>
        /// Handles the click event of the Select Codio Directory button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodioSelectButton_Click(object sender, EventArgs e)
        {
            if (uxConversionResultBox.Text != "")
            {
                uxCodioPath.Text = "";
                uxHugoPath.Text = "";
                _validCodioSource = false;
                _validHugoTarget = false;
                uxCreateHugoTextbookButton.Enabled = false;
            }
            uxConversionResultBox.Text = "";
            string selectedFile;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                EnsurePathExists = true,
                EnsureFileExists = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedFile = dialog.FileName;
                _fileSelectionHandler(SelectionState.Codio, selectedFile);
                
            }
            
        }

        /// <summary>
        /// Handles the click event of the Select Target Hugo Directory Button by using delegate to controller handler for the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HugoTargetButton_Click(object sender, EventArgs e)
        {
            if(uxConversionResultBox.Text != "")
            {
                uxCodioPath.Text = "";
                uxHugoPath.Text = "";
                _validCodioSource = false;
                _validHugoTarget = false;
                uxCreateHugoTextbookButton.Enabled = false;
            }
            uxConversionResultBox.Text = "";
            string selectedFile = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                EnsurePathExists = true,
                EnsureFileExists = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedFile = dialog.FileName;
                _fileSelectionHandler(SelectionState.Hugo, selectedFile);
            }
        }

        /// <summary>
        /// Updates state of the GUI based on checks made within the controller and the resulting state/info
        /// </summary>
        /// <param name="state">The resulting state after controller functions</param>
        /// <param name="result">Any other necessary info to accompany the state</param>
        public void UpdateGUI(ProgramState state, string result)
        {
            switch(state)
            {
                case ProgramState.InvalidCodio:
                    _validCodioSource = false;
                    uxCodioPath.Text = result;
                    if (_validCodioSource && _validHugoTarget) uxCreateHugoTextbookButton.Enabled = true;
                    break;
                case ProgramState.ValidCodio:
                    _validCodioSource = true;
                    uxCodioPath.Text = result;
                    if (_validCodioSource && _validHugoTarget) uxCreateHugoTextbookButton.Enabled = true;
                    break;
                case ProgramState.InvalidHugo:
                    _validHugoTarget = false;
                    uxHugoPath.Text = result;
                    if (_validCodioSource && _validHugoTarget) uxCreateHugoTextbookButton.Enabled = true;
                    break;
                case ProgramState.ValidHugo:
                    _validHugoTarget = true;
                    uxHugoPath.Text = result;
                    if (_validCodioSource && _validHugoTarget) uxCreateHugoTextbookButton.Enabled = true;
                    break;
                case ProgramState.ConversionFailure:
                    uxCreateHugoTextbookButton.Enabled = false;
                    uxCodioPath.Text = "";
                    uxHugoPath.Text = "";
                    uxConversionResultBox.Text = result;
                    break;
                case ProgramState.ConversionSuccess:
                    uxConversionResultBox.Text = result;
                    break;
            }
        }

        /// <summary>
        /// handles the click for Create Hugo Textbook by using delegate to controller handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateHugoTextbookButton_Click(object sender, EventArgs e)
        {
            _textbookConversionHandler();
        }
    }
}
