﻿using SCADA.Utils;
using SCADA.DTOs;
using SCADA.Models;
using SCADA.Repositories;

namespace SCADA.Services
{
    public class UserService
    {
        public static void Login(LoginDTO userCredentials)
        {
            var user = UserRepository.GetByUsername(userCredentials.Username) ?? throw new ArgumentException("Username or password do not match!");
            if (user.Password != userCredentials.Password)
                throw new ArgumentException("Username or password do not match!");
            UserRepository.Login(user);
        }

        public static void Logout(string username)
        {
            var user = UserRepository.GetByUsername(username) ?? throw new ArgumentException("Username or password do not match!");
            UserRepository.Logout(user);
        }

        public static void Register(UserDTO userInfo)
        {
            if (UserRepository.GetByUsername(userInfo.Username) != null)
                throw new ArgumentException("Username already in use!");
            var user = new User(userInfo);
            UserRepository.Save(user);
        }

        public static bool GetIsLoggedIn(string username)
        {
            User user = UserRepository.GetByUsername(username);
            if (user == null)
                throw new ArgumentException("Username does not exist!");
            return user.IsLoggedIn;
        }

        public static List<UserDTO> GetAllUsers()
        {
            List<User> users = UserRepository.GetAll();
            List<UserDTO> usersDtos = new List<UserDTO>();
            foreach (var user in users)
            {
                usersDtos.Add(new UserDTO(user));
            }
            return usersDtos;
        }
    }
}
