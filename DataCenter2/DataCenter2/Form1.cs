using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace DataCenter2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        HttpChannel hh = null;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int port = Int32.Parse(txtPort.Text);
                hh = new HttpChannel(port);                
                ChannelServices.RegisterChannel(hh, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(DSService), "abcd", WellKnownObjectMode.Singleton);
                lblOutput.Text = "Data Center Initialized";
                int size = Int32.Parse(txtMinReq.Text);
                DSService.TotalReq = size;
                button1.Enabled = false;
                timer1.Enabled = true;
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DSService.TotalReq > listView1.Items.Count)
            {
                Random rr = new Random();
                int a = rr.Next(1, 100);
                int b = rr.Next(1, 200);
                int c = a + b;

                int cnt = listView1.Items.Count + 1;
                listView1.Items.Add(cnt.ToString());
                listView1.Items[cnt - 1].SubItems.Add(a.ToString());
                listView1.Items[cnt - 1].SubItems.Add(b.ToString());
                listView1.Items[cnt - 1].SubItems.Add(c.ToString());

                DSService.TotalRes++;
                DSService.status = "Running";

                lblOutput.Text = DSService.TotalRes + " / " + DSService.TotalReq;
            }
            else
            {
                DSService.status = "Sleep";
            }
        }
    }
}
