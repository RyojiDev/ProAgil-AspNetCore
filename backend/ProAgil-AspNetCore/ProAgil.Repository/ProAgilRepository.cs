using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public class ProAgilRepository : IProAgilRepository
  {
      private readonly ProAgilContext _context;

      public ProAgilRepository(ProAgilContext context)
      {
          _context = context;
      }

    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

     public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

     public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
    }

    //EVENTO

    public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
    {
      IQueryable<Evento> query =  _context.Eventos
      .Include(c => c.Lotes)
      .Include(c => c.RedesSociais);

      if(includePalestrantes)
      {
          query = query
          .Include(pe => pe.palestrantesEventos)
          .ThenInclude(p => p.Palestrante);
      }

      query = query.OrderByDescending(c => c.DataEvento);

      return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
    {
      IQueryable<Evento> query =  _context.Eventos
      .Include(c => c.Lotes)
      .Include(c => c.RedesSociais);

      if(includePalestrantes)
      {
          query = query
          .Include(pe => pe.palestrantesEventos)
          .ThenInclude(p => p.Palestrante);
      }

      query = query.OrderBy(c => c.Id)
      .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetEventosyncById(int EventoId, bool includePalestrantes = false)
    {
      IQueryable<Evento> query =  _context.Eventos
      .Include(c => c.Lotes)
      .Include(c => c.RedesSociais);

      if(includePalestrantes)
      {
          query = query
          .Include(pe => pe.palestrantesEventos)
          .ThenInclude(p => p.Palestrante);
      }

      query = query.OrderBy(c => c.Id)
      .Where(e => e.Id == EventoId);

      return await query.FirstOrDefaultAsync();
    }

    //Palestrantes

    public async Task<Palestrante> GetPalestrantesAsync(int palestranteId, bool includeEventos = false)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
      .Include(c => c.RedesSociais);

      if(includeEventos)
      {
          query = query
          .Include(pe=> pe.palestrantesEventos)
          .ThenInclude(e => e.Evento);
      }

      query = query.OrderBy( p => p.Nome)
      .Where(p => p.Id == palestranteId);

      return await query.FirstOrDefaultAsync();
    }

     public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false)
    {
      IQueryable<Palestrante> query =  _context.Palestrantes
      .Include(c => c.RedesSociais);

      if(includeEventos)
      {
          query = query
          .Include(pe => pe.palestrantesEventos)
          .ThenInclude(e => e.Evento);
      }

      query = query.Where( p=> p.Nome.ToLower().Contains(name.ToLower()));

      return await query.ToArrayAsync();
    }
  }
}