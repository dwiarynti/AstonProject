using Aston.Business.Data;
using Aston.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business
{
    public class LookupListComponent
    {
        LookupListExtensions _pref = new LookupListExtensions();
        DepartmentExtensions _department = new DepartmentExtensions();

        public List<LookupList> GetCategory()
        {
            return _pref.GetCategory();
        }
        public List<LookupList> GetLocationType()
        {
            return _pref.GetLocationType();
        }
        public List<LookupList> GetStatus()
        {
            return _pref.GetStatus();
        }
        public List<LookupList> GetApprovalStatus()
        {
            return _pref.GetApprovalStatus();
        }
        public LookupList GetLookupByCategoryCode(int code)
        {
            return _pref.GetLookupByCategoryCode(code);
        }
        public LookupList GetLookupByStatusCode(int code)
        {
            return _pref.GetLookupByStatusCode(code);
        }
        public LookupList GetLookupByLocationTypeCode(int code)
        {
           return _pref.GetLookupByLocationTypeCode(code);
        }
        public LookupList GetLookupByApprovalStatusCode(int code)
        {
            return _pref.GetLookupByApprovalStatusCode(code);
        }
     
        public List<Department> GetDepartment()
        {
            return _department.GetDepartment();
        }

        public Department GetDepartmentByID(int id)
        {
            return _department.GetDepartmentByID(id);
        }
    }
}
