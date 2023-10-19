using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using ApiTcc.Models;


namespace ApiTcc.Models
{
    public class Agendamento
    {
        public long Id { get; set; }

        public DateTime Hora_ag { get; set; }

        public string Local_ag { get; set; }

        public DateTime data_ag { get; set; }

        public string Nome_clie { get; set; }

        public Estabelecimento Estabelecimento { get; set; }
        
        public int EstabelecimentoId {get; set;}

        public Usuario Usuario { get; set; }
        
        public int UsuarioId {get; set;}
        
    }
}