using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lightway_Academy_school_fee_application.Models
{
    public class SchoolFee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name Of Student")]
        public string NameOfStudent { get; set; }

        [Required]
        [Display(Name = "ClassTerm")]
        public string ClassTerm { get; set; }

        [Required]
        [Display(Name = "Term")]
        public string Term { get; set; }

        [Required]
        [Display(Name = "Session")]
        public string Session { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString ="{0:N}")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(14, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "RRR")]
        public string RRR { get; set; }

        public string Statues { get; set; }

        [Display(Name = "Payment Order Id")]
        public string PaymentOrderId { get; set; }

        public decimal ProcessingFee { get; set;}

        [Display(Name = "Date Of Payment")]
        [DataType(DataType.Date)]
        public DateTime DateOfPayment { get; set; }
    }
}