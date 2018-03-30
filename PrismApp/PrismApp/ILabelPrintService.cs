using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp
{
    public interface ILabelPrintService
    {
		Task Print(string zpl, string address);
    }
}
