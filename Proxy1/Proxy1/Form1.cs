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
using System.Threading;
using NetworkComputers;
using System.Net;

namespace Proxy1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static DataTable dt = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("DC Name");
            dt.Columns.Add("IP Address");
            dt.Columns.Add("Port No");
            dt.Columns.Add("Status");

            DataRow dr = dt.NewRow();
            dr[0] = "localhost";
            dr[1] = "127.0.0.1";
            dr[2] = "12345";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "localhost";
            dr[1] = "127.0.0.1";
            dr[2] = "12346";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "localhost";
            dr[1] = "127.0.0.1";
            dr[2] = "12347";
            //dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[2].ReadOnly = true;

            try
            {
                HttpChannel hh = new HttpChannel();
                ChannelServices.RegisterChannel(hh, false);
            }
            catch (Exception ee) { }

        }

        private void addNewDataCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDC dc = new AddDC();
            DialogResult dr1 = dc.ShowDialog();

            if (dr1 == System.Windows.Forms.DialogResult.OK)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dc.DCName;
                dr[1] = dc.IPAddress;
                dr[2] = dc.PortNo;
                dt.Rows.Add(dr);
            }
        }

        private void deleteAllDataCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
        }

        public static DataTable dt2 = null;

        private void button2_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "Status='Running'";
            dt2 = dv.ToTable();

            List<ps_interface> list = new List<ps_interface>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                string ip = dt2.Rows[i]["ip address"].ToString();
                string port = dt2.Rows[i]["port no"].ToString();
                string url = "http://" + ip + ":" + port + "/abcd";

                ps_interface pp = (ps_interface)Activator.GetObject(typeof(ps_interface), url);
                list.Add(pp);
            }

            SendRequest2 ss = new SendRequest2(list, dt2);
            ss.Show();
        }

        List<string> iplist = new List<string>();
        string getIP(string cn)
        {
            string ip = "";
            iplist.Clear();
            try
            {
                IPHostEntry ip2 = Dns.GetHostEntry(cn);

                for (int i = 0; i < ip2.AddressList.Length; i++)
                    iplist.Add(ip2.AddressList[i].ToString());
            }
            catch (Exception ee)
            {

            }

            if (iplist.Count > 0)
            {
                ip = iplist[iplist.Count - 1];
            }
            return ip;
        }

        List<string> dlist;
        List<string> clist = new List<string>();

        private void LoadDomainList()
        {
            dlist = Network.LoadDomainList();
            clist.Clear();
            
            for (int i = 0; i < dlist.Count; i++)
            {
                List<string> cl = Network.LoadComputerList(i);
                for (int j = 0; j < cl.Count; j++)
                {
                    if (!clist.Contains(cl[j]))
                        clist.Add(cl[j]);
                }
            }

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings ss = new Settings();
            ss.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            LoadDomainList();

            dt.Rows.Clear();
            for (int i = 0; i < clist.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = clist[i];
                dr[1] = getIP(clist[i]);
                dr[2] = "12345";
                dr[3] = "Not Running";
                dt.Rows.Add(dr);

                try
                {
                    string ip = dt.Rows[i]["ip address"].ToString();
                    string port = dt.Rows[i]["port no"].ToString();
                    string url = "http://" + ip + ":" + port + "/abcd";

                    ps_interface pp = (ps_interface)Activator.GetObject(typeof(ps_interface), url);
                    int sta = pp.IsDSRunning();
                    if (sta == 1)
                        dt.Rows[i][3] = "Running";
                }
                catch (Exception ee) { }
            }

            if (dt.Rows.Count > 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;

            button1.Enabled = true;
        }

    }
}
