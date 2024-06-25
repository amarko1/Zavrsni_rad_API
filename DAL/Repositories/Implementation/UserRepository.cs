using DAL.AppDbContext;
using DAL.Models;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateUser(User newUser)
        {
            newUser.CreatedAt = DateTime.Now;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public User? GetUser(Expression<Func<User, bool>> filter)
        {
            return _context.Users.FirstOrDefault(filter);
        }

        public void UpdateRefreshToken(string? refreshToken, string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(userEmail));

            if (refreshToken != null)
            {
                user!.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(2);
            }
            else
            {
                user!.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public bool CheckIfEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email.Equals(email));
        }

        public bool CheckIfUsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username.Equals(username));
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
