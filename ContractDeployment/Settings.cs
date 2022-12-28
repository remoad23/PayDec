using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractDeployment
{
    public class Settings
    {
        public static string InfuraUrl { get; set; } = "https://goerli.infura.io/v3/e126aa853d8248adb96b087e545275f3";
        public static string MetaPrivateKey { get; set; } = "391fbc3f6809836e04f7d4210879733f57b3ebef17407429386f7cdf8d55dbc5";


        public static string User_Password { get; set; } = "Retro1992@";
        public static string User_Adress { get; set; } = "0x3Bf985949Ca2f3cD55a76e10f4FE36151BE7a824";

        public static string Adress { get; set; }
        public static string ByteCode { get; set; }
        public static string Abi { get; set; }
    }
}
