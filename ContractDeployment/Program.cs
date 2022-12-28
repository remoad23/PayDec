using ContractDeployment;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Numerics;
using EthContract = Nethereum.Contracts;


Process cmd = new Process();

File.Delete("C:\\Users\\Patrick\\source\\repos\\PayDec\\ContractDeployment\\Payment_sol_Payment.abi");
File.Delete("C:\\Users\\Patrick\\source\\repos\\PayDec\\ContractDeployment\\Payment_sol_Payment.bin");

Console.WriteLine("Compiling solidity file and moving output to other location...");

cmd.StartInfo.FileName = "cmd.exe";
cmd.StartInfo.RedirectStandardInput = true;
cmd.StartInfo.UseShellExecute = false;

cmd.Start();

using (StreamWriter sw = cmd.StandardInput)
{
    if (sw.BaseStream.CanWrite)
    {
        sw.WriteLine("CMD.exe\", \"solcjs --bin --abi Payment.sol");
        sw.WriteLine("cd C:\\Users\\Patrick\\source\\repos\\PayDec\\PayDec\\Server\\Contracts");
        sw.WriteLine("solcjs --bin --abi Payment.sol");
        sw.WriteLine("move Payment_sol_Payment.abi C:\\Users\\Patrick\\source\\repos\\PayDec\\ContractDeployment");
        sw.WriteLine("move Payment_sol_Payment.bin C:\\Users\\Patrick\\source\\repos\\PayDec\\ContractDeployment");
    }
}


while(!File.Exists("C:\\Users\\Patrick\\source\\repos\\PayDec\\ContractDeployment\\Payment_sol_Payment.abi") 
    && !File.Exists("C:\\Users\\Patrick\\source\\repos\\PayDec\\ContractDeployment\\Payment_sol_Payment.bin"))
{
    Console.WriteLine("Waiting for old files to be deleted");
    Thread.Sleep(5000);
    
}

Settings.Abi = File.ReadAllText(@"C:\Users\Patrick\source\repos\PayDec\ContractDeployment\Payment_sol_Payment.abi");

using (StreamReader r = new StreamReader(@"C:\Users\Patrick\source\repos\PayDec\ContractDeployment\Payment_sol_Payment.bin"))
{
    Settings.ByteCode = r.ReadToEnd();
}


Account account = new Account(Settings.MetaPrivateKey);

Web3 web3 = new(account,Settings.InfuraUrl);

web3.Eth.DeployContract.TransactionManager.DefaultGasPrice = BigInteger.Parse("3000000");
web3.Eth.DeployContract.TransactionManager.DefaultGas = BigInteger.Parse("300000");

string transactionHash = await web3.Eth.DeployContract.SendRequestAsync(Settings.Abi, Settings.ByteCode, Settings.User_Adress);

Console.WriteLine("Deployment sent");

var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

while (receipt == null)
{
    Console.WriteLine("waiting for deployment");
    Thread.Sleep(5000);
    receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
}

// "0xe2b47811eb806132ed467b427d71ce39efb15bdf"
var contractAddress = receipt.ContractAddress;

Console.WriteLine("Contract Adress generated:  " + contractAddress);
Thread.Sleep(60000);