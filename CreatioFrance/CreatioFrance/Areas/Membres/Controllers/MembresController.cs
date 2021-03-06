﻿using CreatioFrance.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using CreatioFrance.Controllers;
using System.Threading.Tasks;
using CreatioFrance.Areas.Membres.Models;
using CreatioUsersBusiness;
using System.Web.Security;
using CreatioFranceEntities.Users;

namespace CreatioFrance.Areas.Membres.Controllers
{
   
    public class MembresController : AccountController
    {
        #region Members
        private IUsersManagment _usersManagment = UsersManagment.GetInstance;
        #endregion

        // GET: Test/Test
        public ActionResult Index()
        {
            return View();
        }

        #region Public Methods
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
                return View();
        }

       
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(MembersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Register.Email, Email = model.Register.Email };
                var result = await UserManager.CreateAsync(user, model.Register.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, eRolesInfo.CreatioMembers.ToString());

                    model.Users.Informations.Email = model.Register.Email;
                    model.Users.Informations.Id = user.Id;
                    await _usersManagment.SaveUserInformation(model.Users.Informations);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code, area = "" }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", callbackUrl);

                    return RedirectToAction("Index", "Membres");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion
    }
}