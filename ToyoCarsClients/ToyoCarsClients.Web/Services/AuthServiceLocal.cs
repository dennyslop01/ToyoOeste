using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ToyoCarsClients.Web.Services
{
    public class AuthServiceLocal(AuthenticationStateProvider authenticationStateProvider)
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity!.IsAuthenticated;
        }

        public async Task<string?> GetUserNameAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("unique_name")?.Value;
        }

        public async Task<string?> GetNameAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("Nombre")?.Value;
        }

        public async Task<string?> GetUserRoleAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
        }

        public async Task<int> GetUserIdAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userIdClaim = user.FindFirst("UserId")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public async Task<string?> GetTipoNumberDNIAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            string tiponumberDni = string.Empty;
            try
            {
                if (user.FindFirst("TipoDni")?.Value.Trim() == "V")
                    tiponumberDni = "V-" + user.FindFirst("DniNumeber")?.Value.Trim();
                else
                {
                    string dniNumber = user.FindFirst("DniNumeber")?.Value.Trim() ?? "";
                    string formateado = dniNumber.Substring(0, dniNumber.Length - 1) + "-" + dniNumber.Substring(dniNumber.Length - 1, 1);
                    tiponumberDni = user.FindFirst("TipoDni")?.Value.Trim() + "-" + formateado;
                }
            }
            catch
            {
                tiponumberDni = string.Empty;
            }

            return tiponumberDni;
        }
    }
}