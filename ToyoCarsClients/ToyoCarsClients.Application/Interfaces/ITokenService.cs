using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyoCarsClients.Domain.Entities;

namespace ToyoCarsClients.Application.Interfaces
{
    public interface ITokenService
    {
       public string GenerateToken(User user);
    }
}
