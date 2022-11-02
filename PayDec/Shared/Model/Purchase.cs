using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDec.Shared.Model
{
    public class Purchase
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [ForeignKey("BoughtItem")]
        public long BoughtItemId { get; set; }
        [Required]
        public string BuyerAdress { get; set; }
        public Item BoughtItem { get; set; }
    }
}
