using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class UserLoginViewModel
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public int? DepartmentID { get; set; }
        public bool result { get; set; }
    }
}
