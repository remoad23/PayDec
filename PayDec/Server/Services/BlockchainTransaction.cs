using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PayDec.Server.Model;
using PayDec.Server.Services.Interfaces;
using PayDec.Shared.Model;
using PayDec.Server.Model.Functions;
using Nethereum.Model;
using System.Numerics;
using Account = Nethereum.Web3.Accounts.Account;
using TransferPurchase = PayDec.Server.Model.Functions;
using Purchase = PayDec.Shared.Model.Purchase;
using PayDec.Server.Model.FunctionOutput;

namespace PayDec.Server.Services
{
    public class BlockchainTransaction : IBlockchainTransaction
    {

        private Web3 Web { get; set; }
        private Account Account { get; set; }
        private IConfiguration Configuration { get; set; }

        private string ContractAdress { get; set; } = "0x424126ee7ed5155c38785b2f35ef7fab295a9c85";

        //meine eth adresse vom main account...
        private string HostAdress { get; set; } = "0x3Bf985949Ca2f3cD55a76e10f4FE36151BE7a824";


        public BlockchainTransaction(IConfiguration configuration)
        {
            string url = "https://goerli.infura.io/v3/e126aa853d8248adb96b087e545275f3";
            string privateKey = "0x67ea778afb78f53e4d6e1f4fd499f9aebcc49dff";
            Account = new Account(privateKey);
            Web = new Web3(Account, url);
            Configuration = configuration;
        }

        //should work atleast to get balane
        public async Task<BigInteger> GetBalance(string adress)
        {
            // balance
            var balanceFunction = new Balance();

            var balanceHandler = Web.Eth.GetContractQueryHandler<Balance>();

            BalanceOutput result = await balanceHandler.QueryAsync<BalanceOutput>(ContractAdress, balanceFunction);
            return result.Balance;

        }

        // not working since only uses infura use metamask frontend instead (see paymentsubmit.razor)
        public async Task<bool> Pay(string adress, int amount)
        {
            var transferHandler = Web.Eth.GetContractTransactionHandler<TransferPurchase.Purchase>();
            var transfer = new TransferPurchase.Purchase()
            {
                BuyerAdress = adress,
                Amount = amount,
                FromAddress = HostAdress,
                Gas = 1000000,
                
            };

            //var estimate = await transferHandler.EstimateGasAsync(ContractAdress, transfer);
            //transfer.Gas = estimate.Value;


            var succeeded = false;

            try
            {
                var transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(ContractAdress, transfer);
                succeeded = true;
            }
            catch(Exception e )
            {
                Console.WriteLine(e);
                succeeded = false;
            }

            return succeeded;
        }

    }
}
