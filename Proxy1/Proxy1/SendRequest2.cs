using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pslib1;
using System.Threading;

namespace Proxy1
{
    public partial class SendRequest2 : Form
    {
        public SendRequest2()
        {
            InitializeComponent();
        }

        DataTable dt2 = null;
        List<ps_interface> listObj = null;
        
        public SendRequest2(List<ps_interface> _list, DataTable _dt)
        {
            InitializeComponent();
            timer1.Interval = AppTime.RRUpdate * 1000;
            timer3.Interval = AppTime.AutoReq * 1000;
            
            dt = _dt;
            listObj = _list;
        }

        DataTable dt = null;

        private void SendRequest_Load(object sender, EventArgs e)
        {
            ListView.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string dcno = "DC " + (i + 1);
                string dcname = dt.Rows[i]["dc name"].ToString();
                string ip = dt.Rows[i]["ip address"].ToString();
                string port = dt.Rows[i]["port no"].ToString();
                listView1.Items.Add(dcno);
                
                int c = listView1.Items.Count - 1;
                listView1.Items[c].SubItems.Add(dcname);
                listView1.Items[c].SubItems.Add(ip);
                listView1.Items[c].SubItems.Add("N / A");
                listView1.Items[c].SubItems.Add("0/0");
                listView1.Items[c].SubItems.Add("0");
                listView1.Items[c].SubItems.Add("0");
                listView1.Items[c].SubItems.Add("0");
            }


            timer1.Enabled = true;
            //timer2.Enabled = true;
            timer3.Enabled = true;
            timer4.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.FocusedItem;
            if (item != null)
            {
                string status = item.SubItems[3].Text;
                if (status == "Hibernate")
                {
                    string dcname = listView1.Items[lowIndex].SubItems[1].Text;
                    DialogResult dr = MessageBox.Show("Your request has not been sent, bocz you have selected a hibernated system!\n\nNote: Your request can be redirected to '" + dcname + "', Are you sure to redirect?", "Invalid DC Selection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        sendReq(lowIndex);
                    }
                }
                else
                {
                    sendReq(item.Index);
                }
            }
            else
                MessageBox.Show("Pls select an item from list");
        }

        void sendReq(int low)
        {
            int size = 0;
            size = Int32.Parse(textBox1.Text);
            int ind = low;

            int[] listA = new int[size];
            int[] listB = new int[size];

            for (int i = 0; i < size; i++)
            {
                Random rr = new Random();

                int a = rr.Next(1, 100);
                Thread.Sleep(10);
                int b = rr.Next(1, 200);

                listA[i] = a;
                listB[i] = b;
            }

            try{
                if (listObj[ind] != null)
                    listObj[ind].addReq(listA, listB);
            }
            catch (Exception ee) { listObj[ind] = null; }
        }

