using Microsoft.AspNetCore.Identity;

internal class ManageUserRolesViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public IList<string> UserRoles { get; set; }
    public IQueryable<IdentityRole> AllRoles { get; set; }
}