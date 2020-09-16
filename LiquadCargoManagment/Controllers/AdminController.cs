using LiquadCargoManagment.DataTransferObject;
using LiquadCargoManagment.Helpers;
using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        LCMEntities context;
        SecurityTokenIdentifier _security;
        ModelDML dml;
        public AdminController()
        {
            context = new LCMEntities();
            _security = new SecurityTokenIdentifier();
            dml = new ModelDML();
            var sc = context.SoftwareSetups.FirstOrDefault();
            SoftwareFormatting.DateFormat = sc.DateType.Format;
            SoftwareFormatting.ContactNoFormat = sc.ContactNoType.Format;
            SoftwareFormatting.NumberFormat = sc.NoType.Format;
            SoftwareFormatting.Logo = sc.Logo;
        }

        #region RevenueType
        [HttpGet]
        public ActionResult RevenueType(string auth)
        {
            string _params = _security.IdentifyToken(auth,"revenuetype");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                return View(context.RevenueTypes.OrderByDescending(x => x.RevenueTypeID).ToList());
            }
            else
            {
                return ExpireToken();
            }
          
        }
        [HttpPost]
        public ActionResult addRevenueType(RevenueType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    var Duplicated = context.RevenueTypes.Where(x => x.Code == model.Code && x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.RevenueTypeID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedByID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.isActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.CreatedById = ApplicationHelper.UserID;
                            model.isActive = true;
                            model.DateCreated = DateTime.Now;
                            context.RevenueTypes.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                   
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_RevenueType.cshtml", context.RevenueTypes.OrderByDescending(x => x.RevenueTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteRevenue(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.RevenueTypes.Where(model => model.RevenueTypeID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_RevenueType.cshtml", context.RevenueTypes.OrderByDescending(x => x.RevenueTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ExpenseType
        [HttpGet]
        public ActionResult ExpenseType(string auth)
        {
            string _params = _security.IdentifyToken(auth,"expensetype");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;

                return View(context.ExpensesTypes.OrderByDescending(x => x.ExpensesTypeID).ToList());
            }
            else
            {
                return ExpireToken();
            }

        }
        [HttpPost]
        public ActionResult addExpense(ExpensesType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.ExpensesTypeCode != null || model.ExpensesTypeName != null)
                {
                    var Duplicated = context.ExpensesTypes.Where(x => x.ExpensesTypeCode == model.ExpensesTypeCode && x.ExpensesTypeName == model.ExpensesTypeName).ToList();
                    model.ExpensesTypeCode = model.ExpensesTypeCode.ToUpper();
                    if (model.ExpensesTypeID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedByUserID = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedByUserID = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.DateCreated = DateTime.Now;
                            context.ExpensesTypes.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                     
                    }
               
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_ExpenseType.cshtml", context.ExpensesTypes.OrderByDescending(x => x.ExpensesTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteExpenseType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.ExpensesTypes.Where(model => model.ExpensesTypeID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ExpenseType.cshtml", context.ExpensesTypes.OrderByDescending(x => x.ExpensesTypeID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region VendorType
        [HttpGet]
        public ActionResult VendorType(string auth)
        {
            string _params = _security.IdentifyToken(auth,"vendortype");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                return View(context.VendorTypes.OrderByDescending(x => x.ID).ToList());
            }
            else
            {
                return ExpireToken();
            }
        }
        [HttpPost]
        public ActionResult addVendorType(VendorType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    var Duplicated = context.RevenueTypes.Where(x => x.Code == model.Code && x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.VendorTypes.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                      
                    }
                   
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VendorType.cshtml", context.VendorTypes.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVendorType(int id)
        {
            string Message = "";
            string Status = "OK";
            try{
                if (id > 0)
                {
                    var inRow = context.VendorTypes.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VendorType.cshtml", context.VendorTypes.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vendor
        [HttpGet]
        public ActionResult Vendor(string auth)
        {
         

            string _params = _security.IdentifyToken(auth,"vendor");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                ViewBag.VendorTypes = new SelectList(context.VendorTypes.OrderByDescending(x => x.ID).ToList(), "ID", "Name");
                return View(context.Vendors.OrderByDescending(x => x.ID).ToList());
            }
            else
            {
                return ExpireToken();
            }

        }
        [HttpPost]
        public ActionResult addVendor(Vendor model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.VendorTypeID > 0)
                {
                    var Duplicated = context.Vendors.Where(x => x.Code == model.Code).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.Vendors.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                  
                }
                else
                {
                    Message = "Code and Vendor Type is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<Vendor> data = context.Vendors.ToList();
            string View = RenderPartialToString("~/Views/Shared/_Vendor.cshtml",data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVendor(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Vendors.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Vendor.cshtml", context.Vendors.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        
        #region ProductType
        [HttpGet]
        public ActionResult ProductType(string auth)
        {
            string _params = _security.IdentifyToken(auth,"producttype");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                return View(context.Categories.OrderByDescending(x => x.ID).ToList());
            }
            else
            {
                return ExpireToken();
            }
          
        }
        [HttpPost]
        public ActionResult addProductType(Category model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    var Duplicated = context.Categories.Where(x => x.Code == model.Code && x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.Status = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                      
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.Status = true;
                            model.CreatedDate = DateTime.Now;
                            context.Categories.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                      
                    }
                  
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_ProductType.cshtml", context.Categories.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProductType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Categories.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_ProductType.cshtml", context.Categories.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Product
        public ActionResult Product(string auth)
        {
            string _params = _security.IdentifyToken(auth,"product");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                ViewBag.ProductTypes = new SelectList(context.Categories.ToList(), "ID", "Name");
                return View(context.Products.OrderByDescending(x => x.ID).ToList());
            }
            else
            {
                return ExpireToken();
            }

        

        }

       
        [HttpPost]
        public ActionResult addProduct(Product model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.Name != null && model.Category > 0)
                {
                    var Duplicated = context.Products.Where(x => x.Code == model.Code || x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Products.Add(model);

                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                     
                    }
                 
                }
                else
                {
                    Message = "Code Name and Product type is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Product.cshtml", context.Products.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProduct(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Products.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Product.cshtml", context.Products.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region OwnGroup
        [HttpGet]
        public ActionResult OwnGroup(string auth)
        {
            string _params =_security.IdentifyToken(auth,"owngroup");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                return View(context.OwnGroups.OrderByDescending(x => x.GroupID).ToList());

            }
            else
            {
                return ExpireToken();
            }
        }
        [HttpPost]
        public ActionResult AddOwnGroup(OwnGroup model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null)
                {
                    var Duplicated = context.OwnGroups.Where(x => x.Code == model.Code || x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.GroupID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                      
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.OwnGroups.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<OwnGroup> data = context.OwnGroups.ToList();
            string View = RenderPartialToString("~/Views/Shared/_OwnGroup.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteOwnGroup(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.OwnGroups.Where(model => model.GroupID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_OwnGroup.cshtml", context.OwnGroups.OrderByDescending(x => x.GroupID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region OwnCompany
        [HttpGet]
        public ActionResult OwnCompany(string auth)
        {
            string _params = _security.IdentifyToken(auth,"owncompany");
            if (_params != string.Empty)
            {
                Session["Allow"] = _params;
                ViewBag.OwnGroups = new SelectList(context.OwnGroups.ToList(), "GroupID", "Name");
                ViewBag.Roles = new SelectList(context.Roles.ToList(), "RoleID", "RoleName");
                ViewBag.Subcriptions = new SelectList(context.Subcriptions.ToList(), "ID", "Name");
                return View(context.OwnCompanies.Where(x=>x.SubcriptionID == ApplicationHelper.SubcriptionID).OrderByDescending(x => x.ID).ToList());
            }
            else
            {
                return ExpireToken();
            }
          
        }
        [HttpPost]
        public ActionResult AddOwnCompany(OwnCompany model,UserAccount UserAcc)
        {
            string Message = "";
            string Status = "OK";
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (model.Code != null && model.Name != null && model.GroupID > 0)
                    {
                        
                        var Duplicated = context.OwnCompanies.Where(x => x.Code == model.Code || x.Name == model.Name).ToList();
                        var AllCompanies = new List<OwnCompany>();
                        if (model.SubcriptionID > 0)
                        {
                             AllCompanies = context.OwnCompanies.Where(x => x.SubcriptionID == model.SubcriptionID).ToList();
                        }
                        else
                        {
                             AllCompanies = context.OwnCompanies.Where(x => x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();

                        }
                        model.Code = model.Code.ToUpper();
                        if (model.ID > 0)
                        {
                            if (Duplicated.Count > 1)
                            {
                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                if (model.SubcriptionID <= 0)
                                {
                                    model.SubcriptionID = ApplicationHelper.SubcriptionID;
                                }
                                model.ModifiedBy = ApplicationHelper.UserID;
                                model.ModifiedDate = DateTime.Now;
                                model.IsActive = true;
                                context.Entry(model).State = EntityState.Modified;
                                if (AllCompanies.Count <= 0)
                                {
                                    if (UserAcc.UserName != null && UserAcc.UserPassword != null && UserAcc.RoleID != null)
                                    {

                                        var account = context.UserAccounts.Where(x => x.OwnCompanyId == model.ID).FirstOrDefault();
                                        account.UserName = UserAcc.UserName;
                                        account.UserPassword = UserAcc.UserPassword;
                                        account.ModifiedDate = DateTime.Now;
                                        account.ModifiedBy = ApplicationHelper.UserID;
                                        account.Active = true;
                                        context.Entry(account).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        throw new Exception("if you create first own company so Username,Password and Role is required");
                                    }
                                }
                            }
                           

                        }
                        else
                        {
                            if (Duplicated.Count > 0)
                            {
                                Message = "The record with this code and name is already exist";
                                Status = "Duplicate";
                            }
                            else
                            {
                                if (model.SubcriptionID == null)
                                {
                                    model.SubcriptionID = ApplicationHelper.SubcriptionID;
                                }
                                model.CreatedBy = ApplicationHelper.UserID;
                                model.IsActive = true;
                                model.CreatedDate = DateTime.Now;
                                context.OwnCompanies.Add(model);
                                context.SaveChanges();
                                if (AllCompanies.Count <= 0)
                                {
                                    if (UserAcc.UserName != null && UserAcc.UserPassword != null && UserAcc.RoleID != null)
                                    {
                                        UserAcc.Active = true;
                                        UserAcc.OwnCompanyId = model.ID;
                                        UserAcc.CreatedBy = ApplicationHelper.UserID;
                                        UserAcc.CreatedDate = DateTime.Now;
                                        context.UserAccounts.Add(UserAcc);
                                    }
                                    else
                                    {
                                        throw new Exception("if you create first own company so Username,Password and Role is required");
                                    }

                                }

                            }
                            context.SaveChanges();
                            transaction.Commit();
                            AccountController ctrl = new AccountController();
                            ctrl.GetOwnCompanies();
                        }
                          
                      
                    }
                    else
                    {
                        Message = "Code Name and Group is Required";
                        Status = "Required";
                    }
                }
                catch (Exception ex)
                {

                    Message = $"An error occured due to: {ex.Message}";
                    Status = "Exception";
                    transaction.Rollback();
                }
            }
            List<OwnCompany> data = context.OwnCompanies.Where(x=>x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteOwnCompany(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.OwnCompanies.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {
                        context.OwnCompanies.Remove(inRow);
                        DbModelBuilder modelBuilder = new DbModelBuilder();
                        modelBuilder.Entity<OwnCompany>()
                .HasMany<UserAccount>(c => c.UserAccounts)
                .WithOptional(x => x.OwnCompany)
                .WillCascadeOnDelete(true);
                        context.SaveChanges();
                        Message = "Record Deleted Successfully";

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
                Status = ex.GetType().Name;
            }

            string View = RenderPartialToString("~/Views/Shared/_OwnCompany.cshtml", context.OwnCompanies
                .Where(x=>x.SubcriptionID == ApplicationHelper.SubcriptionID).OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region VehicleType
        [HttpGet]
        public ActionResult VehicleType(string auth)
        {
            string _params = _security.IdentifyToken(auth,"vehicletype");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                return View(context.VehicleTypes.OrderByDescending(x => x.ID).ToList());

            }
            else
            {
                return ExpireToken();
            }
        }
        [HttpPost]
        public ActionResult addVehicleType(VehicleType model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    var Duplicated = context.VehicleTypes.Where(x => x.Code == model.Code && x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                    }
                    else
                    {

                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.VehicleTypes.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                    }
                    
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_VehicleType.cshtml", context.VehicleTypes.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVehicleType(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.VehicleTypes.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_VehicleType.cshtml", context.VehicleTypes.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vehicle
        [HttpGet]
        public ActionResult Vehicle(string auth)
        {
            string _params = _security.IdentifyToken(auth,"vehicle");
            if (_params != string.Empty)
            {
                 Session["Allow"]  = _params;
                ViewBag.VehicleTypes = new SelectList(context.VehicleTypes.ToList(), "ID", "Name");
                ViewBag.Drivers = new SelectList(context.Drivers.ToList(), "ID", "Name");
                ViewBag.PrincipleCompany = new SelectList(dml.getPrincipleCompany(), "ID", "Name");
                return View(context.Vehicles.OrderByDescending(x => x.VehicleID).ToList());

            }
            else
            {
                return ExpireToken();
            }
        
        }
        [HttpPost]
        public ActionResult addVehicle(Vehicle model,HttpPostedFileBase file)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.RegNo != null && model.VehicleTypeID > 0)
                {
                    var Duplicated = context.Vehicles.Where(x => x.Code == model.Code || x.RegNo == model.RegNo).ToList();
                    model.Code = model.Code.ToUpper();
                    if (file != null)
                    {
                        model.Documents = ApplicationHelper.UploadFile(file,ApplicationHelper.VehicleDocuments, Server);
                    }
                    if (model.VehicleID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Vehicles.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                     

                    }
                   
                }
                else
                {
                    Message = "Code , Name and Vehicle Type is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }
            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Vehicle.cshtml", context.Vehicles.OrderByDescending(x => x.VehicleID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteVehicle(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Vehicles.Where(model => model.VehicleID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Vehicle.cshtml", context.Vehicles.OrderByDescending(x => x.VehicleID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverDetails(long Id)
        {
            var data = context.Drivers.Where(x => x.ID == Id).FirstOrDefault();
            return Json(new { Driver = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion
        public ActionResult Ledgers()
        {
            return View(context.VendorTypes.ToList());
        }

        #region City
        [HttpGet]
        public ActionResult City(string auth)
        {
            string _params = _security.IdentifyToken(auth,"city");
            if (_params != string.Empty)
            {

                 Session["Allow"]  = _params;
                ViewBag.Province = new SelectList(context.Provinces.ToList(), "ProvinceID", "ProvinceName");
                return View(context.Cities.Where(x=>x.OwnCompanyID == ApplicationHelper.OwnCompanyID)
                    .OrderByDescending(x => x.CityID).ToList());

            }
            else
            {
                return ExpireToken();
            }
        
        }
        [HttpPost]
        public ActionResult addCity(City model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.CityCode != null || model.CityName!= null)
                {
                    var Duplicated = context.Cities.Where(x => x.CityCode == model.CityCode && x.CityName== model.CityName).ToList();
                    model.CityCode = model.CityCode.ToUpper();
                    if (model.CityID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                        
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.OwnCompanyID = ApplicationHelper.OwnCompanyID;
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.Cities.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                  
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_City.cshtml", context.Cities.OrderByDescending(x => x.CityID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCity(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Cities.Where(model => model.CityID== id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_City.cshtml", context.Cities.OrderByDescending(x => x.CityID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Area
        [HttpGet]
        public ActionResult Area(string auth)
        {
            string _params = _security.IdentifyToken(auth,"area");
            if (_params != string.Empty)
            {

                 Session["Allow"]  = _params;
                ViewBag.Cities = new SelectList(context.Cities.ToList(), "CityID", "CityName");
                return View(dml.getArea());

            }
            else
            {
                return ExpireToken();
            }

        }
        [HttpPost]
        public ActionResult addArea(Area model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null || model.Name != null)
                {
                    var Duplicated = dml.getArea(model.Code,model.Name);
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.DateModified = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                    
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.DateCreated = DateTime.Now;
                            context.Areas.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                    
                    }
                   
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Area.cshtml", dml.getArea());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteArea(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = dml.getArea(id);
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Area.cshtml", dml.getArea());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomMethod
        public string RenderPartialToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        public RedirectToRouteResult ExpireToken()
        {
            AccountController _account = new AccountController();
            _account.LogoutWithoutSessionExpire();
            TempData["unauth"] = "you are trying to change authentication token please login again for security purpose";
            return RedirectToAction("Login", "Account");
        }
        #endregion
        #region CustomerGroup
        [HttpGet]
        public ActionResult CustomerGroup(string auth)
        {
            string _params = _security.IdentifyToken(auth,"customergroup");
            if (_params != string.Empty)
            {

                 Session["Allow"]  = _params;
                return View(context.CustomerGroups.OrderByDescending(x => x.GroupID).ToList());


            }
            else
            {
                return ExpireToken();
            }
        }
        [HttpPost]
        public ActionResult AddCustomerGroup(CustomerGroup model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null)
                {
                    var Duplicated = context.CustomerGroups.Where(x => x.Code == model.Code || x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();

                    if (model.GroupID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                      
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.CustomerGroups.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                  
                    }
                   
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<CustomerGroup> data = context.CustomerGroups.ToList();
            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerGroup(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerGroups.Where(model => model.GroupID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerGroup.cshtml", context.CustomerGroups.OrderByDescending(x => x.GroupID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomerCompany
        [HttpGet]
        public ActionResult CustomerCompany(string auth)
        {
            string _params = _security.IdentifyToken(auth,"customercompany");
            if (_params != string.Empty)
            {

                 Session["Allow"]  = _params;
                ViewBag.CustomerGroup = new SelectList(context.CustomerGroups.ToList(), "GroupID", "Name");
                return View(context.CustomerCompanies.OrderByDescending(x => x.ID).ToList());

            }
            else
            {
                return ExpireToken();
            }
           
        }
        [HttpPost]
        public ActionResult AddCustomerCompany(CustomerCompany model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Code != null && model.Name != null && model.GroupID > 0)
                {
                    var Duplicated = context.CustomerCompanies.Where(x => x.Code == model.Code || x.Name == model.Name).ToList();
                    model.Code = model.Code.ToUpper();
                    if (model.ID > 0)
                    {
                        if (Duplicated.Count <= 1)
                        {
                            model.ModifiedBy = ApplicationHelper.UserID;
                            model.ModifiedDate = DateTime.Now;
                            model.IsActive = true;
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                       
                    }
                    else
                    {
                        if (Duplicated.Count <= 0)
                        {
                            model.CreatedBy = ApplicationHelper.UserID;
                            model.IsActive = true;
                            model.CreatedDate = DateTime.Now;
                            context.CustomerCompanies.Add(model);
                        }
                        else
                        {
                            Message = "The record with this code and name is already exist";
                            Status = "Duplicate";
                        }
                        
                    }
                  
                }
                else
                {
                    Message = "Code Name and Group is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            List<CustomerCompany> data = context.CustomerCompanies.ToList();
            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", data);
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteCustomerCompany(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.CustomerCompanies.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_CustomerCompany.cshtml", context.CustomerCompanies.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Province
        [HttpGet]
        public ActionResult Province()
        {
            return View(context.Provinces.OrderByDescending(x => x.ProvinceID).ToList());
        }
        [HttpPost]
        public ActionResult addProvince(Province model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.ProvinceName != null)
                {
                    var Duplicated = context.Provinces.Where(x => x.ProvinceName == model.ProvinceName).FirstOrDefault();
                    if (Duplicated == null)
                    {
                        if (model.ProvinceID > 0)
                        {
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            context.Provinces.Add(model);
                        }
                    }
                    else
                    {
                        Message = "The record with this code and name is already exist";
                        Status = "Duplicate";
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            string View = RenderPartialToString("~/Views/Shared/_Province.cshtml", context.Provinces.OrderByDescending(x => x.ProvinceID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteProvince(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Provinces.Where(model => model.ProvinceID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_Province.cshtml", context.Provinces.OrderByDescending(x => x.ProvinceID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region WorkOrderGrid
        public ActionResult WorkOrderGrid()
        {
            return View(context.WorkOrders.OrderByDescending(x => x.WorkorderId).ToList());
        }
        [HttpPost]
        public JsonResult DeleteWorkOrderGrid(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.WorkOrders.Where(model => model.WorkorderId == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_WorkOrderGrid.cshtml", context.WorkOrders.OrderByDescending(x => x.WorkorderId).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #region Search
        public ActionResult Search(DateTime? DateFrom, DateTime? DateTo)
        {
            List<WorkOrder> lstOrders = new List<WorkOrder>();
            if (DateFrom != null && DateTo == null)
            {
                lstOrders = context.WorkOrders.Where(x => x.OrderDate >= DateFrom).ToList();
            }
            else if(DateFrom != null && DateTo != null)
            {
                lstOrders = context.WorkOrders.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo).ToList();

            }
            else if (DateFrom == null && DateTo != null)
            {
                lstOrders = context.WorkOrders.Where(x => x.OrderDate <= DateTo).ToList();

            }
            string view = RenderPartialToString("_WorkOrderGrid", lstOrders);
            return Json(new {html = view },JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region WorkOrder
        public ActionResult WorkOrder()
        {

            List<OwnCompany> Billto = context.OwnCompanies.ToList();
            ViewBag.bill = new SelectList(Billto, "ID", "Name");

            List<OwnCompany> Sender = context.OwnCompanies.ToList();
            ViewBag.Sender = new SelectList(Sender, "ID", "Name");

            List<OwnCompany> Oc = context.OwnCompanies.ToList();
            ViewBag.li = new SelectList(Oc, "ID", "Name");

            List<ProductBroker> li = context.ProductBrokers.ToList();
            ViewBag.ProductBroker = new SelectList(li, "Id", "Name");

            List<PackageType> Pt = context.PackageTypes.ToList();
            ViewBag.pt = new SelectList(Pt, "PackageTypeID", "PackageTypeName");

            List<Product> P = context.Products.ToList();
            ViewBag.p = new SelectList(P, "ID", "Name");
          

            return View();
        }
        [HttpPost]
        public ActionResult SaveProductBroker(ProductBroker model)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (model.Name != null)
                {
                    var Duplicated = context.ProductBrokers.Where(x => x.Name == model.Name).FirstOrDefault();
                    if (Duplicated == null)
                    {
                        if (model.Id > 0)
                        {
                            context.Entry(model).State = EntityState.Modified;
                        }
                        else
                        {
                            context.ProductBrokers.Add(model);
                        }
                    }
                    else
                    {
                        Message = "The record with this code and name is already exist";
                        Status = "Duplicate";
                    }
                }
                else
                {
                    Message = "Code and Name is Required";
                    Status = "Required";
                }
            }
            catch (Exception ex)
            {

                Message = $"An error occured due to: {ex.Message}";
                Status = "Exception";
            }

            context.SaveChanges();
            Message = "New Broker Added";
            return Json(new { Status = Status, Message = Message, html = context.ProductBrokers.
                OrderByDescending(x=>x.Id).ToList() }, JsonRequestBehavior.AllowGet);


        }

        //public ActionResult WorkOrder(ProductBroker pro)
        //{
        //    string message = "";
        //    string status = "OK";
        //    try
        //    {
        //        using (LCMEntities lcm = new LCMEntities())
        //        {

        //            if (pro.Id == 0)
        //            {
        //                context.ProductBrokers.Add(pro);
        //                context.SaveChanges();
        //                message = "New broker added";

        //            }
        //            else
        //            {

        //                context.Entry(pro).State = EntityState.Modified;
        //                context.SaveChanges();
        //                message = "New broker added";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        status = ex.GetType().Name;
        //        message = ex.Message;
        //    }
        //    List<ProductBroker> li = context.ProductBrokers.ToList();

        //    return Json(new { list = li, message = message, status = status }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult InsertCustomers(List<OrderDetail> Details, WorkOrder Order, ProductBroker pro)
        {
            string message = "";
            string status = "OK";
            using (LCMEntities lcm = new LCMEntities())
            {
                using (var transaction = lcm.Database.BeginTransaction())
                {
                    try
                    {

                        long OrderID = 0;

                        if (Order != null)
                        {
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            lcm.WorkOrders.Add(Order);
                            lcm.SaveChanges();
                            OrderID = Order.WorkorderId;
                        }
                        if (Details == null)
                        {
                            Details = new List<OrderDetail>();
                        }


                        foreach (OrderDetail customers in Details)
                        {
                            customers.WorkOrderID = OrderID;
                            lcm.OrderDetails.Add(customers);
                        }
                        lcm.SaveChanges();
                        transaction.Commit();
                        message = "Work order successfully created";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        status = ex.GetType().Name;
                        message = ex.Message;
                        if (ex.InnerException != null)
                        {
                            message += " Inner Exception: " + ex.InnerException.Message;
                        }
                    }
                }

            }

            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Bilty
        public ActionResult Bilty(long? Id)
        {
            LCMEntities lcm = new LCMEntities();
            List<OwnCompany> Billto = lcm.OwnCompanies.ToList();
            ViewBag.bill = new SelectList(Billto, "ID", "Name");

            List<OwnCompany> Sender = lcm.OwnCompanies.ToList();
            ViewBag.Sender = new SelectList(Sender, "ID", "Name");

            List<CustomerCompany> Receiver = lcm.CustomerCompanies.ToList();
            ViewBag.li = new SelectList(Receiver, "ID", "Name");

            List<ExpensesType> expense = lcm.ExpensesTypes.ToList();
            ViewBag.Expense = new SelectList(expense, "ExpensesTypeID", "ExpensesTypeName");

            List<ProductBroker> li = lcm.ProductBrokers.ToList();
            ViewBag.ProductBroker = new SelectList(li, "Id", "Name");

            List<Category> ProductType = lcm.Categories.ToList();
            ViewBag.pt = new SelectList(ProductType, "ID", "Name");

            List<Product> P = lcm.Products.ToList();
            ViewBag.p = new SelectList(P, "ID", "Name");

            ViewBag.Reg = new SelectList(context.Vehicles.ToList(), "VehicleID", "RegNo");

            ViewBag.VehicleTypes = new SelectList(context.VehicleTypes.ToList(), "ID", "Name");

            List<Bank> Bank = lcm.Banks.ToList();
            ViewBag.Bank = new SelectList(Bank, "BankID", "Name");

            BiltyViewModel vm = new BiltyViewModel();
            if (Id > 0)
            {
                vm.lstBiltyDetail = context.BiltyDetails.Where(x => x.BiltyID == Id).OrderByDescending(x => x.ID).ToList();
                vm.Bilty = context.Bilties.Where(x => x.ID == Id).FirstOrDefault();
            }
            else
            {
                vm.lstBiltyDetail = new List<BiltyDetail>();
                vm.Bilty = new Bilty();
            }
            return View(vm);
        }

        [HttpPost]

        public JsonResult InsertBilty(List<BiltyDetail> Details, List<VehicleExpens> Expenses, List<DieselExpense> Diesal, Bilty Order)
        {
            string message = "";
            string status = "OK";
            using (LCMEntities lcm = new LCMEntities())
            {
                using (var transaction = lcm.Database.BeginTransaction())
                {
                    try
                    {

                        long OrderID = 0;

                        if (Order != null)
                        {
                            Order.CreatedBy = ApplicationHelper.UserID;
                            Order.CreatedDate = DateTime.Now;
                            lcm.Bilties.Add(Order);
                            lcm.SaveChanges();
                            OrderID = Order.ID;
                        }
                        if (Details == null)
                        {
                            Details = new List<BiltyDetail>();
                        }


                        foreach (BiltyDetail customers in Details)
                        {
                            customers.ID = OrderID;
                            lcm.BiltyDetails.Add(customers);
                        }
                        lcm.SaveChanges();
                        transaction.Commit();
                        message = "Bilty Created Successfully ";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        status = ex.GetType().Name;
                        message = ex.Message;
                        if (ex.InnerException != null)
                        {
                            message += " Inner Exception: " + ex.InnerException.Message;
                        }
                    }
                }

            }

            return Json(new { message = message, status = status }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region BiltyGrid
        public ActionResult BiltyGrid()
        {
            ViewBag.Sender = new SelectList(context.CustomerCompanies.ToList(), "ID", "Name");
            ViewBag.BillTo = new SelectList(context.CustomerCompanies.ToList(), "ID", "Name");
            return View(context.Bilties.OrderByDescending(x => x.ID).ToList());
        }
        [HttpPost]
        public JsonResult DeleteBiltyGrid(int id)
        {
            string Message = "";
            string Status = "OK";
            try
            {
                if (id > 0)
                {
                    var inRow = context.Bilties.Where(model => model.ID == id).FirstOrDefault();
                    if (inRow != null)
                    {

                        context.Entry(inRow).State = EntityState.Deleted;
                        int a = context.SaveChanges();
                        Message = "Record Deleted Successfully";

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

            string View = RenderPartialToString("~/Views/Shared/_BiltyGrid.cshtml", context.Bilties.OrderByDescending(x => x.ID).ToList());
            return Json(new { Status = Status, Message = Message, html = View }, JsonRequestBehavior.AllowGet);
        }
        #region Search
        public ActionResult SearchBilty(DateTime? DateFrom, DateTime? DateTo, DateTime? DeliDate, int SearchSender, int SearchBillTo)
        {
            List<Bilty> lstBilty = new List<Bilty>();
            if (DeliDate != null)
            {
                lstBilty = context.Bilties.Where(x => x.DeliveryDate >= DeliDate).ToList();
            }
            else if (DateFrom != null && DateTo == null)
            {
                lstBilty = context.Bilties.Where(x => x.OrderDate >= DateFrom).ToList();
            }
            else if (DateFrom != null && DateTo != null)
            {
                lstBilty = context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo).ToList();

            }
            else if (DateFrom == null && DateTo != null)
            {
                lstBilty = context.Bilties.Where(x => x.OrderDate <= DateTo).ToList();
            }
            else if (SearchSender.ToString() != null)
            {
                lstBilty = context.Bilties.Where(x => x.Sender <= SearchSender).ToList();
            }
            else if (SearchBillTo.ToString() != null)
            {
                lstBilty = context.Bilties.Where(x => x.BillTo <= SearchBillTo).ToList();
            }
            string view = RenderPartialToString("_BiltyGrid", lstBilty);
            return Json(new { html = view }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

    }
}