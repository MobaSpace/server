using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobaSpace.Core.Data.Models;

namespace MobaSpace.Web.UI.Controllers
{
    public class MailController : Controller
    {
        public async Task<IActionResult> ForgotPassword(
            [FromServices] UserManager<User> userManager, 
            string email,
            string callbackurl_enc,
            string mailto,
            string ret_type)
        {
            var user = await userManager.FindByEmailAsync(email);
            var callbackurl = System.Web.HttpUtility.UrlDecode(callbackurl_enc);

            switch (ret_type)
            {
                case "html":
                    ViewBag.callbackurl = callbackurl;
                    ViewBag.mailto = mailto;
                    return PartialView(user);

                case "txt":
                    var plainText = System.IO.File.ReadAllText("./Views/Mail/ForgotPassword.txt")
                                  .Replace("{user.UserName}", user.UserName)
                                  .Replace("{callbackUrl}", callbackurl)
                                  .Replace("{mailto}", mailto);
                    return Ok(plainText);

                default:
                    return NotFound("Type not found " + ret_type);
            }
        }

        public async Task<IActionResult> ConfirmEmail(
            [FromServices] UserManager<User> userManager,
            string email,
            string callbackurl_enc,
            string mailto,
            string ret_type)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return NotFound("user by email");
            }

            var callbackurl = System.Web.HttpUtility.UrlDecode(callbackurl_enc);

            switch (ret_type)
            {
                case "html":
                    ViewBag.callbackurl = callbackurl;
                    ViewBag.mailto = mailto;
                    return PartialView(user);

                case "txt":
                    var plainText = System.IO.File.ReadAllText("./Views/Mail/ConfirmEmail.txt")
                                  .Replace("{user.UserName}", user.UserName)
                                  .Replace("{callbackUrl}", callbackurl)
                                  .Replace("{mailto}", mailto);
                    return Ok(plainText);

                default:
                    return NotFound("Type not found " + ret_type);
            }
        }      
    }
}