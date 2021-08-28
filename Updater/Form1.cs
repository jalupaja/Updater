using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace Updater
{
    public partial class Form1 : Form
    {
        public bool silent = false;
        public string url = "";
        public string destinationPath = System.Windows.Forms.Application.ExecutablePath;

        public Form1()
        {
            //try
            //{
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Invalid URL!");
                }
            //}
            //catch (Exception)
            //{
            //    throw new Exception("No Internet Connection!");
            //}

            InitializeComponent();
        }
    }
}
