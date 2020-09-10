using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly LCMEntities context;
        AdminController ctr;
        SecurityTokenIdentifier _security;
        public AccountController()
        {
            context = new LCMEntities();
            ctr = new AdminController();
            _security = new SecurityTokenIdentifier();

        }

        #region Login
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserAccount model)
        {
            string Validation = "";
            if (model.UserName == null)
                Validation = "Enter valid username";
            else if (model.UserPassword == null)
                Validation = "Enter Valid Password";
            else
            {
                using (var context = new LCMEntities())
                {
                    UserAccount isAuthenticate = await context.UserAccounts.Where(x => x.UserName.Equals(model.
                        UserName, StringComparison.Ordinal) && x.UserPassword.Equals(model.UserPassword) && x.Active == true).FirstOrDefaultAsync();
                    if (isAuthenticate != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        string path = ApplicationHelper.UserProfileImagePath + isAuthenticate.Image;
                        ApplicationHelper.ProfileImage = path.Replace("~", "");
                        ApplicationHelper.Username = isAuthenticate.FullName;
                        ApplicationHelper.UserID = isAuthenticate.UserID;
                        ApplicationHelper.OwnCompanyID = (long)isAuthenticate.OwnCompanyId;
                        ApplicationHelper.SubcriptionID = (long)context.OwnCompanies.Where(x => x.ID == isAuthenticate.OwnCompanyId)
                            .FirstOrDefault().SubcriptionID;
                         ApplicationHelper.lstRolePerm = context.RolePermissions
                            .Where(x => x.RoleID == isAuthenticate.RoleID && x.Parameter != "None").ToList();
                        GetOwnCompanies();
                        if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                        {
                            return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2], "Admin");
                            
                        }
                        else
                        {
                            Validation = "success";
                        }
                    }
                    else
                    {
                        Validation = "The providing credential is not valid Or account may be deactivated";
                    }
                }
            }
            return Json(new { error = Validation }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
          
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
         
            return RedirectToAction("Login");
        }

        public ActionResult LogoutWithoutSessionExpire()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region Registration
        public ActionResult Registration(string auth)
        {
            string _params = _security.IdentifyToken(auth,"registration");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;

                ViewBag.OwnCompanies = new SelectList(context.OwnCompanies.Where(x=>x.SubcriptionID==ApplicationHelper.SubcriptionID).ToList(), "RoleID", "RoleName");
                ViewBag.Roles = new SelectList(context.Roles.Where(x => x.isDeleted == false || x.isDeleted == null).ToList(), "RoleID", "RoleName");
                return View(new UserAccount());

            }
            else
            {
                return ExpireToken();
            }
            
        }
        public ActionResult addRegistration(UserAccount model,HttpPostedFileBase file)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.OwnCompanyId > 0)
                {
                    if (model.UserName != null && model.UserPassword != null)
                    {
                        var Duplicated = context.UserAccounts.Where(x => x.UserName == model.UserName).ToList();
                        if (model.UserID > 0)
                        {
                            if (!(Duplicated.Count > 1))
                            {
                                var local = context.Set<UserAccount>().Local.FirstOrDefault(x => x.UserID == model.UserID);
                                if (local != null)
                                {
                                    context.Entry(local).State = EntityState.Detached;
                                }
                                model.Image = file == null ? "" : file.FileName;
                                model.ModifiedDate = DateTime.Now;
                                model.ModifiedBy = ApplicationHelper.UserID;
                                model.Active = true;
                                context.Entry(model).State = EntityState.Modified;
                                Message = "User Profile Updated";
                            }
                            else
                            {
                                Message = "The username is already taken try onther one";
                                Status = "Duplicate";
                            }
                        }
                        else
                        {
                            if (!(Duplicated.Count > 0))
                            {
                                model.Image = file == null ? "" : file.FileName;
                                model.Active = true;
                                model.CreatedDate = DateTime.Now;
                                model.CreatedBy = ApplicationHelper.UserID;
                                context.UserAccounts.Add(model);
                                Message = "User Registerd successfully";
                            }
                            else
                            {
                                Message = "The username is already taken try onther one";
                                Status = "Duplicate";
                            }
                        }
                        if (file != null)
                        {
                            ApplicationHelper.UploadFile(file, ApplicationHelper.UserProfileImagePath, Server);
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        Message = "Username and Password is Required";
                        Status = "Required";
                    }
                }
                else
                {
                    Message = "In which company you want to register this user? please select a company";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            return Json(new { Status = Status, Message = Message }, JsonRequestBehavior.AllowGet);
            

        }

        public async Task<ActionResult> UsersList(string auth)
        {
            string _params = _security.IdentifyToken(auth,"userslist");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                return View(await context.UserAccounts.ToListAsync());
            }
            else
            {
                return ExpireToken();
            }
        }

        public async Task<ActionResult> EditUserList(long id)
        {
            return View("Registration",await context.UserAccounts.Where(x => x.UserID == id).FirstOrDefaultAsync());
        }
        public JsonResult DeleteRegistration(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.UserAccounts.Where(model => model.UserID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                         context.SaveChanges();
                        Message = "User Deleted Successfully";

                    }
                    else
                    {
                        Message = "Application cannot find this record in database please refresh the browser";
                        Status = "Exception";
                    }
                }
                else
                {
                    Message = "Something Went Wrong";
                    Status = "Exception";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            string View = ctr.RenderPartialToString("~/Views/Shared/_Registration.cshtml", context.UserAccounts.OrderByDescending(x => x.UserID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public RedirectToRouteResult ExpireToken()
        {
            LogoutWithoutSessionExpire();
            TempData["unauth"] = "you are trying to change authentication token please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }


        public void GetOwnCompanies()
        {
            Session.Add("OwnCompanies", context.OwnCompanies
                           .Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList());
        }
    }
}