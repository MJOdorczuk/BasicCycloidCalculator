using BCC.Core;
using System;
using System.Windows.Forms;

namespace BCC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = Model.Initialize();
            Application.Run(form);
        }
    }
}
