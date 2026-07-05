using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyoCarsClients.Domain.DTOs.Request;
using ToyoCarsClients.Domain.DTOs.Response;

namespace ToyoCarsClients.Business.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);

        Task<UserResponseDto> Register(CreateUserDto createUser);
        Task<UserResponseDto> ResetPassword(ResetUserDto createUser);
        Task<UserResponseDto> ActualizarDatos(UpdateUserDto createUser);
    }
}
