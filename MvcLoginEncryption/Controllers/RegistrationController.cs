using MvcLoginEncryption.Common;
using MvcLoginEncryption.DAL;
using MvcLoginEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLoginEncryption.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User model)
        {

            try
            {
                using (var db = new LoginDbContext())
                {
                    User user = db.Users.Where(s => s.UserName == model.UserName || s.EmailId == model.EmailId).FirstOrDefault();
                    if (user == null)
                    {
                        //产生加密的盐
                        var keyNew = EncryptionHelper.GeneratePassword(10);
                        //加密密码
                        var password = EncryptionHelper.EncodePassword(model.Password, keyNew);

                        //将加密之后的密码保存到数据库
                        model.Password = password;
                        model.CreateDate = DateTime.Now;
                        model.ModifyDate = DateTime.Now;
                        //盐
                        model.VCode = keyNew;
                        db.Users.Add(model);
                        db.SaveChanges();
                        //返回到登陆页面
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User Allredy Exixts!!!!!!!!!!";
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Some exception occured" + ex;
                return View();  
            }
            
        }
    }
}