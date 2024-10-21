using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lightway_Academy_school_fee_application.Models
{
    public class RemitaResponse
    {
        public string orderId { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}