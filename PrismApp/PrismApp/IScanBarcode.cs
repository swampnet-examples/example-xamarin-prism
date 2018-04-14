using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp
{
    public interface IScanBarcode
    {
		Task<string> Scan();
    }
}
