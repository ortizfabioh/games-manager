using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength=3, ErrorMessage="The name of the game must contain between 3 and 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The name of the developer must contain between 1 and 100 characters")]
        public string Developer { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "The price must have a minimum of R$1 and a maximum of R$1000")]
        public double Price { get; set; }
    }
}
