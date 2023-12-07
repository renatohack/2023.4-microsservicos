﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Usuario.Application.Dto
{
    public class AdicionarCartaoDtoRequest
    {
        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public CartaoDto Cartao { get; set; }
    }


}