using Dotnet6_API.Models.User;
using EsercitazioneAPI.Exceptions;
using EsercitazioneAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneAPI.Service
{
    public class UserServiceLayer : IServiceLayer
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncrypter _passwordEncrypter;

        public UserServiceLayer(IUserRepository userRepository, IPasswordEncrypter passwordEncrypter)
        {
            _userRepository = userRepository;
            _passwordEncrypter = passwordEncrypter;
        }

        public async Task<IEnumerable<UsersModel>> ListAllAsync()
        {
            return await _userRepository.GetAll();
        }

        public async Task<UsersModel> GetByIdAsync(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<UsersModel> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<UsersModel> CreateAsync(UsersModel user)
        {
            if(GetByEmailAsync(user.Email) != null)
            {
                throw new DuplicateEmailException(); 
            }
            string salt = _passwordEncrypter.GetSalt();
            user.Password = _passwordEncrypter.ComputeSha512Hash(user.Password, salt);
            user.Role = UsersRolesModel.Intern;
            return await _userRepository.Create(user);
        }

        public async Task<UsersModel> CreateAsAdminAsync(UsersModel user)
        {
            if (GetByEmailAsync(user.Email) != null)
            {
                throw new DuplicateEmailException();
            }
            string salt = _passwordEncrypter.GetSalt();
            user.Password = _passwordEncrypter.ComputeSha512Hash(user.Password, salt);
            return await _userRepository.Create(user);
        }

        public async Task<UsersModel> UpdateAsync(int id, UsersModel user)
        {
            if (id != user.Id)
            {
                throw new BadHttpRequestException("Bad Request", 400);
            }
            string salt = _passwordEncrypter.GetSalt();
            user.Password = _passwordEncrypter.ComputeSha512Hash(user.Password, salt);
            await _userRepository.Update(user);
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _userRepository.Get(id);
            if (userToDelete == null)
            {
                throw new NotFoundException();
            }
            await _userRepository.Delete(userToDelete.Id);
        }
    }
}
