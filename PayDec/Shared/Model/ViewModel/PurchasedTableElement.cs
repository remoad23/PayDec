using PayDec.Shared.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDec.Shared.Model.ViewModel
{
    public class PurchasedTableElement : IPDObject
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int TotalPrice
        {
            get
            {
                return Amount * Price;
            }
        }
        public long ItemId { get; set; }
    }
}
