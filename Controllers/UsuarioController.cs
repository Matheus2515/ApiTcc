using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTcc.Data;
using ApiTcc.Models;
using ApiTcc.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using ApiTcc.Utils;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ApiTcc.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;

        public UsuarioController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

         private async Task<bool> UsuarioExistente(string email)
        {
            if( await _context.Usuarios.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSingle(int cpf)
        {
            try
            {
                Usuario p = await _context.Usuarios
                    .FirstOrDefaultAsync(pBusca => pBusca.Cpf == cpf);

                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Usuario> lista = await _context.Usuarios.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Usuario novoUsuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(novoUsuario);
                await _context.SaveChangesAsync();

                return Ok(novoUsuario.Cpf);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Usuario novoUsuario)
        {
            try
            {
                _context.Usuarios.Update(novoUsuario);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int cpf)
        {
            try
            {
                Usuario pRemover = await _context.Usuarios
                    .FirstOrDefaultAsync(p => p.Cpf == cpf);
                _context.Usuarios.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<ActionResult> RegistrarUsuario(Usuario user)
        {
            try
            {
                if (await UsuarioExistente(user.Email))
                    throw new System.Exception("Email já cadastrado");

                Criptografia.CriarSenhaHash(user.Senha, out byte[] hash, out byte[] salt);
                
                user.Senha = string.Empty;
                user.Senha_hash = hash;
                user.Senha_salt = salt;
                await _context.Usuarios.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Cpf);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                Usuario usuario = await _context.Usuarios
                   .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(credenciais.Email.ToLower()));

                if (usuario == null)
                {
                    throw new System.Exception("Usuário não encontrado.");
                }
                else if (!Criptografia.VerificarSenhaHash(credenciais.Senha, usuario.Senha_hash, usuario.Senha_salt))
                {
                    throw new System.Exception("Senha incorreta.");
                }
                else
                {
                    usuario.Senha_hash = null;
                    usuario.Senha_salt = null;
                    usuario.Token = CriarToken(usuario);
                    return Ok(usuario);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CriarToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Cpf.ToString()),  new Claim(ClaimTypes.Name, usuario.Email)
            };
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8 .GetBytes(_configuration.GetSection("ConfiguracaoToken:Chave").Value)); 
                SigningCredentials creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);  
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();  SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);
        } 
        
    }
}