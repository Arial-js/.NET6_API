using System.ComponentModel;

namespace Dotnet6_API.Models.User
{
    public enum UsersRolesModel
    {
        [Description("Admin")]
        Admin,
        [Description("Dev")]
        Developer,
        [Description("Sec")]
        Security,
        [Description("External")]
        Intern
    }
}
