using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aston.Entities
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Role { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentID { get; set; }
        public List<string> UserRole { get; set; }
        public List<IdentityRole> Roles { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class UserPaginationViewModel
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class RegisterUserViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        //[Display(Name = "Username")]
        public string Username { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public Nullable<int> DepartmentID { get; set; }
    }
}
