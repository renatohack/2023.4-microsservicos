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
            this.Playlists = new List<Playlist>();
            this.Playlists.Add(new Playlist() { Nome = "Favoritas" });

            this.BandasFavoritas = new List<Banda>();
            this.Assinaturas = new List<Assinatura>();
            this.Cartoes = new List<CartaoCredito>();
        }


        public static Usuario CriarUsuario() { return new Usuario(); }

        public void AdicionarCartaoCredito(CartaoCredito cartao)
        {
            this.Cartoes.Add(cartao);
        }

        public void AssinarPlano(Plano plano, CartaoCredito cartao) { }

        public void CriarPlaylist(string nome) { }

        public void FavoritarBanda(string nome) { }

    }
}
