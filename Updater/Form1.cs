using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Updater
{
    public partial class Form1 : Form
    {
        Point lastPoint;

        public bool silent = false;
        public string url = "";
        public string destinationPath;
        public bool question = false;
        public string[] excludeExt = new string[] {};
        private bool finished;
        private string filename;
        private string tmpFolder;
        private bool close;

        protected override void SetVisibleCore(bool value)
        {
            //if (!this.IsHandleCreated) CreateHandle();
            base.SetVisibleCore(!silent);
            Init();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                backgroundWorker.Dispose();
                Application.ExitThread();
            }
            else if (close)
            {
                Application.ExitThread();
            }
            else
            {
                if (MessageBox.Show("Do you really want to cancel the update?", "Cancel Update?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Application.ExitThread();
                }
                else
                    e.Cancel = true;
            }
            this.Close();
            base.OnFormClosing(e);
        }

        private BackgroundWorker backgroundWorker;

        public Form1()
        {
                InitializeComponent();
        }

        public void Init()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);

            filename = url.Substring(url.LastIndexOf('/') + 1);
            if (filename.Contains("."))
                tmpFolder = Path.Combine(Path.GetTempPath(), $"{filename.Remove(filename.LastIndexOf("."))} Updater");
            else
                tmpFolder = Path.Combine(Path.GetTempPath(), $"{filename} Updater");
            if (Directory.Exists(tmpFolder)) { Cleanup(); }
            Directory.CreateDirectory(tmpFolder);
            if (filename.Contains("."))
                LblTitle.Text = "Updater: " + filename.Substring(0, filename.LastIndexOf('.'));
            else
                LblTitle.Text = filename;

            if (question)
            {
                if (MessageBox.Show("Do you want to install the new update?", "New update available", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    backgroundWorker.Dispose();
                    Application.ExitThread();
                    Application.Exit();
                    return;
                }
            }

            backgroundWorker.RunWorkerAsync();
            ProgBar.Value = 10;
            while (backgroundWorker.IsBusy)
            {
                Thread.Sleep(200);
                if (ProgBar.Value < 90)
                    ProgBar.Increment(1);
                Application.DoEvents();
            }
            while (!finished)
                ;
            backgroundWorker.Dispose();
            backgroundWorker = null;
            if (url.Contains("."))
            {
                if (url.Substring(url.LastIndexOf('.') + 1) == "zip")
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c powershell.exe -windowstyle hidden -executionpolicy bypass -nop -c \"expand-archive -DestinationPath '{tmpFolder}' -Path '{Path.Combine(tmpFolder, filename)}'\"") { CreateNoWindow = false }).WaitForExit();
                    Thread.Sleep(500);
                    File.Delete(Path.Combine(tmpFolder, filename));
                }
            }
            mvFolder(tmpFolder, destinationPath);
            ProgBar.Value = 100;
            Cleanup();
            if (!silent) { MessageBox.Show($"Successfully updated {LblTitle.Text}", "Update finished"); }
            close = true;
            Application.Exit();
        }

        private void mvFolder(string origin, string to)
        {
            //try if process is open
            foreach (string file in Directory.GetFiles(origin))
            {
                if (File.Exists(Path.Combine(to, Path.GetFileName(file))))
                {
                    try
                    {
                        using (FileStream stream = new FileInfo(Path.Combine(to, Path.GetFileName(file))).Open(FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            stream.Close();
                        }
                    }
                    catch (Exception)
                    {
                        bool good = true;
                        if (file.Contains(".") && excludeExt != null)
                        {
                            for (int i = 0; i < excludeExt.Length; i++)
                            {
                                if (file.Substring(file.LastIndexOf('.') + 1).ToLower() == excludeExt[i])
                                {
                                    good = false;
                                }
                            }
                        }
                        if (Path.GetFileName(file) == Path.GetFileName(Application.ExecutablePath))
                            good = false;

                        if (good) { wait4Proc(to, file); }
                        
                    }
                }
            }
            foreach (string folder in Directory.GetDirectories(origin))
            {
                if (Directory.Exists(Path.Combine(to, new DirectoryInfo(folder).Name)))
                    mvFolder(folder, Path.Combine(to, new DirectoryInfo(folder).Name));
            }
            //actually replace files
            
            foreach (string file in Directory.GetFiles(origin))
            {
                if (File.Exists(Path.Combine(to, Path.GetFileName(file))))
                {
                    try
                    {
                        File.Delete(Path.Combine(to, Path.GetFileName(file)));
                    }
                    catch (Exception)
                    {
                        wait4Proc(to, file);
                    }
                }
                bool good = true;
                if (file.Contains(".") && excludeExt != null)
                {
                    for (int i = 0; i < excludeExt.Length; i++)
                    {
                        if (file.Substring(file.LastIndexOf('.') + 1).ToLower() == excludeExt[i])
                        {
                            good = false;
                        }
                    }
                }
                if (Path.GetFileName(file) == Path.GetFileName(Application.ExecutablePath))
                    good = false;

                if (good) { File.Move(file, Path.Combine(to, Path.GetFileName(file))); }
            }
            ProgBar.Increment(1);
            foreach (string folder in Directory.GetDirectories(origin))
            {
                if (Directory.Exists(Path.Combine(to, new DirectoryInfo(folder).Name)))
                    mvFolder(folder, Path.Combine(to, new DirectoryInfo(folder).Name));
                else
                    Directory.Move(folder, Path.Combine(to, new DirectoryInfo(folder).Name));
            }
        }

        private void wait4Proc(string to, string file)
        {
            bool working = false;
            while (!working)
            {
                if (silent)
                {
                    Thread.Sleep(5000);
                    try
                    {
                        using (FileStream stream = new FileInfo(Path.Combine(to, Path.GetFileName(file))).Open(FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            stream.Close();
                        }
                        working = true;
                    }
                    catch (Exception)
                    {
                        working = false;
                    }
                }
                else
                {
                    if (MessageBox.Show($"Cannot access {Path.GetFileName(file)}." + Environment.NewLine + "Please close all programs and press retry", $"Cannot access {Path.GetFileName(file)}", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        try
                        {
                            using (FileStream stream = new FileInfo(Path.Combine(to, Path.GetFileName(file))).Open(FileMode.Open, FileAccess.Read, FileShare.None))
                            {
                                stream.Close();
                            }
                            working = true;
                        }
                        catch (Exception)
                        {
                            working = false;
                        }
                    }
                    else
                    {
                        close = true;
                        Application.Exit();
                        return;
                    }
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, Path.Combine(tmpFolder, filename));
                }
            }
            catch (Exception)
            {
                if (!silent)
                    MessageBox.Show("Invalid Url!", "Invalid Url!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                backgroundWorker.Dispose();
                backgroundWorker = null;
                Application.Exit();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgBar.Value = 90;

            if (e.Error == null)
            {
                finished = true;
            }
            else
            {
                MessageBox.Show(
                    "Failed to download file",
                    "Download failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Cleanup()
        {
            if (Directory.Exists(tmpFolder)){ Directory.Delete(tmpFolder, true); }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
