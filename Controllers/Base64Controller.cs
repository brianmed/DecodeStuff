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

namespace DecodeStuff.Controllers
{
    public class Base64Controller : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Base64";

            return View();
        }

        [HttpGet]
        [Route("/Base64/Encode")]
        [Route("/Base64/Decode")]
        public IActionResult Redirects()
        {
            return Redirect("/Base64");
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

                var unencoded = System.Text.Encoding.UTF8.GetBytes((string)o["data"]);
                var encoded = System.Convert.ToBase64String(unencoded);

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

                var unencoded = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String((string)o["data"]));

                return JsonConvert.SerializeObject(new { data = unencoded });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }                
    }
}
