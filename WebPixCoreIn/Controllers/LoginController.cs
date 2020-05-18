using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPixCoreIn.Models;

namespace WebPixCoreIn.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        // POST: Login/Create
        [HttpPost]
        public ActionResult login(LoginViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (collection.login == "webpix")
                    if (collection.senha == "lucas07")
                    {
                        Response.Cookies["UsuarioLogado"].Value = "logado";
                        Response.Cookies["UsuarioLogado"].Expires = DateTime.Now.AddMinutes(30);
                        return Redirect("~/cliente/Index");
                    }

                return RedirectToAction("login");
            }
            catch
            {
                return View();
            }
        }

    }
}
