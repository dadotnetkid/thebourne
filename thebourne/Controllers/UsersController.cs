using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using thebourne.Models;

namespace thebourne.Controllers
{
    public class UsersController : AccountController
    {
        private TheBournEntity db = new TheBournEntity();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UsersGridViewPartial()
        {
            var model = db.AspNetUsers.ToList();
            return PartialView("_UsersGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UsersGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] thebourne.Models.AspNetUsers item)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    ApplicationUser user = new ApplicationUser() { UserName = item.Email, Email = item.Email, FirstName = item.FirstName, LastName = item.LastName };
                    var result = await UserManager.CreateAsync(user, item.Password);
                    if (result.Succeeded)
                    {
                        var role = await UserManager.AddToRoleAsync(user.Id, item.UserRoles);
                    }
                    else
                    {
                        ViewData["EditError"] = string.Join(Environment.NewLine, result.Errors);
                    }
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = db.AspNetUsers.ToList();
            return PartialView("_UsersGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UsersGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] thebourne.Models.AspNetUsers item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var users = await UserManager.FindByIdAsync(item.Id);
                    users.LastName = item.LastName;
                    users.FirstName = item.FirstName;
                    users.PhoneNumber = item.PhoneNumber;
                    foreach (var i in users.Roles.ToList())
                    {
                        await UserManager.RemoveFromRoleAsync(users.Id, db.AspNetRoles.Find(i.RoleId).Name);

                    }

                    await UserManager.AddToRoleAsync(users.Id, item.UserRoles);

                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = db.AspNetUsers.ToList();
            return PartialView("_UsersGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UsersGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.String Id)
        {
            if (Id != null)
            {
                try
                {
                    await UserManager.DeleteAsync(await UserManager.FindByIdAsync(Id));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = db.AspNetUsers.ToList();
            return PartialView("_UsersGridViewPartial", model);
        }

        public ActionResult AddEditUsersPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            string UserId)
        {
            return PartialView("AddEditUsersPartial", db.AspNetUsers.Find(UserId));
        }
    }
}