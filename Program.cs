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
            FileSelection mainGUI = new FileSelection(controller);
            controller.RegisterLoginObserver(mainGUI.UpdateGUI);
            Application.Run(mainGUI);
        }
    }
}
