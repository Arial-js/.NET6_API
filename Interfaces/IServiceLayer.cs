using EsercitazioneAPI.Models;

namespace EsercitazioneAPI.Interfaces
{
    public interface IServiceLayer
    {
        Task<IEnumerable<UsersModel>> ListAllAsync();
        Task<UsersModel> GetByIdAsync(int id);
        Task<UsersModel> GetByEmailAsync(string email);
        Task<UsersModel> CreateAsync(UsersModel user);
        Task<UsersModel> CreateAsAdminAsync(UsersModel user);
        Task<UsersModel> UpdateAsync(int id, UsersModel user);
        Task DeleteAsync(int id);
    }
}
