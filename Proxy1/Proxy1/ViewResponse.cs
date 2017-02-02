using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pslib1;

namespace Proxy1
{
    public partial class ViewResponse : Form
    {
        public ViewResponse()
        {
            InitializeComponent();
        }

        List<string> list = null;
        List<ps_interface> list2 = null;

        public ViewResponse(List<string> _list, List<ps_interface> _list2)
        {
            InitializeComponent();
            list = _list;
            list2 = _list2;
        }

        private void ViewResponse_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            ps_interface pp = list2[comboBox1.SelectedIndex];
            if (pp != null)
            {
                int[] a = pp.getInputA();
                int[] b = pp.getInputB();
                int[] c = pp.getOutputC();

                for (int i = 0; i < c.Length; i++)
                {
                    int cnt = i + 1;
                    listView2.Items.Add(cnt.ToString());
                    listView2.Items[i].SubItems.Add(a[i].ToString());
                    listView2.Items[i].SubItems.Add(b[i].ToString());
                    listView2.Items[i].SubItems.Add(c[i].ToString());
                }
            }
            else
                listView2.Items.Clear();
        }
    }
}
