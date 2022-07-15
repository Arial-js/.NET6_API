using System.ComponentModel;

namespace EsercitazioneAPI.Models
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
