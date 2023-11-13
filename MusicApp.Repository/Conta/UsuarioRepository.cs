using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Repository.Conta
{
    public class UsuarioRepository
    {

        private static List<Usuario> usuarios = new List<Usuario>();


        public void SalvarUsuarioNaBase(Usuario usuario)
        {
            UsuarioRepository.usuarios.Add(usuario);
        }

        public int RetornarNumeroUsuarioNaBase() => usuarios.Count;

    }
}
