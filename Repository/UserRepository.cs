using EsercitazioneAPI.Database;
using EsercitazioneAPI.Models;
using EsercitazioneAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using EsercitazioneAPI.Models.DTO;
using AutoMapper;

namespace EsercitazioneAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UsersModel> Get(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<UsersModel> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UsersModel>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UsersModel> Create(UsersModel user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UsersModel> CreateAsAdmin(UsersModel user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task Update(UsersModel user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userToDelete = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
