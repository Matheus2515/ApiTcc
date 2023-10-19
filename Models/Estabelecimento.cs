using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiTcc.Models
{
    public class Estabelecimento
    {
        public long Id {get; set;}

        public string Cnpj { get; set; }

        public string Nome_est { get; set; }

        public string Local_est { get; set; }

        public int Numero_est { get; set; }

        [JsonIgnore]
        public Agendamento Agendamento { get; set; }
    }
}