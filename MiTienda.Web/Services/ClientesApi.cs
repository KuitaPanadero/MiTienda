using System.Net.Http.Json;
using MiTienda.Web.Models;

namespace MiTienda.Web.Services
{
    public class ClientesApi
    {
        private readonly HttpClient _http;

        public ClientesApi(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Cliente>> Listar()
        {
            return await _http.GetFromJsonAsync<List<Cliente>>("api/clientes")
                   ?? new List<Cliente>();
        }

        public async Task<Cliente?> Buscar(int id)
        {
            return await _http.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
        }

        public async Task<bool> Crear(Cliente cliente)
        {
            var res = await _http.PostAsJsonAsync("api/clientes", cliente);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> Editar(Cliente cliente)
        {
            var res = await _http.PutAsJsonAsync($"api/clientes/{cliente.Id}", cliente);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> Borrar(int id)
        {
            var res = await _http.DeleteAsync($"api/clientes/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
