using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class CardInfoModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CVV { get; set; }
        public string Amount { get; set; }
    }
}