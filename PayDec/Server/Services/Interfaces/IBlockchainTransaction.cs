using PayDec.Shared.Model;
using System.Numerics;

namespace PayDec.Server.Services.Interfaces
{
    public interface IBlockchainTransaction
    {
        public async Task<bool> Pay(string adress, int amoun) => throw new NotImplementedException();
        public async Task<BigInteger> GetBalance(string adress) => throw new NotImplementedException();

    }
}
