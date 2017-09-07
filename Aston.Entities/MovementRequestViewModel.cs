using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Entities
{
    public class MovementRequestViewModel : MovementRequest
    {
    
        public List<MovementRequestDetailViewModel> MovementRequestDetail { get; set; }
    }
}
