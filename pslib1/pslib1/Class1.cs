using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pslib1
{
    public interface ps_interface
    {        
        int IsDSRunning();
        int addReq(int []list1, int []list2);
        int getTotalReq();
        int getTotalRes();
        string getDCStatus();

        int[] getInputA();
        int[] getInputB();
        int[] getOutputC();

        int setDCSleep(int mode, int sec);
    }
}
