using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventoController : ControllerBase
  {
    private readonly IProAgilRepository _repository;
    public IMapper _mapper { get; }
    public EventoController(Repository.IProAgilRepository repository, IMapper mapper)
    {
      _mapper = mapper;
      _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(bool includePalestrantes)
    {
      try
      {
        var eventos = await _repository.GetAllEventoAsync(includePalestrantes);
        var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);
        return Ok(results);
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status404NotFound, "Erro ao retornar os dados, tente novamente");
      }
    }

    [HttpGet("{EventoId}")]
    public async Task<IActionResult> Get(int eventoId, bool includePalestrantes = true)
    {
      try
      {
        var evento = await _repository.GetEventosyncById(eventoId, includePalestrantes);

        var results = _mapper.Map<EventoDto>(evento);
        return Ok(results);

      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status404NotFound, "Erro ao retornar os dados, tente novamente");
      }
    }

    [HttpGet("getByTema/{tema}")]
    public async Task<IActionResult> Get(string tema, bool includePalestrantes)
    {
      try
      {
        var eventos = await _repository.GetAllEventoAsyncByTema(tema, includePalestrantes);
        var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);
        return Ok(results);

      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status404NotFound, "Erro ao retornar os dados, tente novamente");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventoDto model)
    {
      try
      {
        var evento = _mapper.Map<Evento>(model);
        _repository.Add(evento);

        if (await _repository.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento));
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados");
      }

      return BadRequest();
    }

    [HttpPut("{EventoId}")]
    public async Task<IActionResult> Put(int EventoId, EventoDto model)
    {
      try
      {
        var evento = await _repository.GetEventosyncById(EventoId, false);
        if (evento == null) return NotFound();

        _mapper.Map(model, evento);

        _repository.Update(evento);

        
        if (await _repository.SaveChangesAsync())
        {
          return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento));
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados");
      }

      return BadRequest();
    }

    [HttpDelete("{EventoId}")]
    public async Task<IActionResult> Delete(int eventoId)
    {
      try
      {
        var evento = await _repository.GetEventosyncById(eventoId, false);
        _repository.Delete(evento);
        if (evento == null) return NotFound();

        if (await _repository.SaveChangesAsync())
        {
          return NoContent();
        }
      }
      catch (System.Exception)
      {
        return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar os dados");
      }

      return BadRequest();
    }

  }
}