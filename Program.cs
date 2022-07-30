using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoLCD16x2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "MainProcessor", out createdNew))
            {
                if (createdNew)
                {
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());

                }
                else
                {
                    MessageBox.Show("The Application Is Already Running", "i2cDisplay(16x2)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
        }
    }
}
