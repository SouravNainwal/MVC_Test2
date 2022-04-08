using Mvc_emp.Data_Emp;
using Mvc_emp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc_emp.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("Test/gettable")]
        public List<EMP_Entry> getemp()
        {
            Emp_DetailsEntities obj = new Emp_DetailsEntities();
            List<EmpEntry> mobj = new List<EmpEntry>();
            var rep = obj.EMP_Entry.ToList();
            return rep;
        }
    }
}
