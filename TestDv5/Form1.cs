using DocsVision.BackOffice.CardLib.CardDefs;
using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.ObjectModel.Services;
using DocsVision.DocumentsManagement.ObjectModel.Services;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.ObjectManager.Rest;
using DocsVision.Platform.ObjectManager.SystemCards;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDv5
{
    public partial class Form1 : Form
    {
        bool working = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var cllient = new MyClient();
            cllient.UseDefaultCredentials = true;

            
            var from = DateTime.Now;
            var countPerSecond = 0;
            working = true;
            while (working)
            {
                var content = cllient.DownloadString(textBoxAddress.Text);
                countPerSecond++;
                if ((DateTime.Now - from).TotalSeconds >= 1)
                {
                    this.labelRPS.Text = countPerSecond.ToString();
                    countPerSecond = 0;
                    from = DateTime.Now;
                }
                Application.DoEvents();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            working = false;
        }
    }

    class MyClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.KeepAlive = true;
            return request;
        }
    }
}

