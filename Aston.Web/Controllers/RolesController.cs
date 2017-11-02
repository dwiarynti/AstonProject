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
using Microsoft.AspNetCore.Authorization;

namespace Aston.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Role")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly RoleProcess _roleProcess;

        public RolesController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            RoleProcess roleProcess)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleProcess = roleProcess;
        }

        [HttpGet]
        [Route("GetRoles")]
        public HttpResponseMessage GetRoles(HttpRequestMessage request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            List<RoleViewModel> result = new List<RoleViewModel>();
            var roles = _roleManager.Roles.ToList();

            foreach (var item in roles.ToList())
            {
                RoleViewModel obj = new RoleViewModel();
                obj.Id = item.Id;
                obj.Name = item.Name;
                result.Add(obj);
            }

            response = request.CreateResponse(HttpStatusCode.OK, new { success = true, obj = result });
            return response;
        }

        [HttpPost]
        [Route("GetRolePagination")]
        public HttpResponseMessage GetRolePagination(HttpRequestMessage request, [FromBody] int Skip)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = _roleProcess.GetRolePagination(Skip);
            return response;
        }

        [HttpPost]
        [Route("RoleRegister")]
        public HttpResponseMessage RoleRegister(HttpRequestMessage request, [FromBody] RoleViewModel obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var IdentityRole = new IdentityRole();
            IdentityRole.Name = obj.Name;
            var result = _roleManager.CreateAsync(IdentityRole);
          
            response = request.CreateResponse(HttpStatusCode.OK, new { success = result.Result.Succeeded ? true : false, obj = obj });
            return response;
        }

        [HttpPost]
        [Route("RoleEdit")]
        public HttpResponseMessage RoleEdit(HttpRequestMessage request, [FromBody] RoleViewModel obj)
        {
            var role = _roleManager.FindByIdAsync(obj.Id).Result;

            role.Name = obj.Name;
            var update = _roleManager.UpdateAsync(role);

            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = update.Result.Succeeded ? true : false });
            return response;
        }

        [HttpPost]
        [Route("DeleteRole")]
        public HttpResponseMessage DeleteRole(HttpRequestMessage request, [FromBody] RoleViewModel obj)
        {
            var role = _roleManager.FindByIdAsync(obj.Id).Result;
            var deleterole = _roleManager.DeleteAsync(role).Result;

            HttpResponseMessage response = new HttpResponseMessage();
            response = request.CreateResponse(HttpStatusCode.OK, new { success = deleterole.Succeeded });
            return response;
        }

        #region MVC Action

        //public IActionResult Index()
        //{
        //    List<RoleViewModel> result = new List<RoleViewModel>();
        //    var roles = _roleManager.Roles.ToList();

        //    foreach (var item in roles.ToList())
        //    {
        //        RoleViewModel obj = new RoleViewModel();
        //        obj.Id = item.Id;
        //        obj.Name = item.Name;
        //        result.Add(obj);
        //    }
        //    return View(result);
        //}

        //public async Task<IActionResult> Create()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(RoleViewModel model)
        //{
        //    var IdentityRole = new IdentityRole();
        //    IdentityRole.Name = model.Name;
        //    var result = await _roleManager.CreateAsync(IdentityRole);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(AccountController.Index), "Roles");
        //    }
        //    return View(model.Name);
        //}

        //public async Task<IActionResult> Delete(string Id)
        //{

        //    RoleViewModel obj = new RoleViewModel();
        //    var roles = await _roleManager.FindByIdAsync(Id);
        //    obj.Id = roles.Id;
        //    obj.Name = roles.Name;
        //    return View(obj);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(IdentityRole model)
        //{

        //    var role = await _roleManager.FindByIdAsync(model.Id);
        //    var result = await _roleManager.DeleteAsync(role);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(AccountController.Index), "Roles");
        //    }
        //    return View(model);
        //} 
        #endregion

    }
}