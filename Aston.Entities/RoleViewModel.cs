using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RolePaginationViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int TotalRow { get; set; }
    }
}
