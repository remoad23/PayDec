using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace PayDec.Server.Model.Functions
{
    [Function("getBalance", "uint256")]
    public class Balance : FunctionMessage
    {
        
    }
}
