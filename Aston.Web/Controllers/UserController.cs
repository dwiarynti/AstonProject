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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aston.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserProcess _userProcess;

        public UserController(
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager,
              RoleManager<IdentityRole> roleManager,
              UserProcess userProcess)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

        [HttpGet]
        [Route("GetRoles")]
        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var roles = _roleManager.Roles.ToList();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = roles });
            return response;
        }

        [HttpPost]
        [Route("UserRegister")]
        public HttpResponseMessage UserRegister(HttpRequestMessage request, [FromBody] RegisterViewModel obj)
        {
            var user = new ApplicationUser { UserName = obj.Username, Email = obj.Email ,IsActive = true ,DepartmentID = obj.DepartmentID};
            var result = _userManager.CreateAsync(user, obj.Password);
            var addUserRole = _userManager.AddToRoleAsync(user, obj.Role);

            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.Result.Succeeded && addUserRole.Result.Succeeded ? true:false, obj = user });
            return response;
        }

        [HttpPost]
        [Route("GenerateUserCode")]
        public HttpResponseMessage GenerateUserCode(HttpRequestMessage request, [FromBody] ResetPasswordViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var user = _userManager.FindByIdAsync(obj.Id);
            var code = _userManager.GeneratePasswordResetTokenAsync(user.Result).Result;
            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = code });
            return response;
        }

        [HttpPost]
        [Route("UserEdit")]
        public HttpResponseMessage UserEdit(HttpRequestMessage request, [FromBody] ResetPasswordViewModel obj)
        {
            var user = _userManager.FindByIdAsync(obj.Id).Result;
            user.Email = obj.Email;
            user.UserName = obj.Username;
            user.DepartmentID = obj.DepartmentID;
            var update = _userManager.UpdateAsync(user);
            var addUserRole = _userManager.AddToRoleAsync(user, obj.Role);


            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = update.Result.Succeeded && addUserRole.Result.Succeeded ? true:false });
            return response;
        }

        [HttpPost]
        [Route("ResetUserPassword")]
        public HttpResponseMessage ResetUserPassword(HttpRequestMessage request, [FromBody] ResetPasswordViewModel obj)
        {
            var user = _userManager.FindByIdAsync(obj.Id).Result;
            user.Email = obj.Email;
            user.UserName = obj.Username;
            user.DepartmentID = obj.DepartmentID;
            var resetpassword = _userManager.ResetPasswordAsync(user, obj.Code, obj.Password).Result;

            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = resetpassword.Succeeded });
            return response;
        }
    }
}