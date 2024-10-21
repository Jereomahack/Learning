using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lightway_Academy_school_fee_application.Models
{
    public class Fees
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Class")]

        [Required]
        public string ClassName { get; set; }

        [Required]
        public string  Session { get; set; }

        [Required]
        public string Term { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Amount { get; set; }
        
    }

}