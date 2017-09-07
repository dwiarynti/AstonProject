using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequestDetailViewModel : MovementRequestDetail
    {
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
    }
}
