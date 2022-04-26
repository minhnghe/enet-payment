using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1
{


    public class PaymentResponseHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<Tuple<string, string>> nv = new List<Tuple<string, string>>();
            foreach (string f in context.Request.Params.AllKeys)
            {
                nv.Add(new Tuple<string, string>(f, context.Request.Params[f]));
            }
            string formData = JsonConvert.SerializeObject(nv);
            formData = "Params Data: " + formData;
            StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
            string requestFromPost = reader.ReadToEnd();
            formData += " Body: " + requestFromPost;

            try
            {
                string hmac = context.Request.Params["HTTP_HMAC"];

                if (string.IsNullOrEmpty(requestFromPost))
                {
                    context.Response.End();
                }

                if (string.IsNullOrEmpty(hmac))
                {
                    context.Response.End();
                }

                string secretKey = ConfigurationManager.AppSettings[Constants.AppSettingKeys.ENETS_Secret];
                string calculatedHMAC = GetHmac(requestFromPost, secretKey);
                if (calculatedHMAC != hmac)
                {
                    context.Response.End();
                }

                Dictionary<string, object> payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestFromPost);
                if (payload["msg"] == null)
                {
                    context.Response.End();
                }

                eNetsPaymentMessage message = JsonConvert.DeserializeObject<eNetsPaymentMessage>(payload["msg"].ToString());
                string salesorderno = message.merchantTxnRef;
                Console.WriteLine(message);
            }
            catch (Exception)
            {
            }
        }

        public bool IsReusable => false;

        public string GetHmac(string request_payload, string secret_key)
        {
            string payload = request_payload;
            string hashstring = GetHashString(request_payload + secret_key);
            string base64string = Convert.ToBase64String(StringToByteArray(hashstring));
            return base64string;
        }

        public static byte[] GetSHA256Hash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetSHA256Hash(inputString))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        private static void LogPayment(string refNo, string msg)
        {
            string filePath = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/paymentlogs_enets/{0}.log", refNo));
            Directory.CreateDirectory(new FileInfo(filePath).Directory.FullName);
            File.AppendAllText(filePath, msg);
        }

        private void SendAcknowledgement(string pageUrl, HttpContext context, string mid, string refNo, decimal amt)
        {
            string urlAcknowledgement = string.Format(pageUrl, mid, refNo, amt.ToString("F2"));
            context.Response.Redirect(urlAcknowledgement);
        }
    }


    public class eNetsPaymentMessage
    {
        public string netsMid { get; set; }
        public string merchantTxnRef { get; set; }
        public string merchantTxnDtm { get; set; }
        public string paymentType { get; set; }
        public string currencyCode { get; set; }
        public string paymentMode { get; set; }
        public string merchantTimeZone { get; set; }
        public string netsTxnRef { get; set; }
        public string netsTxnStatus { get; set; }
        public string netsTxnMsg { get; set; }
        public string maskPan { get; set; }
        public string stageRespCode { get; set; }
        public string txnRand { get; set; }
        public string actionCode { get; set; }
        public string netsMidIndicator { get; set; }
    }
}