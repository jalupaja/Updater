using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Updater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var updater = new Form1();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                updater.url = args[1].ToString();

                foreach (string cmd in args)
                {
                    if (cmd == updater.url)
                    {

                    }
                    else if (cmd == "silent")
                    {
                        updater.silent = true;
                    }
                    else
                    {
                        try
                        {
                            if (Directory.Exists(cmd))
                            {
                                updater.destinationPath = cmd;
                            }
                            else
                            {
                                throw new DirectoryNotFoundException();
                            }
                        }
                        catch (Exception)
                        {
                            throw new DirectoryNotFoundException();
                        }
                    }
                }

            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(updater);
        }
    }
}
