using LastClinc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LastClinc.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminstrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminstrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = viewModel.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Adminstration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} not found";
                return View("NotFound");
            }
            var model = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleViewModel viewModel)
        {
            var role = await roleManager.FindByIdAsync(viewModel.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {viewModel.Id} not found";
                return View("NotFound");
            }
            role.Name = viewModel.RoleName;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles", "Adminstration");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var userInRole = await userManager.GetUsersInRoleAsync(role.Name);
            if (role == null)
            {
                ViewBag.ErrorMessage($"Role with id = {id} not found");
                return View("NotFound");
            }
            else if (userInRole.Count == 0)
            {
                var result = await roleManager.DeleteAsync(role);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else if (userInRole.Count > 0)
            {
                ViewBag.ErrorMessage($"Sorry can not remove role {role.Name} that has users");
                return View("NotFound");
            }
            return RedirectToAction("ListRoles");
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage($"Role with id = {roleId} not found");
                return View("NotFound");
            }
            if (await CheackRole(roleId))
            {
                var model = new List<UserRoleViewModel>();
                foreach (var user in userManager.Users)
                {
                    var userViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        userViewModel.IsSelected = true;
                    }
                    else
                    {
                        userViewModel.IsSelected = false;
                    }
                    model.Add(userViewModel);
                }
                return View(model);
            }
            else
            {
                return View("NotFound");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage($"Role with id = {roleId} not found");
                return View("NotFound");
            }
            for (int item = 0; item < model.Count; item++)
            {
                var user = await userManager.FindByIdAsync(model[item].UserId);
                IdentityResult result = null;
                if (model[item].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (model[item].IsSelected == false && await userManager.IsInRoleAsync(user, role.Name))
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction("ListRoles", new { id = roleId });
        }
        private async Task<bool> CheackRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage($"Role with id = {id} not found");
                return false;
            }
            return true;
        }
    }
}
