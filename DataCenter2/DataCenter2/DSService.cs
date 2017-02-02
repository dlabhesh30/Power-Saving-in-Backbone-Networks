using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using pslib1;

namespace DataCenter2
{
    class DSService : MarshalByRefObject
    {
        public static int TotalReq = 0;
        public static int TotalRes = 0;
        public static string status = "N / A";

        public static void initReq(int size)
        {
            TotalReq += size;
        }

        public int IsDSRunning()
        {
            return 1;
        }

        public int addReq(int size)
        {
            TotalReq += size;
            return 1;
        }

        public string getDCStatus()
        {
            return status;
        }

        public int getTotalReq()
        {
            return TotalReq;
        }

        public int getTotalRes()
        {
            return TotalRes;
        }

    }
}
