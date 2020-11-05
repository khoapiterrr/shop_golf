using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPayment.Model
{
    public class InputParameter
    {
        public string BankCode { get; set; }
        public string Locale { get; set; }
        public string Amount { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public string Vnp_Returnurl { get; set; }
        public string Vnp_Url { get; set; }
        public string Vnp_TmnCode { get; set; }
        public string Vnp_HashSecret { get; set; }
        public string Vnp_Version { get; set; }
        public string Vnp_Command { get; set; }
        public string Vnp_CurrCode { get; set; }
    }
}