        int lowIndex = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int[] nos = null;
                int k = 0;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    string status = listView1.Items[i].SubItems[3].Text;
                    if (status != "Hibernate")
                        k++;
                }

                nos = new int[k];
                
                for (int i = 0; i < listObj.Count; i++)
                {
                    string status = listView1.Items[i].SubItems[3].Text;
                    if (status != "Hibernate")
                    {
                        if (listObj[i] != null)
                        {
                            listView1.Items[i].SubItems[3].Text = listObj[i].getDCStatus();

                            int req = 0;

                            try { req = listObj[i].getTotalReq(); }
                            catch (Exception ee) { listObj[i] = null; }

                            int res = 0;
                            try { res = listObj[i].getTotalRes(); }
                            catch (Exception ee) { listObj[i] = null; }

                            listView1.Items[i].SubItems[4].Text = res + " / " + req;

                            int bal = req - res;
                            nos[i] = bal;

                            listView1.Items[i].SubItems[5].Text = bal.ToString();
                        }
                    }
                }

                int min = nos[0], max = nos[0];
                int ind = 0, ind2 = 0;

                for (int i = 1; i < nos.Length; i++)
                {
                    if (nos[i] > max)
                    {
                        max = nos[i];
                        ind2 = i;
                    }
                    else if (nos[i] < min)
                    {
                        min = nos[i];
                        ind = i;
                    }
                }

                lowIndex = ind;
                string name1 = listView1.Items[ind].Text;
                string name2 = listView1.Items[ind2].Text;

                //label2.Text = "Lowest -> " + name1 + "   ,   Highest -> " + name2;
                label5.Text = name1;
                label7.Text = name2;
            }
            catch (Exception ee) 
            {
                //label2.Text = "Start DataCenter"; this.Dispose(); 
            }
        }


        //  Auto Request Send and Redirection

        private void timer3_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //timer4.Enabled = false;

            Random rr = new Random();
            int dc = rr.Next(0, listView1.Items.Count);

            ListViewItem item = listView1.Items[dc];
            if (item != null)
            {
                int size = rr.Next(1, 11);
                int ind = item.Index;

                int[] listA = new int[size];
                int[] listB = new int[size];

                for (int i = 0; i < size; i++)
                {
                    int a = rr.Next(1, 100);
                    Thread.Sleep(10);
                    int b = rr.Next(1, 200);

                    listA[i] = a;
                    listB[i] = b;
                }
                
                string name1 = listView1.Items[ind].Text;
                
                if (ind != lowIndex)
                    ind = lowIndex;

                string name2 = listView1.Items[ind].Text;

                if (listView1.Items[ind].SubItems[3].Text == "Hibernate")
                {
                    ind = lowIndex;
                    name2 = listView1.Items[lowIndex].Text;
                }

                try{
                    if (listObj[ind] != null)
                        listObj[ind].addReq(listA, listB);
                }
                catch (Exception ee) { listObj[ind] = null; }

                listView3.Items[0].SubItems[1].Text = name1;
                listView3.Items[1].SubItems[1].Text = size.ToString();
                listView3.Items[2].SubItems[1].Text = name2;
                listView3.Update();
            }

            //timer1.Enabled = true;
            //timer4.Enabled = true;
        }

        //  Sleep Time Count

        private void timer4_Tick(object sender, EventArgs e)
        {            
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string st = listView1.Items[i].SubItems[3].Text;
                if (st == "Waiting")
                {
                    string tcnt = listView1.Items[i].SubItems[6].Text;
                    int ct = Int32.Parse(tcnt);
                    ct++;
                    listView1.Items[i].SubItems[6].Text = ct.ToString();

                    if (ct >= AppTime.Sleep)
                    {                        
                        listView1.Items[i].SubItems[3].Text = "Hibernate";
                        listView1.Items[i].SubItems[6].Text = "0";

                        //Thread.Sleep(1000);
                        try
                        {
                            if (listObj[i] != null)
                            {
                                listObj[i].setDCSleep(0, AppTime.WakeUp);
                                listObj[i] = null;
                                Thread.Sleep(1000);
                            }

                            MessageBox.Show("DC is going to sleep mode");
                        }
                        catch (Exception ee) { listObj[i] = null; }
                    }
                }
                else if (st == "Running")
                {
                    listView1.Items[i].SubItems[6].Text = "0";
                    listView1.Items[i].SubItems[7].Text = "0";
                }
                else if (st == "Hibernate")
                {
                    string tcnt = listView1.Items[i].SubItems[7].Text;
                    int ct = Int32.Parse(tcnt);
                    ct++;
                    listView1.Items[i].SubItems[7].Text = ct.ToString();

                    if (ct >= AppTime.WakeUp)
                    {
                        listView1.Items[i].SubItems[3].Text = "Waiting";
                        listView1.Items[i].SubItems[6].Text = "0";
                        listView1.Items[i].SubItems[7].Text = "0";
                    }
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string item = listView1.Items[i].SubItems[1].Text;
                list.Add(item);
            }

            ViewResponse vv = new ViewResponse(list, listObj);
            vv.ShowDialog();
        }

        
    }
}
