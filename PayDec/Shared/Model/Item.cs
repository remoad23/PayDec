using PayDec.Shared.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PayDec.Shared.Model
{
    public class Item : IPDObject

    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        [Required]
        public short Price {get;set;}
        [Required]
        public short Stock { get; set; }

        public string ItemImage { get; set; } = "";
    }
}
