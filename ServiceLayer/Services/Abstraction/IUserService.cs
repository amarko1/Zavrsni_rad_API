using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface IUserService
    {
        int GetUserIdByEmail(string email);
        AuthResponse Register(UserRegistrationRequest registerRequest);
        LoginResponse Login(LoginRequest loginRequest);
        AuthResponse RefreshToken(RefreshRequest refreshRequest);
        AuthResponse Logout(string email);
        void DisableUser(int id);
        void EnableUser(int id);
        List<UserDto> GetAllUsers();
        public UserDto GetUserById(int id);
        void UpdateUserRole(int userId, string newRole);
    }
}
