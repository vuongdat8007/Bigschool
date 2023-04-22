using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class BankingInfo
    {
        [Key, ForeignKey("CBNV")]
        public string CBNVId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string Branch { get; set; }
        public string SwiftCode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        
        public virtual CBNV CBNV { get; set; }
    }

    public enum PaymentMethod
    {
        BankWire,
        Cash
    }
}