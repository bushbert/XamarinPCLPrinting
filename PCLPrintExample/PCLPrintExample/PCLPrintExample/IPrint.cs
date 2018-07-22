using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCLPrintExample
{
    public interface IPrint
    {
        void Print(byte[] content);
    }
    //This is interface is defined in shared code. You then need a clss in each project whicj
    //Implements this interface. 
}
