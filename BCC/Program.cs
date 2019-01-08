using BCC.Core;
using BCC.Interface_View.MetroInterface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace BCC
{
    public class Dummy
    {
        public int DummyProperty { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(Initializator.Initialize());
        }
    }
}
