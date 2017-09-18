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
using Microsoft.AspNetCore.WebUtilities;

namespace DecodeStuff.Controllers
{
    public class HtmlController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Html";

            return View();
        }

        [HttpGet]
        [Route("/Html/Encode")]
        [Route("/Html/Decode")]
        public IActionResult Redirects()
        {
            return Redirect("/Html");
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

                var encoded = System.Net.WebUtility.HtmlEncode((string)o["data"]);

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
            
                var unencoded = System.Net.WebUtility.HtmlDecode((string)o["data"]);

                return JsonConvert.SerializeObject(new { data = unencoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }                
    }
}
