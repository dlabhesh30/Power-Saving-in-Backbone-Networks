using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using pslib1;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace DataCenter2
{
    class DSService2 : MarshalByRefObject, ps_interface
    {
        public static string status = "N / A";

        public static List<Int32> listA = null;
        public static List<Int32> listB = null;
        public static List<Int32> listC = null;

        public static void initReq(int size)
        {
            listA = new List<int>();
            listB = new List<int>();
            listC = new List<int>();

            for (int i = 0; i < size; i++)
            {
                Random rr = new Random();

                int a = rr.Next(1, 100);
                Thread.Sleep(10);
                int b = rr.Next(1, 200);

                listA.Add(a);
                listB.Add(b);
            }

        }

        public int IsDSRunning()
        {
            return 1;
        }

        public int addReq(int[] list1, int[] list2)
        {
            if (status == "Waiting")
            {
                //listA.Clear();
                //listB.Clear();
                //listC.Clear();
            }

            for (int i = 0; i < list1.Length; i++)
            {
                int a = list1[i];
                int b = list2[i];
                
                listA.Add(a);
                listB.Add(b);
            }

            return 1;
        }

        public int[] getInputA()
        {
            return listA.ToArray();
        }

        public int[] getInputB()
        {
            return listB.ToArray();
        }

        public int[] getOutputC()
        {
            return listC.ToArray();
        }

        public string getDCStatus()
        {
            return status;
        }

        public int getTotalReq()
        {
            return listA.Count;
        }

        public int getTotalRes()
        {
            return listC.Count;
        }

        public int setDCSleep(int mode, int sec)
        {
            BackgroundWorker bb = new BackgroundWorker();
            bb.DoWork += new DoWorkEventHandler(bb_DoWork);
            bb.RunWorkerAsync(sec);
            return 1;
        }

        void bb_DoWork(object sender, DoWorkEventArgs e)
        {
            int sec = (int)e.Argument;
            string path = System.Windows.Forms.Application.StartupPath;
            string ft = path + "\\wt.txt";
            StreamWriter sw = new StreamWriter(ft);
            sw.Write(sec);
            sw.Close();
            string wp = path + "\\dc_wakeup.exe";
            Thread.Sleep(1000);
            Process.Start(wp);
        }

    }
}
