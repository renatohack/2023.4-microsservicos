using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Repository
{
    public class BandaRepository
    {
        private HttpClient HttpClient { get; set; }

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public BandaRepository()
        {
            this.HttpClient = new HttpClient();
        }

        public async Task<Banda> ObterBandaPorId(Guid id)
        {
            var response = await this.HttpClient.GetAsync($"https://localhost:7262/api/Banda/{id}");

            if (response.IsSuccessStatusCode == false)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            Banda banda = JsonSerializer.Deserialize<Banda>(content, options);

            return banda;
        }
    }
}
