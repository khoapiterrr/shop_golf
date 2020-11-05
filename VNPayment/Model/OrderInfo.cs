using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPayment.Model
{
    public class OrderInfo
    {
        public decimal OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderDescription { get; set; }
        public string BankCode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Vnp_TransactionNo { get; set; }
        public string Vpn_Message { get; set; }
        public string Vpn_TxnResponseCode { get; set; }
    }
}