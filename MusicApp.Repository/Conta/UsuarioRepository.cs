using MusicApp.Domain.Aplicativo.Aggregates;
using MusicApp.Domain.Conta.Aggregates;
using MusicApp.Repository.Aplicativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Repository.Conta
{
    public class UsuarioRepository
    {

        private static List<Usuario> _usuarios = new List<Usuario>();


        public void SalvarUsuarioNaBase(Usuario usuario)
        {

<<<<<<< HEAD
            Usuario usuarioBanco = this.ObterUsuarioPorId(usuario.Id);

            if (usuarioBanco == null)
=======
            usuario = this.ObterUsuarioPorId(usuario.Id);

            if (usuario == null)
>>>>>>> becf74eacd17de26db36cdc59e9bd9f1fbf45304
            {
                UsuarioRepository._usuarios.Add(usuario);
            }
            else
            {
                int indexToUpdate = UsuarioRepository._usuarios.IndexOf(usuario);
                UsuarioRepository._usuarios[indexToUpdate] = usuario;
            }

        }


        public Usuario ObterUsuarioPorId(Guid idUsuario) => UsuarioRepository._usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);
    }
}
