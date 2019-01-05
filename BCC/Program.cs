using BCC.Core;
using BCC.Interface_View.MetroInterface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

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
            var form = Initializator.Initialize();
            Application.Run(form);
        }
    }
}
