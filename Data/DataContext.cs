using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ApiTcc.Models;
using ApiTcc.Utils;
using ApiTcc.Models;

namespace ApiTcc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Usuario user = new Usuario();
            Usuario user1 = new Usuario();
            
            

            Criptografia.CriarSenhaHash("123456789", out byte[] hash, out byte[] salt);
            user.Id = 1;
            user.Cpf = 500230222322;
            user.Nome = "Agatha";
            user.Email = "Agatha.linhares@gmail.com";
            user.Foto = null;
            user.Senha_hash = hash;
            user.Senha_salt = salt;
            user.Senha = string.Empty;

            modelBuilder.Entity<Usuario>().HasData(user);
           


            Criptografia.CriarSenhaHash("987654321", out byte[] passwordhash, out byte[] passwordsalt);
            user1.Id = 2;
            user1.Cpf = 12345678911;
            user1.Nome = "Matheus";
            user1.Email = "MatheusDias@gmail.com";
            user1.Foto = null;
            user1.Senha_hash = passwordhash;
            user1.Senha_salt = passwordsalt;
            user1.Senha = string.Empty;

            modelBuilder.Entity<Usuario>().HasData(user1);
            
            
            Estabelecimento est1 = new Estabelecimento();
            est1.Id = 1;
            est1.Cnpj = "12123456/0001-12";
            est1.Nome_est = "CutsCuts";
            est1.Local_est = "Av. Ramiz Galvão";
            est1.Numero_est = 1082;
            

            modelBuilder.Entity<Estabelecimento>().HasData(est1);
            
            Estabelecimento est2 = new Estabelecimento();
            est2.Id = 2;
            est2.Cnpj = "31245678/0001-13";
            est2.Nome_est = "ShowHair";
            est2.Local_est = "Rua  Caracaru";
            est2.Numero_est = 102;
            

            modelBuilder.Entity<Estabelecimento>().HasData(est2);
           
            Agendamento ag1 = new Agendamento();
            
            ag1.Id = 1;
            ag1.Nome_clie = "Vinicius";
            ag1.Hora_ag = DateTime.Now;
            ag1.Local_ag = "Av. Ramiz Galvão";
            ag1.EstabelecimentoId = 2;
            ag1.data_ag = DateTime.Today;
            ag1.UsuarioId = 2;

            modelBuilder.Entity<Agendamento>().HasData(ag1);
            
            Agendamento ag2 = new Agendamento();
            ag2.Id = 2;
            ag2.Nome_clie = "Luiz";
            ag2.Hora_ag = DateTime.Now;
            ag2.Local_ag = "Rua Caracaru";
            ag2.EstabelecimentoId = 2;
            ag2.data_ag = DateTime.Today;
            ag2.UsuarioId = 2;


            modelBuilder.Entity<Agendamento>().HasData(ag2);
            
        }
    }
}