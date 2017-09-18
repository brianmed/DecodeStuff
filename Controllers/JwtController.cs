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
// using System.IdentityModel.Tokens.SecurityTokenHandler;
using System.IdentityModel.Tokens.Jwt;

namespace DecodeStuff.Controllers
{
    public class JwtController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Jwt";

            return View();
        }

        [HttpGet]
        [Route("/Jwt/Validate")]
        public IActionResult Redirects()
        {
            return Redirect("/Jwt");
        }

        [HttpPost]
        [RequestSizeLimit(16384)]
        public string Validate()
        {
            try {
                string j;

                using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    j = reader.ReadToEnd();
                }
                var o = JObject.Parse(j);

                var aa = new JwtSecurityTokenHandler();

                var jwt = aa.ReadJwtToken((string)o["data"]);        

                return JsonConvert.SerializeObject(new { data = $"Valid From: {jwt.ValidFrom} -> Valid To: {jwt.ValidTo}\n\n\n{jwt}" });
            } catch (Exception e) {
                return JsonConvert.SerializeObject(new { data = (string)null, error = e.Message });
            }
        }        
    }
}
