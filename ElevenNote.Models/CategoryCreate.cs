using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryCreate
    {[Required]
        [MinLength(5, ErrorMessage = "Please enter at least 5 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string CategoryTitle { get; set; }
    }
}