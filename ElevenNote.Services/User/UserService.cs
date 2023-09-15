using ElevenNote.Data;
using ElevenNote.Data.Entities;
using ElevenNote.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElevenNote.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (await GetUserByEmailAsync(model.Email) != null || await GetUserByUsernameAsync(model.Username) != null)
            { return false; }

            var entity = new UserEntity
            {
                Email = model.Email,
                Username = model.Username,                
                DateCreated = DateTime.UtcNow,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity, model.Password);
            /* first solution
            UserEntity CheckEmail = _context.Users.FirstOrDefault(e => e.Email == model.Email);
            if (CheckEmail != null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                CheckEmail.Username = entity.Email;
                return false;
            }

            UserEntity CheckUsername = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (CheckUsername.Username != null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(model.Username))
            {
                CheckUsername.Username = entity.Username;
                return false;
            }
            */
            _context.Users.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(User => User.Email == email.ToLower());
        }

        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(User => User.Username.ToLower() == username.ToLower());
        }

        public async Task<UserDetail> GetUserByIdAsync(int userId)
        {
            var entity = await _context.Users.FindAsync(userId);
            if (entity is null)
                return null;

            var userDetail = new UserDetail
            {
                Id = entity.Id,
                Email = entity.Email,
                Username = entity.Username,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateCreated = entity.DateCreated,
            };
            return userDetail;
        }
    }
}
