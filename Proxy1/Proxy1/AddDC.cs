using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using pslib1;

namespace Proxy1
{
    public partial class AddDC : Form
    {
        public AddDC()
        {
            InitializeComponent();
        }

        public string IPAddress = "";
        public string DCName = "";
        public int PortNo = -1;
        public string Status = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                IPAddress = textBox1.Text;
                DCName = textBox2.Text;
                PortNo = Int32.Parse(textBox3.Text);
                Status = "Running";
            }

        }

        bool isConnect = false;
       
        private void button2_Click(object sender, EventArgs e)
        {
            isConnect = false;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ps_interface cc1 = (ps_interface)Activator.GetObject(typeof(ps_interface), "http://" + textBox2.Text + ":" + textBox3.Text + "/abcd");
                int st = cc1.IsDSRunning();
                
                this.Cursor = Cursors.Default;
                if (st == 1)
                {
                    isConnect = true;
                    label5.Text = "Status : Running";
                    button1.Enabled = true;
                }
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                label5.Text = ee.Message;
            }

            label5.Visible = true;
        }
    }
}
