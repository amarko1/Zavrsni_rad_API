﻿using AutoMapper;
using DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Dto;
using ServiceLayer.Provider;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _repository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public AuthResponse Login(LoginRequest loginRequest)
        {
            if (!Authenticate(loginRequest.Email, loginRequest.Password))
            {
                return new AuthResponse { IsSuccessful = false, Message = "Invalid email or password" };
            }

            JwtTokenProvider jwtTokenProvider = new(_configuration);
            var refreshToken = jwtTokenProvider.GenerateRefreshToken();

            _repository.UpdateRefreshToken(refreshToken, loginRequest.Email);
            _repository.Save();

            return new LoginResponse
            {
                IsSuccessful = true,
                Message = "Login successful",
                Tokens = new()
                {
                    AccessToken = jwtTokenProvider.GenerateAccessToken(new JwtTokenBodyInfo
                    {
                        Email = loginRequest.Email
                    }),
                    RefreshToken = refreshToken
                }
            };
        }

        public AuthResponse Logout(string email)
        {
            _repository.UpdateRefreshToken(null, email);
            _repository.Save();

            return new AuthResponse { IsSuccessful = true, Message = "Logout successful" };
        }

        public AuthResponse RefreshToken(RefreshRequest refreshRequest)
        {
            var user = _repository.GetUser(u => u.RefreshToken!.Equals(refreshRequest.RefreshToken));

            if (user == null)
            {
                return new AuthResponse { IsSuccessful = false, Message = "Invalid refresh token" };
            }

            JwtTokenProvider jwtTokenProvider = new(_configuration);

            var refreshToken = jwtTokenProvider.Refresh(refreshRequest.ExpiredAccessToken, refreshRequest.RefreshToken);

            return new LoginResponse
            {
                IsSuccessful = true,
                Message = "Token refreshed",
                Tokens = new()
                {
                    AccessToken = refreshToken,
                    RefreshToken = refreshRequest.RefreshToken
                }
            };
        }

        public AuthResponse Register(RegisterRequest registerRequest)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string b64Salt = Convert.ToBase64String(salt);

            byte[] hash =
                KeyDerivation.Pbkdf2(
                    password: registerRequest.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8);

            string b64Hash = Convert.ToBase64String(hash);

            _repository.CreateUser(new()
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PwdSalt = b64Salt,
                PwdHash = b64Hash
            });

            _repository.Save();

            return new AuthResponse { IsSuccessful = true, Message = "Registration successful" };
        }

        private bool Authenticate(string email, string password)
        {
            var user = _repository.GetUser(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(user.PwdSalt);
            byte[] hash = Convert.FromBase64String(user.PwdHash);

            byte[] calcHash =
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8);

            return hash.SequenceEqual(calcHash);
        }

        public void DisableUser(int id)
        {
            var user = _repository.GetUser(u => u.Id == id);

            if (!user.IsDisabled)
            {
                user.IsDisabled = true;
                _repository.Save();
            }
        }

        public void EnableUser(int id)
        {
            var user = _repository.GetUser(u => u.Id == id);

            if (user.IsDisabled)
            {
                user.IsDisabled = false;
                _repository.Save();
            }
        }

        public int GetUserIdByEmail(string email)
        {
            var user = _repository.GetUser(u => u.Email == email);
            return user!.Id;
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _repository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
