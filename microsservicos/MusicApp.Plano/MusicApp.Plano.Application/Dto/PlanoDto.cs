﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Plano.Application.DTO
{
    public class PlanoDto
    {
        public Guid IdPlano { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
