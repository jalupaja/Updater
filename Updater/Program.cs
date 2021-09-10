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
        static Form1 updater = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            updater = new Form1();
            updater.destinationPath = Path.GetDirectoryName(Application.ExecutablePath);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                updater.url = args[1].ToString();

                foreach (string cmd in args)
                {
                    if (cmd == updater.url) { }
                    else if (cmd == "silent")
                        updater.silent = true;
                    else if (cmd == "question")
                        updater.question = true;
                    else if (cmd.Contains("EXCLUDE:"))
                        updater.excludeExt = cmd.Replace("EXCLUDE:", "").Split(',');
                    else
                    {
                        try
                        {
                            if (Directory.Exists(cmd)) { updater.destinationPath = cmd; }
                        }
                        catch (Exception)
                        {
                            throw new DirectoryNotFoundException();
                        }
                    }
                }
                Application.Run(updater);
            }
        }
    }
}
