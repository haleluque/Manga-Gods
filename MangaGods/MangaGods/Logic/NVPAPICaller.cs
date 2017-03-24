using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace MangaGods.Logic
{
    public class NVPAPICaller
    {
        //Flag that determines the PayPal environment (live or sandbox)
        private const bool BSandbox = true;
        private const string Cvv2 = "CVV2";

        // Live strings.
        private string _pEndPointUrl = "https://api-3t.paypal.com/nvp";
        private string _host = "www.paypal.com";

        // Sandbox strings.
        private string pEndPointURL_SB = "https://api-3t.sandbox.paypal.com/nvp";
        private string host_SB = "www.sandbox.paypal.com";

        private const string Signature = "SIGNATURE";
        private const string Pwd = "PWD";
        private const string Acct = "ACCT";

        //Replace <Your API Username> with your API Username
        //Replace <Your API Password> with your API Password
        //Replace <Your Signature> with your Signature
        public string ApiUsername = "henry-luque-facilitator_api1.hotmail.com";
        private string _apiPassword = "SGGB2A82Z5KRSKHQ";
        private string _apiSignature = "AFcWxV21C7fd0v3bYYYRCpSSRl31ACoUVPCRtf5jbwTN3r6trW8qtjxt";
        private string Subject = "";
        private string BNCode = "PP-ECWizard";


        //HttpWebRequest Timeout specified in milliseconds 
        private const int Timeout = 15000;
        /*
                private static readonly string[] SecuredNvps = new string[] { Acct, Cvv2, Signature, Pwd };
        */

        public void SetCredentials(string userid, string pwd, string signature)
        {
            ApiUsername = userid;
            _apiPassword = pwd;
            _apiSignature = signature;
        }

        public bool ShortcutExpressCheckout(string amt, ref string token, ref string retMsg)
        {
            if (BSandbox)
            {
                _pEndPointUrl = pEndPointURL_SB;
                _host = host_SB;
            }

            string returnURL = "https://localhost:44367/Checkout/CheckoutReview.aspx";
            string cancelURL = "https://localhost:44367/Checkout/CheckoutCancel.aspx";

            NvpCodec encoder = new NvpCodec();
            encoder["METHOD"] = "SetExpressCheckout";
            encoder["RETURNURL"] = returnURL;
            encoder["CANCELURL"] = cancelURL;
            encoder["BRANDNAME"] = "Wingtip Toys Sample Application";
            encoder["PAYMENTREQUEST_0_AMT"] = amt;
            encoder["PAYMENTREQUEST_0_ITEMAMT"] = amt;
            encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
            encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";

            // Obtiene los productos el carrito de compra
            using (var core = new CoreCarrito())
            {
                var carro = core.ConsultarCarros();

                for (var i = 0; i < carro.Count; i++)
                {
                    encoder["L_PAYMENTREQUEST_0_NAME" + i] = carro[i].Manga.Nombre;
                    var price = carro[i].Manga.Precio.ToString(CultureInfo.InvariantCulture);
                    encoder["L_PAYMENTREQUEST_0_AMT" + i] = price.Replace(",", ".");
                    //encoder["L_PAYMENTREQUEST_0_AMT" + i] = myOrderList[i].Product.UnitPrice.ToString();
                    encoder["L_PAYMENTREQUEST_0_QTY" + i] = carro[i].Cantidad.ToString();
                }
            }

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            NvpCodec decoder = new NvpCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck == "success" || strAck == "successwithwarning")
            {
                token = decoder["TOKEN"];
                string ecurl = "https://" + _host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;
                retMsg = ecurl;
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];
                return false;
            }
        }

        public bool GetCheckoutDetails(string token, ref string payerId, ref NvpCodec decoder, ref string retMsg)
        {
            if (BSandbox)
            {
                _pEndPointUrl = pEndPointURL_SB;
            }

            NvpCodec encoder = new NvpCodec();
            encoder["METHOD"] = "GetExpressCheckoutDetails";
            encoder["TOKEN"] = token;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NvpCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck == "success" || strAck == "successwithwarning")
            {
                payerId = decoder["PAYERID"];
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        public bool DoCheckoutPayment(string finalPaymentAmount, string token, string payerId, ref NvpCodec decoder, ref string retMsg)
        {
            if (BSandbox)
            {
                _pEndPointUrl = pEndPointURL_SB;
            }

            NvpCodec encoder = new NvpCodec();
            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = token;
            encoder["PAYERID"] = payerId;
            encoder["PAYMENTREQUEST_0_AMT"] = finalPaymentAmount;
            encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";
            encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NvpCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck == "success" || strAck == "successwithwarning")
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        public string HttpCall(string nvpRequest)
        {
            #region Prototipo anterior
            ////Estas dos líneas evitan que se genere el error de generación de certificado SSL
            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //string url = _pEndPointUrl;

            //string strPost = nvpRequest + "&" + BuildCredentialsNvpString();
            //strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);

            //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            //objRequest.Timeout = Timeout;
            //objRequest.Method = "POST";
            //objRequest.ContentLength = strPost.Length;

            //try
            //{
            //    using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
            //    {
            //        myWriter.Write(strPost);
            //    }
            //}
            //catch (Exception)
            //{
            //    // Log the exception.
            //    //WingtipToys.Logic.ExceptionUtility.LogException(e, "HttpCall in PayPalFunction.cs");
            //}

            ////Retrieve the Response returned from the NVP API call to PayPal.
            //HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            //string result;
            //using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            //{
            //    result = sr.ReadToEnd();
            //}

            //return result;
            #endregion

            string url = _pEndPointUrl;
            string strPost = nvpRequest + "&" + BuildCredentialsNvpString();
            strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Timeout = Timeout;
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception e)
            {
                // Registra el error enviado el objeto de tipo Object y un mensaje para identificar la fuente
                ExceptionUtility.LogException(e, "HttpCall in PayPalFunction.cs");
            }
            //Retrieve the Response returned from the NVP API call to PayPal.
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }

        private string BuildCredentialsNvpString()
        {
            NvpCodec codec = new NvpCodec();

            if (!IsEmpty(ApiUsername))
                codec["USER"] = ApiUsername;

            if (!IsEmpty(_apiPassword))
                codec[Pwd] = _apiPassword;

            if (!IsEmpty(_apiSignature))
                codec[Signature] = _apiSignature;

            if (!IsEmpty(Subject))
                codec["SUBJECT"] = Subject;

            codec["VERSION"] = "88.0";

            return codec.Encode();
        }

        public static bool IsEmpty(string s)
        {
            return s == null || s.Trim() == string.Empty;
        }
    }


    public sealed class NvpCodec : NameValueCollection
    {
        private const string Ampersand = "&";
        private const string EQUALS = "=";
        private static readonly char[] AmpersandCharArray = Ampersand.ToCharArray();
        private static readonly char[] EqualsCharArray = EQUALS.ToCharArray();

        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            bool firstPair = true;
            foreach (string kv in AllKeys)
            {
                string name = HttpUtility.UrlEncode(kv);
                string value = HttpUtility.UrlEncode(this[kv]);
                if (!firstPair)
                {
                    sb.Append(Ampersand);
                }
                sb.Append(name).Append(EQUALS).Append(value);
                firstPair = false;
            }
            return sb.ToString();
        }

        public void Decode(string nvpstring)
        {
            Clear();
            foreach (string nvp in nvpstring.Split(AmpersandCharArray))
            {
                string[] tokens = nvp.Split(EqualsCharArray);
                if (tokens.Length >= 2)
                {
                    string name = HttpUtility.UrlDecode(tokens[0]);
                    string value = HttpUtility.UrlDecode(tokens[1]);
                    Add(name, value);
                }
            }
        }

        public void Add(string name, string value, int index)
        {
            Add(GetArrayName(index, name), value);
        }

        public void Remove(string arrayName, int index)
        {
            Remove(GetArrayName(index, arrayName));
        }

        public string this[string name, int index]
        {
            get
            {
                return this[GetArrayName(index, name)];
            }
            set
            {
                this[GetArrayName(index, name)] = value;
            }
        }

        private static string GetArrayName(int index, string name)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), @"index cannot be negative : " + index);
            }
            return name + index;
        }
    }
}
