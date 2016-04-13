using MvcLoginEncryption.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLoginEncryption.Models;
using MvcLoginEncryption.Common;

namespace MvcLoginEncryption.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Session["UserName"].ToString()))
            {
                ViewBag.UserName = Session["UserName"].ToString();
            }
           
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            try
            {
                using (var db = new LoginDbContext())
                {
                    User user = db.Users.Where(s => s.UserName == username || s.EmailId == username).FirstOrDefault();
                    if (user != null)
                    {
                        //获取登陆用户存在数据库中的盐
                        var hashCode = user.VCode;
                        //获取登陆用户加密的密码  
                        var encodingPasswordString = EncryptionHelper.EncodePassword(password, hashCode);
                        //Check Login Detail User Name Or Password    

                        var query = (from s in db.Users where (s.UserName == username || s.EmailId == username) && s.Password.Equals(encodingPasswordString) select s).FirstOrDefault();
                        if (query != null)
                        {
                            //RedirectToAction("Details/" + id.ToString(), "FullTimeEmployees");    
                            //return View("../Admin/Registration"); url not change in browser    
                            Session["UserName"] = query.UserName.ToString();
                        
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Invallid User Name or Password";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invallid User Name or Password";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = " Error!!! contact cms@info.in";
                return View();  
               
            }
        }
    }
}