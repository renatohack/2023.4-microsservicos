using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Domain.Aplicativo.Aggregates;


namespace MusicApp.Domain.Conta.Aggregates
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


        public static Usuario CriarUsuario(string nome, CartaoCredito cartao, Plano plano)
        {
            Usuario usuario = new Usuario() {
                Nome = nome,
            };

            usuario.AdicionarCartaoCredito(cartao);
            usuario.AssinarPlano(new Empresa().Cnpj, plano, cartao);

            return usuario;
        }

        public void AdicionarCartaoCredito(CartaoCredito cartao) => this.Cartoes.Add(cartao);

        public void AssinarPlano(string cnpj, Plano plano, CartaoCredito cartao) 
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

        }

        public void CriarPlaylist(string nome) => this.Playlists.Add(new Playlist() { Nome = nome });

        public void FavoritarBanda(Banda banda) => this.BandasFavoritas.Add(banda);

        public List<Playlist> BuscarPlaylist(string nome) => this.Playlists.Where(playlist => playlist.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

        public List<Banda> BuscarBanda(string nome) => this.BandasFavoritas.Where(banda => banda.Nome.ToUpper().Contains(nome.ToUpper())).ToList();

    }
}
