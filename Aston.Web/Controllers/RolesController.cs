using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Aston.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Aston.Web.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
   
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
         
        }
        public IActionResult Index()
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            var roles = _roleManager.Roles.ToList();
           
          
            foreach(var item in roles.ToList())
            {
                RoleViewModel obj = new RoleViewModel();
                obj.Id = item.Id;
                obj.Name = item.Name;
                result.Add(obj);
            }   
            return View(result);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            var IdentityRole = new IdentityRole();
            IdentityRole.Name = model.Name;
            var result = await _roleManager.CreateAsync(IdentityRole);
            if(result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.Index), "Roles");
            }
            return View(model.Name);
        }

        public async Task<IActionResult> Delete(string Id)
        {

            RoleViewModel obj = new RoleViewModel();
            var roles =  await _roleManager.FindByIdAsync(Id);
            obj.Id = roles.Id;
            obj.Name = roles.Name;
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
           
            var role = await _roleManager.FindByIdAsync(model.Id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.Index), "Roles");
            }
            return View(model);
        }





    }
}