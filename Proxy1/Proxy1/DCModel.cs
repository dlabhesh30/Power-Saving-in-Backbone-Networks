using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proxy1
{
    class DCModel
    {
        public string dcno;
        public string dcname;
        public string ipaddress;
        public int port;
        public string status;
        public int req;
        public int res;
        public int sleep;
        public int wakeup;

        public DCModel(string _dcno, string dcn, string ipa, int pno, string sta, int rq, int rs, int sl, int wk)
        {
            dcno = _dcno;
            dcname = dcn;
            ipaddress = ipa;
            port = pno;
            status = sta;
            req = rq;
            res = rs;
            sleep = sl;
            wakeup = wk;
        }

        public string DCNO
        {
            set { dcno = value; }
            get { return dcno; }
        }

        public string DCName
        {
            get { return dcname; }
        }

        public string IPAddress
        {
            get { return ipaddress; }
        }

        public int PortNo
        {
            get { return port; }
        }

        public string Status
        {
            set { status = value; }
            get { return status; }
        }

        public int Request
        {
            set { req = value; }
            get { return req; }
        }

        public int Response
        {
            set { res = value; }
            get { return res; }
        }

        public int SleepCount
        {
            set { sleep = value; }
            get { return sleep; }
        }

        public int WakeupCount
        {
            set { wakeup = value; }
            get { return wakeup; }
        }
    }
}
