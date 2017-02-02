using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proxy1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            label1.Text = i.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.SetSuspendState(PowerState.Suspend, false, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WakeUP wup = new WakeUP();
            wup.Woken += WakeUP_Woken;
            DateTime dd = DateTime.Now;
            dd.AddSeconds(10);
            wup.SetWakeUpTime(dd);
            Application.SetSuspendState(PowerState.Suspend, false, false);
        }


        private void WakeUP_Woken(object sender, EventArgs e)
        {

        }

    }
}
