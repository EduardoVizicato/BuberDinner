using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // 1 validate user exists

            if(_userRepository.GetUserByEmail(email) is not User user){
                throw new Exception("The User with given email doesn't exists");
            }

            // 2 validate password

            if(user.Password != password){
                throw new Exception("The password is incorrect");
            }

            // 3 Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
            );
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // 1 Validate the user doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null){
                throw new DuplicateEmailException();
            }

            // 2 Create a new user (generate unique Id) & Persist to DB
            var user = new User{
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);
            //create JWT token

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}