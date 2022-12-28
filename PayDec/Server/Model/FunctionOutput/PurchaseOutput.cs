using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDec.Server.Model.FunctionOutput
{
    [FunctionOutput]
    public class PurchaseOutput : IFunctionOutputDTO
    {
        [Parameter("bool", "success", 1)]
        bool Success { get; set; }
    }
}
