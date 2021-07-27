using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodioToHugoConverter
{
    /// <summary>
    /// Used to updateGUI when necessary. Didn't end up used much since GUI remained simple.
    /// </summary>
    /// <param name="result">String representing the result. Just used to update text field in GUI</param>
    public delegate void GUIObserver(ProgramState state, string result);

    /// <summary>
    /// Delegate for controller handler for File selection by UI
    /// </summary>
    /// <param name="state">Which file type is being selected, Codio or Hugo basically</param>
    /// <param name="file">The path to the file</param>
    public delegate void FileSelectionDelegate(SelectionState state, string file);

    /// <summary>
    /// UI handler for converting the textbook
    /// </summary>
    public delegate void ConvertTextbookDelegate();


    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            ConversionController controller = new ConversionController();
            FileSelection mainGUI = new FileSelection(controller.handleViewFileSelection, controller.handleConvertTextbook);
            controller.RegisterLoginObserver(mainGUI.UpdateGUI);
            Application.Run(mainGUI);
        }
    }
}
