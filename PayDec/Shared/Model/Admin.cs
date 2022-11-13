using PayDec.Shared.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayDec.Shared.Model
{
    public class Admin : IPDObject
    {
        [Key]
        public long Id { get; set; } = 0;
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
    }
}
