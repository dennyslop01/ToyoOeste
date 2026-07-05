using AutoMapper;
using ToyoCarsClients.Application.Interfaces;
using ToyoCarsClients.Business.Interfaces;
using ToyoCarsClients.Domain.DTOs.Request;
using ToyoCarsClients.Domain.DTOs.Response;
using ToyoCarsClients.Domain.Entities;

namespace ToyoCarsClients.Business.Services
{
    public class AuthService(ITokenService tokenService, IUserRepository userRepository, IMapper mapper) : IAuthService
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public Task<UserResponseDto> ActualizarDatos(UpdateUserDto createUser)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            var isAuthenticated = user != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);
            if (isAuthenticated)
            {
                var token = _tokenService.GenerateToken(user!);
                return new LoginResponseDto { IsAuthenticated = true, Token = token, Message = "Inicio de sesion exitoso" };
            }
            else
            {
                return new LoginResponseDto { IsAuthenticated = false, Token = string.Empty, Message = "Credenciales incorrectas" };
            }
        }

        public async Task<UserResponseDto> Register(CreateUserDto createUser)
        {
            var user = new User
            {
                Name = createUser.Name.Trim(),
                Email = createUser.Email.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.Password),
                MovilNumber = createUser.MovilNumber.Trim(),
                DNINumber = createUser.DNINumber.Trim(),
                TipoDNI = createUser.TipoDNI.Trim()
            };

            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserResponseDto>(createdUser);
        }

        public async Task<UserResponseDto> ResetPassword(ResetUserDto createUser)
        {
            var user = new User
            {
                Email = createUser.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.Password),
                MovilNumber = createUser.MovilNumber,
                DNINumber = createUser.DNINumber
            };

            var createdUser = await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(createdUser);
        }
    }
}
