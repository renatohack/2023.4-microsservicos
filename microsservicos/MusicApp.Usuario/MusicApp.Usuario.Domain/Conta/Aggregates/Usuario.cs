using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Usuario.Domain.Aggregates;


namespace MusicApp.Usuario.Domain.Aggregates
{
    public class Usuario
    {

        // Info
        public Guid Id { get; set; }
        public String Nome { get; set; }

        // Pagamento
        public List<Cartao> Cartoes { get; set; }
        public List<Assinatura> Assinaturas { get; set; }

        // App
        public List<Playlist> Playlists { get; set; }
        public List<Banda> BandasFavoritas { get; set; }

        // Construtor
        public Usuario() 
        {

            this.Id = Guid.NewGuid();

            this.Cartoes = new List<Cartao>();
            this.Assinaturas = new List<Assinatura>();

            this.Playlists = new List<Playlist> {
                new Playlist() { Nome = "Favoritas" }
            };

            this.BandasFavoritas = new List<Banda>();
        }


        public static Usuario CriarUsuario(string nome, Cartao cartao, Plano plano)
        {
            Usuario usuario = new Usuario() {
                Nome = nome,
            };

            usuario.AdicionarCartao(cartao);
            usuario.AssinarPlano(new Empresa().Cnpj, plano, cartao);

            return usuario;
        }

        public void AdicionarCartao(Cartao cartao) => this.Cartoes.Add(cartao);

        public Assinatura AssinarPlano(string cnpj, Plano plano, Cartao cartao) 
        {

            cartao.RealizarTransacao(cnpj, plano.Valor);

            // Cancela ultima assinatura
            if (Assinaturas.Any()) Assinaturas.Last().AssinaturaAtiva = false;

            // Gera nova assinatura
            Assinatura assinatura = new Assinatura() {
                Plano = plano,
                AssinaturaAtiva = true
            };

            // Adiciona assinatura ao usuario
            this.Assinaturas.Add(assinatura);

            return assinatura;

        }

        public Playlist CriarPlaylist(string nome) 
        {
            Playlist playlist = new Playlist()
            {
                Nome = nome
            };

            this.Playlists.Add(playlist);

            return playlist;
        }

        public void FavoritarBanda(Banda banda) => this.BandasFavoritas.Add(banda);

        public List<Playlist> BuscarPlaylist(string nome) => this.Playlists.Where(playlist => playlist.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

        public List<Banda> BuscarBanda(string nome) => this.BandasFavoritas.Where(banda => banda.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

        public Cartao BuscarCartaoPorId(Guid idCartao) => this.Cartoes.FirstOrDefault(cartao => cartao.Id == idCartao);

    }
}
