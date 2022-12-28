using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace PayDec.Server.Model.FunctionOutput
{
    [FunctionOutput]
    public class BalanceOutput
    {
        [Parameter("uint", "balance", 1)]
        public BigInteger Balance { get; set; }
    }
}
