using ExamenOpdracht.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users;
        return View(users);
    }

    public IActionResult ManageRoles(string userId)
    {
        var user = _userManager.FindByIdAsync(userId).Result;
        var roles = _roleManager.Roles;
        var userRoles = _userManager.GetRolesAsync(user).Result;

        var model = new ManageUserRolesViewModel
        {
            UserId = userId,
            UserName = user.UserName,
            UserRoles = userRoles,
            AllRoles = roles
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddToRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.AddToRoleAsync(user, roleName);

        return RedirectToAction("ManageRoles", new { userId });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.RemoveFromRoleAsync(user, roleName);

        return RedirectToAction("ManageRoles", new { userId });
    }
}
