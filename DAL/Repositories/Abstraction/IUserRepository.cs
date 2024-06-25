using Azure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstraction
{
    public interface IUserRepository
    {
        void CreateUser(User newUser);
        User? GetUser(Expression<Func<User, bool>> filter);
        void UpdateRefreshToken(string? refreshToken, string userEmail);
        void Save();
        IEnumerable<User> GetAll();

        //Task<T?> GetUserAsyncBy(Expression<Func<T, bool>> filter);
        bool CheckIfUsernameExists(string username);
        bool CheckIfEmailExists(string email);
    }
}
