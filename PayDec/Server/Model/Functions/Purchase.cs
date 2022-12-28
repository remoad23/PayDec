using Microsoft.AspNetCore.Mvc;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace PayDec.Server.Model.Functions
{
    [Function("Purchase", "bool")]
    public class Purchase : FunctionMessage
    {
        [Parameter("address", "buyer", 1)]
        public string BuyerAdress { get; set; }

        [Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }
    }
}
