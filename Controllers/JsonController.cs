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
    public class JsonController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Json";

            return View();
        }

        [HttpGet]
        [Route("/Json/PrettyPrint")]
        public IActionResult Redirects()
        {
            return Redirect("/Json");
        }

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string PrettyPrint()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var json = JObject.Parse((string)o["data"]);

                return JsonConvert.SerializeObject(new { data = JsonConvert.SerializeObject(json, Formatting.Indented) });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string OneLine()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var json = JObject.Parse((string)o["data"]);

                return JsonConvert.SerializeObject(new { data = JsonConvert.SerializeObject(json, Formatting.None) });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }        
    }
}
