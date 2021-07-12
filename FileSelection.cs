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
    public partial class FileSelection : Form
    {
        private ConversionController _controller;
        public FileSelection(ConversionController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void CodioSelectButton_Click(object sender, EventArgs e)
        {
            string selectedFile = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.EnsurePathExists = true;
            dialog.EnsureFileExists = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedFile = dialog.FileName;
                updateCodioText(_controller.handleViewFileSelection("CodioFileSelected", selectedFile));
                
            }
            
        }

        private void HugoTargetButton_Click(object sender, EventArgs e)
        {
            string selectedFile = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.EnsurePathExists = true;
            dialog.EnsureFileExists = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedFile = dialog.FileName;
                updateHugoText(_controller.handleViewFileSelection("HugoTargetSelected", selectedFile));
            }
        }

        private void updateCodioText(string newText)
        {
            uxCodioPath.Text = newText;
            if(!newText.Equals("Invalid Directory selected. Codio file structure not present."))
            {
                uxHugoTargetButton.Enabled = true;
            }
        }

        private void updateHugoText(string newText)
        {
            uxHugoPath.Text = newText;
            if (!newText.Equals("Invalid directory selected. Directory is not empty."))
            {
                uxCreateHugoTextbookButton.Enabled = true;
            }
        }

        private void uxCreateHugoTextbookButton_Click(object sender, EventArgs e)
        {
            _controller.handleConvertTextbook();
        }
    }
}
