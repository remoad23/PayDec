using System.ComponentModel.DataAnnotations;

namespace PayDec.Shared.Model
{
    public class Item
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        [Required]
        public ushort Price {get;set;}
        [Required]
        public ushort Stock { get; set; }
    }
}
