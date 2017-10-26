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
using Aston.Web.Models.AccountViewModels;
using Aston.Web.Process;

namespace Aston.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserProcess _userProcess;

        public UserController(
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager,
              UserProcess userProcess)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userProcess = userProcess;
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


        [HttpPost]
        [Route("GetUserPagination")]
        public HttpResponseMessage GetUserPagination(HttpRequestMessage request, [FromBody] int Skip)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _userProcess.GetUserPagination(Skip);
            return response;
        }

        [HttpPost]
        [Route("UserRegister")]
        public HttpResponseMessage UserRegister(HttpRequestMessage request, [FromBody] RegisterViewModel obj)
        {
            var user = new ApplicationUser { UserName = obj.Username, Email = obj.Email ,IsActive = true ,DepartmentID = obj.DepartmentID};
            var result = _userManager.CreateAsync(user, obj.Password);

            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.Result.Succeeded, obj = user });
            return response;
        }
    }
}