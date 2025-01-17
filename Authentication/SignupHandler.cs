﻿using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Test.Models;

namespace Test.Authentication
{
    public class SignupHandler
    {
        private readonly AppDbContext dbContext;

        public SignupHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string HashingPassword(string password)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);

            return hashed;
        }
        public bool isValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, "[a-zA-Z0-9._-]+@[a-z]+\\.+[a-z]+", RegexOptions.IgnoreCase,TimeSpan.FromMilliseconds(250));
        }
        public bool isExistEmail(string email)
        {
            //return dbContext.Set<User>().Where(u => u.Email == email).Any();
            return dbContext.Set<User>().Any(u => u.Email == email);
        }

        public bool isValidHeight(double height)
        {
            return height > 0;
        }
        public bool isValidWeight(double weight)
        {
            return weight > 0;
        }
        public bool isValidAge(int age)
        {
            return age > 0;
        }
        public int CreateUser(User user)
        {
            user.Id = 0;
            dbContext.Set<User>().Add(user);
            dbContext.SaveChanges();
            return user.Id;
        }
        public void AddChronicsToUser(int id, List<int> list)
        {

            var ucd = new UserChronicDisease();
            ucd.user_id = id;
            foreach (var item in list)
            {
                ucd.disease_id = item;
                dbContext.Set<UserChronicDisease>().Add(ucd);
                dbContext.SaveChanges();
            }
            
        }
    }
}
