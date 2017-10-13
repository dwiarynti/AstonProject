using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aston.Business;
using System.Net.Http;
using Aston.Entities;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Aston.Web.Models;

namespace Aston.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody] UserLoginViewModel obj)
        {
            UserLoginViewModel result = new UserLoginViewModel();
            var checkuser = _userManager.Users.Where(p => p.UserName == obj.Username).FirstOrDefault();
            if(checkuser != null)
            {
               if(checkuser.IsActive == true)
                {
                    var login = _signInManager.PasswordSignInAsync(obj.Username, obj.Password, false, lockoutOnFailure: false);
                    if(login.Result.Succeeded)
                    {
                        result.result = true;
                        result.DepartmentID = checkuser.DepartmentID;
                        result.Username = checkuser.UserName;
                    }
                }
               else
                {
                    result.result = false;
                }
            }
            else
            {
                result.result = false;
            } 

           


            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.result, obj = result });
            return response;
        }
       
    }
}