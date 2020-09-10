using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using Newtonsoft.Json;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class SetupController : Controller
    {
        private readonly LCMEntities context;
        SecurityTokenIdentifier _security;
        public SetupController()
        {
            context = new LCMEntities();
            _security = new SecurityTokenIdentifier();

        }
        public ActionResult Setting(string auth)
        {
            string _params = _security.IdentifyToken(auth,"setting");
            if (_params != string.Empty)
            {

                 Session["Allow"]  = _params;
                ViewBag.Currency = new SelectList(context.CurrencyTypes.ToList(), "ID", "CurrencyType1");
                ViewBag.DateType = new SelectList(context.DateTypes.ToList(), "ID", "DateType1");
                ViewBag.ContactType = new SelectList(context.ContactNoTypes.ToList(), "ID", "ContactNoType1");
                ViewBag.NumType = new SelectList(context.NoTypes.ToList(), "ID", "NoType1");
                return View();

            }
            else
            {
                return ExpireToken();
            }
      
        }
        [HttpPost]
        public ActionResult Setting(SoftwareSetup model, HttpPostedFileBase file)
        {
            string Message = "";
            string Status = "OK";
            if (model.ID > 0)
            {
                try
                {
                    if (file != null)
                    {
                        string ext = Path.GetExtension(file.FileName);
                        if (ext.ToLower() == ".jpg" || ext.ToLower() == ".png" || ext.ToLower() == ".jpeg")
                        {
                            ApplicationHelper.UploadFile(file, ApplicationHelper.ApplicationLogo, Server);
                            model.Logo = file.FileName;
                        }
                        else
                        {
                            Status = "ExtensionNotMatched";
                        }
                    }
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    if (Status == "ExtensionNotMatched")
                        Message = "Settings Updated but Logo does'nt uploaded because just 'jpg' or 'png' format is allowed ";
                    else
                        Message = "Settings Updated";
                }
                catch (Exception ex)
                {

                    Message = "can't save your new changes due to: " + ex.Message;
                    Status = "Exception";
                }
              
            }
            else
            {
                Message = "System can't find the latest changing due to some issue";
                Status = "IdNotFound";
            }
            return Json(new { Message = Message, status = Status },JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrentSetting()
        {
            var x = (from sc in context.SoftwareSetups select new { sc.CurrencyTypeID, sc.DateTypeID,sc.ID,sc.ContactNoTypeID,sc.NoTypeID }).FirstOrDefault();
            return Json(x,JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveLogo()
        {
            string Message = "";
            string status = "OK";
            if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Logo/"),SoftwareFormatting.Logo)))
            {    
               System.IO.File.Delete(Path.Combine(Server.MapPath("~/Logo/"), SoftwareFormatting.Logo));
                Message = "Logo Removed";
            }
            else
            {
                Message = "Logo not exist";
                status = "NotFound";
            }
            return Json(new { Message = Message, status = status }, JsonRequestBehavior.AllowGet);
        }


        public RedirectToRouteResult ExpireToken()
        {
            AccountController _account = new AccountController();
            _account.LogoutWithoutSessionExpire();
            TempData["unauth"] = "you are trying to change authentication token please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }
    }
}