using Aston.Business.Data;
using Aston.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aston.Business
{
    public class PrefComponent
    {
        PrefExtensions _pref = new PrefExtensions();

        public List<Pref> GetCategory()
        {
            return _pref.GetCategory();
        }
        public List<Pref> GetLocationType()
        {
            return _pref.GetLocationType();
        }
        public List<Pref> GetStatus()
        {
            return _pref.GetStatus();
        }
    }
}
