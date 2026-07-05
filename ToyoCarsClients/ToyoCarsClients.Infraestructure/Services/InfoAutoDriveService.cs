using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using ToyoCarsClients.Domain.DTOs.Request;
using ToyoCarsClients.Domain.DTOs.Response;

namespace ToyoCarsClients.Infraestructure.Services
{
    public class InfoAutoDriveService
    {
        private readonly IConfiguration _config;
        public string rutaBase = string.Empty;
        public string usuario = string.Empty;
        public string clave = string.Empty;
        public string serLoging = string.Empty;
        public string serLogout = string.Empty;
        public string serVehiculoCedula = string.Empty;
        public string serVehDetalCedula = string.Empty;
        public string serUpdateCliente = string.Empty;
        public string serDatosCliente = string.Empty;

        public InfoAutoDriveService(IConfiguration config)
        {
            _config = config;
            rutaBase = _config["AppSettings:RutaBase"] ?? string.Empty;
            usuario = _config["AppSettings:Usuario"] ?? string.Empty;
            clave = _config["AppSettings:Clave"] ?? string.Empty;
            serLoging = _config["AppSettings:serLogin"] ?? string.Empty;
            serLogout = _config["AppSettings:serLogout"] ?? string.Empty;
            serVehiculoCedula = _config["AppSettings:serVehiculoCedula"] ?? string.Empty;
            serVehDetalCedula = _config["AppSettings:serVehDetalCedula"] ?? string.Empty;
            serUpdateCliente = _config["AppSettings:serUpdateCliente"] ?? string.Empty;
            serDatosCliente = _config["AppSettings:serDatosCliente"] ?? string.Empty;
        }

        private async Task<LoginResponse> LoginService()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{rutaBase}{serLoging}");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("cod_usuario", usuario));
            collection.Add(new("clave", clave));
            request.Content = new FormUrlEncodedContent(collection);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // Read once and deserialize explicitly (handles array -> take first element)
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);

            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var list = JsonSerializer.Deserialize<List<LoginResponse>>(json, options);
            return list?.FirstOrDefault() ?? throw new InvalidOperationException("Login response did not contain a valid LoginResponse object.");
        }

        private async Task<HttpResponseMessage> LogoutService(string? token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{rutaBase}{serLogout}");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim());
            }

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return response;
        }

        public async Task<List<VehiculoTaller>> GetVehiculosPorCedula(string cedula)
        {
            var response = new HttpResponseMessage();
            var vehiculos = new List<VehiculoTaller>();

            string? token = null;

            try
            {
                var loginData = await LoginService();
                token = $"{loginData.Token.Trim('"')}";

                if (string.IsNullOrWhiteSpace(token))
                    throw new InvalidOperationException("Login returned no token.");

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{rutaBase}{serVehDetalCedula}");

                request.Headers.TryAddWithoutValidation("Authorization", token);

                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new("cedula_rif", cedula)); // use provided cedula
                
                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var bodyerror = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status: {(int)response.StatusCode} {response.ReasonPhrase}; Body: {bodyerror}");

                    return vehiculos;
                }

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);

                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                vehiculos = JsonSerializer.Deserialize<List<VehiculoTaller>>(json, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // pass token (may be null) so logout can include auth if available
                var logoutResponse = await LogoutService(token);
            }

            return vehiculos;
        }

        public async Task<DatosCliente> GetDatsClientesPorCedula(string cedula)
        {
            var response = new HttpResponseMessage();
            var cliente = new DatosCliente();

            string? token = null;

            try
            {
                var loginData = await LoginService();
                token = $"{loginData.Token.Trim('"')}";

                if (string.IsNullOrWhiteSpace(token))
                    throw new InvalidOperationException("Login returned no token.");

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{rutaBase}{serDatosCliente}");

                request.Headers.TryAddWithoutValidation("Authorization", token);

                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new("cedula_rif", cedula)); // use provided cedula

                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var bodyerror = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status: {(int)response.StatusCode} {response.ReasonPhrase}; Body: {bodyerror}");

                    return cliente;
                }

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);

                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                List<DatosCliente> clienteaux = JsonSerializer.Deserialize<List<DatosCliente>>(json, options);

                cliente = clienteaux?.FirstOrDefault() ?? new DatosCliente();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // pass token (may be null) so logout can include auth if available
                var logoutResponse = await LogoutService(token);
            }

            return cliente;
        }

        public async Task<DatosCliente> ActualizarDatosClientes(UpdateUserDto datos)
        {
            var response = new HttpResponseMessage();
            var cliente = new DatosCliente();

            string? token = null;

            try
            {
                var loginData = await LoginService();
                token = $"{loginData.Token.Trim('"')}";

                if (string.IsNullOrWhiteSpace(token))
                    throw new InvalidOperationException("Login returned no token.");

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{rutaBase}{serUpdateCliente}");

                request.Headers.TryAddWithoutValidation("Authorization", token);

                var listaTelefonos = new List<TelefonoJson>();
                if (!string.IsNullOrWhiteSpace(datos.MovilNumber))
                {
                    listaTelefonos.Add(new TelefonoJson
                    {
                        codAreaTel = datos.MovilNumber.Substring(0, 4), // Ejemplo: 0416
                        telefono = datos.MovilNumber.Substring(4),     // Ejemplo: 1111112
                        tipo = "movil"
                    });
                }

                if (!string.IsNullOrWhiteSpace(datos.PhoneNumber))
                {
                    // Para teléfonos fijos (particular), ajusta el Substring según el largo del código (ej. 3 dígitos para 0212)
                    listaTelefonos.Add(new TelefonoJson
                    {
                        codAreaTel = datos.PhoneNumber.Substring(0, 3),
                        telefono = datos.PhoneNumber.Substring(3),
                        tipo = "particular"
                    });
                }

                string jsonFinal = JsonSerializer.Serialize(listaTelefonos);

                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new("cedula_rif", datos.DNINumber));
                collection.Add(new("correo", datos.Email));
                collection.Add(new("telefono", jsonFinal));

                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var bodyerror = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status: {(int)response.StatusCode} {response.ReasonPhrase}; Body: {bodyerror}");

                    return cliente;
                }

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);

                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                List<DatosCliente> clienteaux = JsonSerializer.Deserialize<List<DatosCliente>>(json, options);

                cliente = clienteaux?.FirstOrDefault() ?? new DatosCliente();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // pass token (may be null) so logout can include auth if available
                var logoutResponse = await LogoutService(token);
            }

            return cliente;
        }
    }
}
