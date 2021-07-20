using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorseBetting.Common
{
    public class Utils
    {
        public Utils()
        {

        }
        public static int ParseToInt(string str)
        {
            int result = 0;
            int.TryParse(str, out result);
            return result;
        }

        // Token: 0x06000174 RID: 372 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
        public static double ParseToDouble(string str)
        {
            double result = 0.0;
            try
            {
                double.TryParse(str, out result);
            }
            catch (Exception)
            {
            }
            return result;
        }

        // Token: 0x06000175 RID: 373 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
        public static DateTime ParseToDateTime(string str, string format = "")
        {
            global::System.DateTime result = default(DateTime);
            try
            {
                if (!string.IsNullOrEmpty(format))
                {
                    DateTime.TryParseExact(str, format, null, DateTimeStyles.None, out result);
                }
                else
                {
                    DateTime.TryParse(str, out result);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }

        public static void getCountry(string strCountry, ref string country, ref string animal)
        {
            try
            {
                if (!string.IsNullOrEmpty(strCountry))
                {
                    strCountry = strCountry.Trim();
                    if (strCountry.Contains("("))
                    {
                        global::System.Text.RegularExpressions.Match match = global::System.Text.RegularExpressions.Regex.Match(strCountry, "(?<Country>[^\\ ]+)[ (]*(?<Type>[^)]+)");
                        country = match.Groups["Country"].Value;
                        animal = match.Groups["Type"].Value;
                    }
                    else
                    {
                        country = strCountry;
                    }
                }
            }
            catch (global::System.Exception ex)
            {
                string message = ex.Message;
            }
        }

        public static bool downloadFile(string uri, CookieContainer cookieContainer)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.Referer = string.Format("{0}/_index_ctb.jsp", Settings.GetSettings.url1);
            httpWebRequest.Accept = "image/webp,image/apng,image/*,*/*;q=0.8";
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.129 Safari/537.36";
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            bool flag = httpWebResponse.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
            bool result;
            if ((httpWebResponse.StatusCode == HttpStatusCode.OK || httpWebResponse.StatusCode == HttpStatusCode.MovedPermanently || httpWebResponse.StatusCode == HttpStatusCode.Found) && flag)
            {
                using (Stream responseStream = httpWebResponse.GetResponseStream())
                {
                    using (Stream stream = File.OpenWrite(Settings.GetSettings.captha_img))
                    {
                        byte[] array = new byte[4096];
                        int num;
                        do
                        {
                            num = responseStream.Read(array, 0, array.Length);
                            stream.Write(array, 0, num);
                        }
                        while (num != 0);
                    }
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static int trySolveCaptchaUpdate(string path, ref string message)
        {
            int result = 0;
            string text = string.Empty;
            int result3;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html, application/xhtml+xml, application/xml; q=0.9, image/webp, */*; q=0.8");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent("----WebKitFormBoundaryQRIA65tkuSBONMDE");
                multipartFormDataContent.Headers.Clear();
                multipartFormDataContent.Headers.Add("Content-Type", "multipart/form-data; boundary=----WebKitFormBoundaryQRIA65tkuSBONMDE");
                multipartFormDataContent.Add(new ByteArrayContent(Encoding.UTF8.GetBytes("post")), "method");
                multipartFormDataContent.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(Settings.GetSettings.token1)), "key");
                StreamContent streamContent = new StreamContent(File.OpenRead(path));
                streamContent.Headers.Clear();
                streamContent.Headers.Add("Content-Disposition", string.Format("form-data; name=\"file\"; filename=\"{0}\"", path));
                streamContent.Headers.Add("Content-Type", "image/png");
                multipartFormDataContent.Add(streamContent);
                HttpResponseMessage result2 = httpClient.PostAsync("http://2captcha.com/in.php", multipartFormDataContent).Result;
                result2.EnsureSuccessStatusCode();
                string text2 = result2.Content.ReadAsStringAsync().Result;
                if (!text2.Contains("OK|"))
                {
                    message = text2;
                    result3 = 1;
                }
                else
                {
                    text2 = text2.Replace("OK|", "");
                    if (!string.IsNullOrEmpty(text2))
                    {
                        int i = 0;
                        while (i < 20)
                        {
                            i++;
                            Thread.Sleep(3000);
                            result2 = httpClient.GetAsync(string.Format("http://2captcha.com/res.php?key={0}&action=get&id={1}", Settings.GetSettings.token1, text2)).Result;
                            result2.EnsureSuccessStatusCode();
                            text = result2.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(text) && text.Contains("OK|"))
                            {
                                text = text.Replace("OK|", "");
                            
                                message = text;
                                return result;
                            }
                        }
                        message = text;
                        return result;
                    }
                    result3 = 2;
                }
            }
            catch (Exception ex)
            {
                message = ex.StackTrace;
                result3 = -1;
            }
            return result3;
        }
    }

    
}
