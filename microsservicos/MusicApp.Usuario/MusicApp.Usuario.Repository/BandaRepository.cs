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

            Dictionary<String, Dictionary<String, Object>> dictPlano =
                JsonSerializer.Deserialize<Dictionary<string, Dictionary<String, Object>>>(content);



            Banda banda = JsonSerializer.Deserialize<Banda>(content);

            return banda;
        }
    }
}
