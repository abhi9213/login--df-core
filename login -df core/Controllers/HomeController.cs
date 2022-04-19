using login__df_core.datafoldr;
using login__df_core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace login__df_core.Controllers
{
    public class HomeController : Controller
    {
        mvcdatabaseContext dbobj = new mvcdatabaseContext();
        loginClass mobj = new loginClass();
        Logtable tobj = new Logtable();

        empClass emobj = new empClass();

        Emptable etobj = new Emptable();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(loginClass mobj)
        {


            var res = dbobj.Logtables.Where(m => m.Email == mobj.Email).FirstOrDefault();


            if (res == null)
            {

                TempData["Invalid"] = "Email is not found";
            }

            else
            {
                if (res.Email == mobj.Email && res.Pass == mobj.Pass)
                {

                    var claims = new[] { new Claim(ClaimTypes.Name, res.Name),
                                        new Claim(ClaimTypes.Email, res.Email) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    authProperties);


                   // HttpContext.Session.SetString("email", res.Email);                 

                   

                    return RedirectToAction("Index");

                }

                else
                {

                    ViewBag.Inv = "Wrong Email Id or password";

                    return View("login");
                }


            }



            return View();
        }
        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View("login");
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult show()
        {
            var res = dbobj.Emptables.ToList();
            List<empClass> lobj = new List<empClass>();
            foreach(var i in res)
            {
                lobj.Add(new empClass { 
                Id=i.Id,
                    Name = i.Name,
                    Email = i.Email,
                    Mobile = i.Mobile,
                    Dept = i.Dept,
                    Sal = i.Sal,
                    Zip = i.Zip                 

                });
            }

            return View(lobj);
        }
        [Authorize]
        [HttpGet]
        public IActionResult addata()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult addata(empClass emobj)
        {
            etobj.Id = emobj.Id;
            etobj.Name = emobj.Name;
            etobj.Email = emobj.Email;
            etobj.Mobile = emobj.Mobile;
            etobj.Dept = emobj.Dept;
            etobj.Sal = emobj.Sal;
            etobj.Zip = emobj.Zip;
            if(emobj.Id==0)
            {
                dbobj.Emptables.Add(etobj);
                dbobj.SaveChanges();


            }
            else
            {
                dbobj.Entry(etobj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbobj.SaveChanges();
            }


            return RedirectToAction("show");
        }
        [Authorize]
        public IActionResult delete(int Id)
        {
            var del = dbobj.Emptables.Where(m => m.Id == Id).First();
            dbobj.Emptables.Remove(del);
            dbobj.SaveChanges();

            return RedirectToAction("show");
        }
        [Authorize]
        public IActionResult edit(int Id)
        {
            var edit = dbobj.Emptables.Where(m => m.Id == Id).First();
            emobj.Id = edit.Id;
            emobj.Name = edit.Name;
            emobj.Mobile = edit.Mobile;
            emobj.Email = edit.Email;
            emobj.Dept = edit.Dept;
            emobj.Sal = edit.Sal;
            emobj.Zip = edit.Zip;


            return View("addata",emobj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
