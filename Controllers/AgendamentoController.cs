using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTcc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTcc.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;

        public AgendamentoController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


    }
}