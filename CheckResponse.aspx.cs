using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

using Newtonsoft.Json;

using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

namespace OTAformApp
{
    public partial class CheckResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CUser user = new CUser();
                
            if (Request.Form["Response"] != null)
            {
                string Ds_Date = "";
                string Ds_Hour = "";
                string Ds_Amount = "0";
                string Ds_Currency = "null";
                string Ds_Order = "";
                string Ds_MerchantCode = "";
                string Ds_Terminal = "";
                string Ds_Signature = "";
                string Ds_Response = "";
                string Ds_MerchantData = "";
                string Ds_SecurePayment = "null";
                string Ds_TransactionType = "";
                string Ds_Card_Country = "null";
                string Ds_Authorisation_Code = "";
                string Ds_ConsumerLanguage = "null";
                string Ds_Card_Type = "";
                string strTokenUserId = "";
                string strTokenId = "";

                if (Request.Form["BankDateTime"] != null) Ds_Date = Request.Form["BankDateTime"].ToString();
                if (Request.Form["BankDateTime"] != null) Ds_Hour = Request.Form["BankDateTime"].ToString();
                if (Request.Form["Amount"] != null) Ds_Amount = Request.Form["Amount"].ToString();
                if (Request.Form["Currency"] != null) Ds_Currency = Request.Form["Currency"].ToString();
                if (Request.Form["Order"] != null) Ds_Order = Request.Form["Order"].ToString();
                if (Request.Form["AccountCode"] != null) Ds_MerchantCode = Request.Form["AccountCode"].ToString();
                if (Request.Form["TpvID"] != null) Ds_Terminal = Request.Form["TpvID"].ToString();
                if (Request.Form["Signature"] != null) Ds_Signature = Request.Form["Signature"].ToString();
                if (Request.Form["Response"] != null) Ds_Response = Request.Form["Response"].ToString();
                if (Request.Form["Concept"] != null) Ds_MerchantData = Request.Form["Concept"].ToString();
                if (Request.Form["SecurePayment"] != null) Ds_SecurePayment = Request.Form["SecurePayment"].ToString();
                if (Request.Form["TransactionType"] != null) Ds_TransactionType = Request.Form["TransactionType"].ToString();
                if (Request.Form["CardCountry"] != null) Ds_Card_Country = Request.Form["CardCountry"].ToString();
                if (Request.Form["AuthCode"] != null) Ds_Authorisation_Code = Request.Form["AuthCode"].ToString();
                if (Request.Form["Language"] != null) Ds_ConsumerLanguage = Request.Form["Language"].ToString();
                if (Request.Form["IdUser"] != null) strTokenUserId = Request.Form["IdUser"].ToString();
                if (Request.Form["TokenUser"] != null) strTokenId = Request.Form["TokenUser"].ToString();

                user.UpdateOrderStatus(Ds_Order, Ds_Date, Ds_Hour, Ds_Amount, Ds_Currency,
                                      Ds_MerchantCode, Ds_Terminal, Ds_Signature, Ds_Response,
                                      Ds_MerchantData, Ds_SecurePayment, Ds_TransactionType, Ds_Card_Country, Ds_Authorisation_Code,
                                      Ds_ConsumerLanguage, Ds_Card_Type, strTokenUserId, strTokenId);
                Log.Info("CheckResponse Page_Load INFO: UpdateOrderStatus");
            }
        }





    //////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////
        ////////////        FUNCIONES PARA LA GENERACIÓN DEL FORMULARIO DE PAGO:          ////////////
        //////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////

        public static string EncodeTo64(string data)
        {
            byte[] toEncodeAsBytes = Encoding.GetEncoding(1252).GetBytes(data);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string DecodeFrom64(string data)
        {
            byte[] binary = Convert.FromBase64String(data);
            return Encoding.GetEncoding(1252).GetString(binary);
        }


        public static byte[] EncriptarSHA256(string texto, byte[] privateKey)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = privateKey;
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(texto));
            myhmacsha256.Clear();
            return hashValue;
        }


        public static string Encrypt(string textKey, string content)
        {
            byte[] key = Encoding.GetEncoding(1252).GetBytes(textKey);
            byte[] iv = new byte[8];
            byte[] data = Encoding.GetEncoding(1252).GetBytes(content);
            byte[] enc = new byte[0];
            System.Security.Cryptography.TripleDES tdes = System.Security.Cryptography.TripleDES.Create();
            tdes.IV = iv;
            tdes.Key = key;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.Zeros;
            ICryptoTransform ict = tdes.CreateEncryptor();
            enc = ict.TransformFinalBlock(data, 0, data.Length);
            return Encoding.GetEncoding(1252).GetString(enc);
        }


        public static string HashHMAC(string data, string key)
        {
            key = key ?? "";
            var encoding = Encoding.GetEncoding(1252);
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(data);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public static byte[] TripleDESEncrypt(string texto, byte[] key)
        {
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                byte[] iv_0 = { 0, 0, 0, 0, 0, 0, 0, 0 };

                byte[] toEncryptArray = Encoding.ASCII.GetBytes(texto);

                tdes.IV = iv_0;

                //assign the secret key
                tdes.Key = key;

                tdes.Mode = CipherMode.CBC;

                tdes.Padding = PaddingMode.Zeros;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                //transform the specified region of bytes array to resultArray
                byte[] resultArray =
                  cTransform.TransformFinalBlock(toEncryptArray, 0,
                  toEncryptArray.Length);

                //Clear to Best Practices
                tdes.Clear();

                return resultArray;
            }
        }


        
    }
}
