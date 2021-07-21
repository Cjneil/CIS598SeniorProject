﻿using Microsoft.WindowsAPICodePack.Dialogs;
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
        private ConversionController _controller;
        private bool _validHugoTarget;
        private bool _validCodioSource;

        public FileSelection(ConversionController controller)
        {
            InitializeComponent();
            _controller = controller;
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
                _controller.handleViewFileSelection(SelectionState.Codio, selectedFile);
                
            }
            
        }

        /// <summary>
        /// Handles the click event of the Select Target Hugo Directory Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HugoTargetButton_Click(object sender, EventArgs e)
        {
            if(uxConversionResultBox.Text != "")
            {
                uxCodioPath.Text = "";
                uxHugoPath.Text = "";
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
                _controller.handleViewFileSelection(SelectionState.Hugo, selectedFile);
            }
        }

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

        private void CreateHugoTextbookButton_Click(object sender, EventArgs e)
        {
            _controller.handleConvertTextbook();
        }
    }
}
