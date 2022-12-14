using PayDec.Shared.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayDec.Shared.Model
{
    public class Purchase : IPDObject
    {
        [Key]
        public long Id { get; set; } = 0;
        [Required]
        [ForeignKey("BoughtItem")]
        public long BoughtItemId { get; set; } = 0;
        [Required]
        public string BuyerAdress { get; set; } = "";

        public int Amount { get; set; }

        public int Price { get; set; }
        [NotMapped]
        public int TotalPrice
        {
            get
            {
                return Amount * Price;     
            }
        }
        public Item? BoughtItem { get; set; } = null;
    }
}
