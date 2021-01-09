using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PM_Assignment.Models;
using System.Web.Security;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PM_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private ProductManagementEntities dbobj;
        public HomeController()
        {
            dbobj = new ProductManagementEntities();
        }
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user_master model)
        {
            using (var context = new ProductManagementEntities())
            {
                bool isValid = context.user_master.Any(x => x.EmailID == model.EmailID && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.EmailID, false);
                    return RedirectToAction("Index", "DataConnect");
                }

                ModelState.AddModelError("", "Invalid emailid and password");
                return View();
            }
                
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(user_master model)
        {
            using (var context = new ProductManagementEntities())
            {
                context.user_master.Add(model);
                context.SaveChanges();  
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public ActionResult ProductList()
        {
            return View();
        }


        public ActionResult AddProduct()
        {
            AddProductModel objaddProductModel = new AddProductModel();
            objaddProductModel.CategoryList = (from objCat in dbobj.Categories
                                               select new SelectListItem()
                                               {
                                                   Text = objCat.CategoryName,
                                                   Value = objCat.CategoryId.ToString(),
                                                   Selected = true
                                               });
            ViewBag.CategoryList = objaddProductModel.CategoryList;
            return View(objaddProductModel);
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductModel objaddproduct)
        {
            string s_fileName = Path.GetFileNameWithoutExtension(objaddproduct.SmallImageFile.FileName);
            string s_fileExtension = Path.GetExtension(objaddproduct.SmallImageFile.FileName);
            s_fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + s_fileName.Trim() + s_fileExtension;
            string s_UploadPath = ConfigurationManager.AppSettings["S_UserImagePath"].ToString();
            objaddproduct.SmallImagePath = s_UploadPath + s_fileName;
            objaddproduct.SmallImageFile.SaveAs(objaddproduct.SmallImagePath);

            string l_fileName = Path.GetFileNameWithoutExtension(objaddproduct.LongImageFile.FileName);
            string l_fileExtension = Path.GetExtension(objaddproduct.LongImageFile.FileName);
            l_fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + l_fileName.Trim() + l_fileExtension;
            string l_UploadPath = ConfigurationManager.AppSettings["L_UserImagePath"].ToString();
            objaddproduct.LongImagePath = l_UploadPath + l_fileName;
            objaddproduct.LongImageFile.SaveAs(objaddproduct.LongImagePath);


            Item obItem = new Item();
            obItem.CategoryId = Int16.Parse(objaddproduct.Category_Id);
            //obItem.ItemCode = objaddproduct.ItemCode;
            obItem.ItemCode = "code";
            obItem.ItemName = objaddproduct.ItemName;
            obItem.ShortDescription = objaddproduct.ShortDescription;
            obItem.LongDescription = objaddproduct.LongDescription;
            obItem.ItemPrice = objaddproduct.ItemPrice;
            obItem.SmallImagePath = objaddproduct.SmallImagePath;
            obItem.LongImagePath = objaddproduct.LongImagePath;
            obItem.ItemId = Guid.NewGuid();

            dbobj.Items.Add(obItem);
            dbobj.SaveChanges();
            return View();
        }
    }
}