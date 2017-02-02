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
    public partial class Form2 : Form
    {
        public Form2()
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
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(DSService2), "abcd", WellKnownObjectMode.Singleton);
                lblOutput.Text = "Data Center Initialized";
                int size = Int32.Parse(txtMinReq.Text);
                DSService2.initReq(size);
                button1.Enabled = false;
                timer1.Enabled = true;
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DSService2.listA.Count > listView1.Items.Count)
            {
                int cnt = listView1.Items.Count;

                int a = DSService2.listA[cnt];
                int b = DSService2.listB[cnt];
                int c = a + b;
                cnt++;
                
                listView1.Items.Add(cnt.ToString());
                listView1.Items[cnt - 1].SubItems.Add(a.ToString());
                listView1.Items[cnt - 1].SubItems.Add(b.ToString());
                listView1.Items[cnt - 1].SubItems.Add(c.ToString());

                DSService2.listC.Add(c);
                DSService2.status = "Running";

                lblOutput.Text = DSService2.listC.Count + " / " + DSService2.listA.Count;
            }
            else if (DSService2.listA.Count == listView1.Items.Count)
            {
                DSService2.status = "Waiting";
                //listView1.Items.Clear();
            }
            
        }
    }
}
