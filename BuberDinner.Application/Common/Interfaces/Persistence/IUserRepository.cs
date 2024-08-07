using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetUserByEmail(string email);
    }
}