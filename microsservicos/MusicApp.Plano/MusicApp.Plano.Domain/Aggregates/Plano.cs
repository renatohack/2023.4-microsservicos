﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Plano.Domain.Aggregates
{
    public class Plano
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }


        public Plano()
        {
            Id = Guid.NewGuid();
        }
    }
}
