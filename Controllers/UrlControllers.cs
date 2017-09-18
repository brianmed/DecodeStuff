using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using Base36Library;

namespace DecodeStuff.Controllers
{
    public class UrlController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Url";

            return View();
        }

        [HttpGet]
        [Route("/Url/Encode")]
        [Route("/Url/Decode")]
        [Route("/Url/Base36UrlEncode")]
        [Route("/Url/Base36UrlDecode")]
        [Route("/Url/Base64UrlEncode")]
        [Route("/Url/Base64UrlDecode")]        
        public IActionResult Redirects()
        {
            return Redirect("/Url");
        }

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Base36UrlEncode()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var encoded = Base36.Encode((int)o["data"]);
                
                return JsonConvert.SerializeObject(new { data = encoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }        

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Base36UrlDecode()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var unencoded = Base36.Decode((string)o["data"]).ToString();
                
                return JsonConvert.SerializeObject(new { data = unencoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }
        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Base64UrlEncode()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var bytes = System.Text.Encoding.UTF8.GetBytes((string)o["data"]);
                var encoded = WebEncoders.Base64UrlEncode(bytes);
                
                return JsonConvert.SerializeObject(new { data = encoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }        

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Base64UrlDecode()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var unencoded = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode((string)o["data"]));
                
                return JsonConvert.SerializeObject(new { data = unencoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }        

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Encode()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var encoded = System.Net.WebUtility.UrlEncode((string)o["data"]);

                return JsonConvert.SerializeObject(new { data = encoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }        

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Decode()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);
            
                var unencoded = System.Net.WebUtility.UrlDecode((string)o["data"]);

                return JsonConvert.SerializeObject(new { data = unencoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }                
    }
}
