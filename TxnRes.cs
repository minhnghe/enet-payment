using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class TxnRes
    {
        public string Ss { get; set; }
        public Msg Msg { get; set; }
    }

    public class Msg
    {
        public string NetsMid { get; set; }
        public string MerchantTxnRef { get; set; }
        public string MerchantTxnDtm { get; set; }
        public string PaymentType { get; set; }
        public string CurrencyCode { get; set; }
        public string NetsTxnRef { get; set; }
        public string NetsTxnDtm { get; set; }
        public string PaymentMode { get; set; }
        public string MerchantTimeZone { get; set; }
        public string NetsTxnStatus { get; set; }
        public string NetsTxnMsg { get; set; }
        public string NetsAmountDeducted { get; set; }
        public string MaskPan { get; set; }
        public string BankAuthId { get; set; }
        public string StageRespCode { get; set; }
        public string TxnRand { get; set; }
        public string ActionCode { get; set; }
        public string NetsMidIndicator { get; set; }
    }
}