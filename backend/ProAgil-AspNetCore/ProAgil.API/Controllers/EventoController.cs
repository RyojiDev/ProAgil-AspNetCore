using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repository;
        public EventoController(Repository.IProAgilRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includePalestrantes)
        {
            try
            {
                var results = await _repository.GetAllEventoAsync(includePalestrantes);
                return  Ok(results);
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "Erro ao retornar os dados, tente novamente");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int eventoId, bool includePalestrantes)
        {
            try
            {
                var results = await _repository.GetEventosyncById(eventoId, includePalestrantes);
                return Ok(results);

            }catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "Erro ao retornar os dados, tente novamente");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema, bool includePalestrantes)
        {
            try
            {
                var results = await _repository.GetAllEventoAsyncByTema(tema, includePalestrantes);
                return Ok(results);

            }catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "Erro ao retornar os dados, tente novamente");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                 _repository.Add(model);

                 if(await _repository.SaveChangesAsync())
                 {
                     return Created($"/api/evento/{model.Id}", model);
                 }
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = await _repository.GetEventosyncById(eventoId, false);
                 _repository.Update(evento);
                 if(evento == null) return NotFound();

                 if(await _repository.SaveChangesAsync())
                 {
                     return Created($"/api/evento/{evento.Id}", evento);
                 }
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Delete(int eventoId, Evento model)
        {
            try
            {
                var evento = await _repository.GetEventosyncById(eventoId, false);
                 _repository.Delete(evento);
                 if(evento == null) return NotFound();

                 if(await _repository.SaveChangesAsync())
                 {
                     return NoContent();
                 }
            }
            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados");
            }

            return BadRequest();
        }

    }
}