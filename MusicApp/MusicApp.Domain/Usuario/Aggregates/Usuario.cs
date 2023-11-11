using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Domain.Aplicativo.Aggregates;


namespace MusicApp.Domain.Usuario.Aggregates
{
    public class Usuario
    {

        // Info
        public Guid Id { get; set; }
        public String Nome { get; set; }

        // Pagamento
        public List<CartaoCredito> Cartoes { get; set; }
        public List<Assinatura> Assinaturas { get; set; }

        // App
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }

        // Construtor
        public Usuario() 
        {

            this.Id = Guid.NewGuid();

            this.Cartoes = new List<CartaoCredito>();
            this.Assinaturas = new List<Assinatura>();

            this.Playlists = new List<Playlist> {
                new Playlist() { Nome = "Favoritas" }
            };

            this.BandasFavoritas = new List<Banda>();
        }


        public void AdicionarCartaoCredito(CartaoCredito cartao) => this.Cartoes.Add(cartao);

        public bool CartaoCreditoValido(CartaoCredito cartao) => cartao.CartaoAtivo && cartao.LimiteDisponivel > 0;


        public void AssinarPlano(Plano plano, CartaoCredito cartao) 
        { 

            // Verifica limite disponivel e se cartao esta ativo
            if (cartao.LimiteDisponivel > plano.Valor && cartao.CartaoAtivo) {
                
                // Cancela ultima assinatura
                if (Assinaturas.Any()) Assinaturas.Last().AssinaturaAtiva = false;

                // Gera ultima assinatura
                Assinatura assinatura = new Assinatura() {
                    Plano = plano,
                    AssinaturaAtiva = true
                };

                // Adiciona assinatura ao usuario
                this.Assinaturas.Add(assinatura);

            }
            else
            {
                // TODO
            }
        }

        public void CriarPlaylist(string nome) => this.Playlists.Add(new Playlist() { Nome = nome });

        public void FavoritarBanda(Banda banda) => this.BandasFavoritas.Add(banda);

        public List<Playlist> BuscarPlaylist(string nome) => this.Playlists.Where(playlist => playlist.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

        public List<Banda> BuscarBanda(string nome) => this.BandasFavoritas.Where(banda => banda.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

    }
}
