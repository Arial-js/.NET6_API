using Dotnet6_API.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsersModel>> GetAll();
        Task<UsersModel> Get(int id);
        Task<UsersModel> GetByEmail(string email);
        Task<UsersModel> Create(UsersModel model);
        Task<UsersModel> CreateAsAdmin(UsersModel model);
        Task Update(UsersModel model);
        Task Delete(int id);
    }
}
