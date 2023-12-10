using MusicApp.Usuario.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Repository
{
    public class PlanoRepository
    {
        private HttpClient HttpClient { get; set; }

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        public PlanoRepository()
        {
            this.HttpClient = new HttpClient();
        }


        public async Task<Plano> ObterPlanoPorId(Guid id)
        {
            var response = await this.HttpClient.GetAsync($"https://localhost:7133/api/Plano/{id}");

            if (response.IsSuccessStatusCode == false)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            /*
            Dictionary<String, Dictionary<String, Object>>  dictPlano = 
                JsonSerializer.Deserialize<Dictionary<string, Dictionary<String, Object>>>(content);

            Plano plano = new Plano()
            {
                Id = new Guid(dictPlano["plano"]["idPlano"].ToString()),
                Nome = dictPlano["plano"]["nome"].ToString(),
                Valor = decimal.Parse(dictPlano["plano"]["valor"].ToString())
            };
            */

            Plano plano = JsonSerializer.Deserialize<Plano>(content, options);

            return plano;
        }
    }
}
