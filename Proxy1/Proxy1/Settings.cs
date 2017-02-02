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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int t1 = 1, t2 = 10, t3 = 20, t4 = 30;
            
            try { t1 = Int32.Parse(textBox1.Text); }
            catch (Exception ee) { }

            try { t2 = Int32.Parse(textBox2.Text); }
            catch (Exception ee) { }

            try { t3 = Int32.Parse(textBox3.Text); }
            catch (Exception ee) { }

            try { t4 = Int32.Parse(textBox4.Text); }
            catch (Exception ee) { }

            AppTime.RRUpdate = t1;
            AppTime.AutoReq = t2;
            AppTime.Sleep = t3;
            AppTime.WakeUp = t4;

            this.Dispose();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = AppTime.RRUpdate.ToString();
            textBox2.Text = AppTime.AutoReq.ToString();
            textBox3.Text = AppTime.Sleep.ToString();
            textBox4.Text = AppTime.WakeUp.ToString();
        }
    }
}
