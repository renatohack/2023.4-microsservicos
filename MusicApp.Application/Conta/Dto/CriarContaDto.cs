﻿using MusicApp.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Application.Conta.Dto
{
    public class CriarContaDto
    {

        public String Nome { get; set; }

        public Guid Id { get; set; }

        public Guid PlanoId { get; set; }

        public CartaoCreditoDto CartaoCredito { get; set; }


        public class CartaoCreditoDto
        {
            public string Numero { get; set; }
            public bool CartaoAtivo { get; set; }
            public decimal LimiteDisponivel { get; set; }

        }
    }
}