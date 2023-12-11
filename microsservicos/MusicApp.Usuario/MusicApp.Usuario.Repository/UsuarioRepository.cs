using domain = MusicApp.Usuario.Domain.Aggregates;
using MusicApp.Usuario.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Repository
{
    public class UsuarioRepository
    {

        private static List<domain.Usuario> _usuarios = new List<domain.Usuario>();


        public void SalvarUsuarioNaBase(domain.Usuario usuario)
        {

            domain.Usuario usuarioBanco = this.ObterUsuarioPorId(usuario.Id);

            if (usuarioBanco == null)
            {
                UsuarioRepository._usuarios.Add(usuario);
            }
            else
            {
                int indexToUpdate = UsuarioRepository._usuarios.FindIndex(u => u.Id.Equals(usuario.Id));
                UsuarioRepository._usuarios[indexToUpdate] = usuario;
            }

        }


        public domain.Usuario ObterUsuarioPorId(Guid idUsuario) => UsuarioRepository._usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);
    }
}
