using System;
using System.Collections.Generic;
using System.Text;
using NetworkComputers;
using System.Threading;

namespace Proxy1
{
    class Network
    {
        private static DomainCollection myDomains;
        static List<string> dl = new List<string>();
        static List<string> cl = new List<string>();

        public static List<string> LoadDomainList()
        {
            dl.Clear();
            Mutex loadMutex = new Mutex(false, "NetworkComputer");
            loadMutex.WaitOne();
            myDomains = new DomainCollection();
            myDomains.Refresh();
            for (int i = 0; i < myDomains.Count; i++)
            {
                dl.Add(myDomains[i].Name);
            }
            loadMutex.ReleaseMutex();
            return dl;
        }

        public static List<string> LoadComputerList(int dn)
        {
            Mutex loadMutex = new Mutex(false, "NetworkComputer");
            cl.Clear();
            if (loadMutex.WaitOne(0, false) == true)
            {
                try
                {
                    if (myDomains[dn].Workstations.Count > 0)
                    {
                        for (int j = 0; j < myDomains[dn].Workstations.Count; j++)
                        {
                            cl.Add(myDomains[dn].Workstations[j].Name);
                        }
                    }
                }
                catch (Exception ee) { }

                loadMutex.ReleaseMutex();                
            }
            return cl;
        }
    }
}
