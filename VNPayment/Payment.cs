using System;
using VNPayment;
using VNPayment.Model;

namespace VNPayment
{
    public class Payment
    {
        public string Pay(InputParameter input)
        {
            VnPayLib vnpay = new VnPayLib();

            vnpay.AddRequestData("vnp_Amount", (Convert.ToDouble(input.Amount) * 100).ToString());
            vnpay.AddRequestData("vnp_Command", input.Vnp_Command);
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", input.Vnp_CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", "103.90.232.190");
            vnpay.AddRequestData("vnp_Locale", input.Locale);
            vnpay.AddRequestData("vnp_OrderInfo", input.OrderInfo);
            vnpay.AddRequestData("vnp_ReturnUrl", input.Vnp_Returnurl);
            vnpay.AddRequestData("vnp_TmnCode", input.Vnp_TmnCode);
            //vnpay.AddRequestData("vnp_TxnRef", (new DateTimeOffset(DateTime.Now)).ToUniversalTime().ToString());
            vnpay.AddRequestData("vnp_Version", input.Vnp_Version);

            string paymentUrl = vnpay.CreateRequestUrl(input.Vnp_Url, input.Vnp_HashSecret);
            return paymentUrl;
        }
    }
}