﻿using NextItemBuy.Domain;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Mapper;
using NextItemBuy.Services.Model;
using NextItemBuy.Services.Utils;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;

namespace NextItemBuy.Services.Implementation
{
    public class AuthenticationService: IAuthenticationService
    {
        public UserModel Login(LoginModel model)
        {
            var validator = new LoginModelValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                throw new CustomException(result.Errors.Select(x => new ValidationRecord(x.PropertyName, x.ErrorMessage)).ToList());
            }

            using (var ctx = new NextItemBuyEntities())
            {
                var user = ctx.Users.FirstOrDefault(x => x.Username.Equals(model.Username) || x.Email.Equals(model.Username));
                if (user == null)
                {
                    throw new ApplicationException("User not found!");
                }

                if (!EncriptionUtil.VerifyMd5Hash(model.Password, user.Password))
                {
                    throw new ApplicationException("Wrong Password");
                }

                return user.ToViewModel();
            }
        }

        public void Register(UserModel model)
        {
            var validator = new UserModelValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                throw new CustomException(result.Errors.Select(x => new ValidationRecord(x.PropertyName, x.ErrorMessage)).ToList());
            }

            using(var ctx = new NextItemBuyEntities())
            {
                var userExists = ctx.Users.FirstOrDefault(x => x.Email == model.Email);
                if (userExists != null)
                {
                    throw new ApplicationException("User already exists!");
                }

                var hashedPass = EncriptionUtil.EncryptPassword(model.Password);

                var newuser = new User(model.FirstName, model.LastName, model.Email, model.UserName, hashedPass);

                ctx.Users.Add(newuser);
                ctx.SaveChanges();
            }
        }
    }
}
