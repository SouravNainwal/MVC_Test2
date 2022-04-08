using MVC_Test2.Data_Mvc;
using MVC_Test2.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_Test2.Controllers
{
    public class HomeController : Controller
    {
        Uri baseaddress = new Uri("http://localhost:60126/");
        HttpClient client = new HttpClient();
        public HomeController()
        {
            client.BaseAddress = baseaddress;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Reg_Class obj)
        {
            Emp_DetailsEntities dobj = new Emp_DetailsEntities();
            var UserRes = dobj.login_table.Where(m => m.email == obj.email).FirstOrDefault();
            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found";
            }
            else
            {
                if (UserRes.email == obj.email && UserRes.password == obj.password)
                {
                    FormsAuthentication.SetAuthCookie(UserRes.email, false);
                    Session["UserName"] = UserRes.name;
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    TempData["Wrong"] = "Wrong Password";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Registration(Reg_Class Robj)
        {
            Emp_DetailsEntities dobj = new Emp_DetailsEntities();
            login_table tobj = new login_table();
            tobj.name = Robj.name;
            tobj.email = Robj.email;
            tobj.password = Robj.password;

            dobj.login_table.Add(tobj);
            dobj.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
        [Authorize]
        public ActionResult Tables()
        {
            //var client = new RestClient("http://localhost:52047/test/gettable");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = client.ExecuteGetAsync(request);
            //Console.WriteLine(response.Content);

            //List<EMP_Entry> tobj = new List<EMP_Entry>();
            //List<F_Class> cobj = new List<F_Class>();
            //HttpResponseMessage rmsg = client.GetAsync(client.BaseAddress + "test/gettable").Result;
            //if (rmsg.IsSuccessStatusCode)
            //{
            //    string data = rmsg.Content.ReadAsStringAsync().Result;
            //    tobj = JsonConvert.DeserializeObject<List<EMP_Entry>>(data);
            //}


            List<F_Class> tobj = new List<F_Class>();
            HttpResponseMessage rmsg = client.GetAsync(client.BaseAddress + "Test/gettable").Result;
            if (rmsg.IsSuccessStatusCode)
            {
                string data = rmsg.Content.ReadAsStringAsync().Result;
                tobj = JsonConvert.DeserializeObject<List<F_Class>>(data);
            }
            return View(tobj); 

             

            //foreach (var item in tobj)
            //{
            //    cobj.Add(new F_Class
            //    {
            //        Id = item.Id,
            //        Emp_Name = item.Emp_Name,
            //        Emp_Email = item.Emp_Email,
            //        Emp_Salary = item.Emp_Salary,
            //        Emp_Id = item.Emp_Id

            //    });

            //}
            //return View(cobj);
           
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Form()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Form(F_Class mobj)
        {
            Emp_DetailsEntities dobj = new Emp_DetailsEntities();
            EMP_Entry tobj = new EMP_Entry();
            tobj.Id = mobj.Id;
            tobj.Emp_Name = mobj.Emp_Name;
            tobj.Emp_Email = mobj.Emp_Email;
            tobj.Emp_Salary = (int)mobj.Emp_Salary;
            tobj.Emp_Id = mobj.Emp_Id;
            if (mobj.Id == 0)
            {
                dobj.EMP_Entry.Add(tobj);
                dobj.SaveChanges();
            }
            else
            {
                dobj.Entry(tobj).State = System.Data.Entity.EntityState.Modified;
                dobj.SaveChanges();
            }
            return RedirectToAction("Tables");
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult edit(int Id)
        {
            F_Class mobj = new F_Class();
            Emp_DetailsEntities dobj = new Emp_DetailsEntities();
            var eobj = dobj.EMP_Entry.Where(m => m.Id == Id).First();
            mobj.Id = eobj.Id;
            mobj.Emp_Name = eobj.Emp_Name;
            mobj.Emp_Email = eobj.Emp_Email;
            mobj.Emp_Salary = eobj.Emp_Salary;
            mobj.Emp_Id= eobj.Emp_Id;

            return View("Form", mobj);
        }
        [Authorize]
        public ActionResult delete(int Id)
        {
            Emp_DetailsEntities dobj = new Emp_DetailsEntities();
            var ditem = dobj.EMP_Entry.Where(m => m.Id == Id).First();
            dobj.EMP_Entry.Remove(ditem);
            dobj.SaveChanges();

            return RedirectToAction("Tables");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}