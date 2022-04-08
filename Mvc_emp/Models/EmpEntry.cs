using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_emp.Models
{
    public class EmpEntry
    {
        public int Id { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Email { get; set; }
        public Nullable<int> Emp_Salary { get; set; }
        public string Emp_Id { get; set; }
    }
}