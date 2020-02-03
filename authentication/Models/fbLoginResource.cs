using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authentication.Models
{
    public class fbLoginResource
    {
        [Required]
        [StringLength(255)]
        public string facebookToken { get; set; }
    }
}
