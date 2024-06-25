﻿using ServiceLayer.Dto;
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
        AuthResponse Register(RegisterRequest registerRequest);
        AuthResponse Login(LoginRequest loginRequest);
        AuthResponse RefreshToken(RefreshRequest refreshRequest);
        AuthResponse Logout(string email);
        void DisableUser(int id);
        void EnableUser(int id);
        List<UserDto> GetAllUsers();
    }
}
