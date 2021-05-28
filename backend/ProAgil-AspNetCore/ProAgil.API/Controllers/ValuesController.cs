using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
      private readonly ProAgilContext _context;
      public ValuesController(ProAgilContext context)
      {
          _context = context;
      }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Get()
        {
            var result = await _context.Eventos.ToListAsync();
            try
            {
              return Ok(result);
            }
            catch(System.Exception){
              return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao encontrar");
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id)
        {
            return _context.Eventos.FirstOrDefault(p => p.Id == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
