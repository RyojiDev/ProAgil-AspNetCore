using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Model;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
            return new Evento[] {
                new Evento(){
              EventoId = 1,
              Tema = "Angular e suas novidades",
              DataEvento = DateTime.Now.ToString(),
              Local = "Fortaleza",
              Lote =  "1º Lote",
              QtdPessoas = 250    
            } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id)
        {
            return new Evento[]{
              new Evento(){
              EventoId = 1,
              Tema = "Angular e suas novidades",
              DataEvento = DateTime.Now.ToString(),
              Local = "Fortaleza",
              Lote =  "1º Lote",
              QtdPessoas = 250    
            },
              new Evento(){
              EventoId = 2,
              Tema = "React e suas novidades",
              DataEvento = DateTime.Now.ToString(),
              Local = "Fortaleza",
              Lote =  "2º Lote",
              QtdPessoas = 500    
            }
            }.FirstOrDefault(p => p.EventoId == id);
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
