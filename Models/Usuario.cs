using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTcc.Models.Enuns;
using System.ComponentModel.DataAnnotations.Schema;
using ApiTcc.Models;
using System.Text.Json.Serialization;


namespace ApiTcc.Models
{
    public class Usuario
    {
        public long Id {get; set;}
        
        public long Cpf { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public byte[]? Foto { get; set; }

        [NotMapped]
        public string Senha { get; set; }

        public TipoClasseUsuario TipoUsuario { get; set; }

        public byte[]? Senha_hash { get; set; }

        public byte[]? Senha_salt { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [JsonIgnore]
        public Agendamento Agendamento { get; set; }

        
    }

}