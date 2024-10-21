using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lightway_Academy_school_fee_application.Models
{
    public class ClassTerm
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Class Name")]
        public string ClassName  { get; set; }


    }
}