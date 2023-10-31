using Microsoft.EntityFrameworkCore;
using SWBiblioteca.Data;
using SWBiblioteca.Models;
using SWBiblioteca.Services.Contract;

namespace SWBiblioteca.Services.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<PERSONA> GetUsuario(string correo, string clave)
        {
            PERSONA usuario = await _context.PERSONA.Where(z => z.Correo == correo && z.Clave == clave).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<LOG_PERSONA> EstadoUsuario(string codigo)
        {
            LOG_PERSONA loG_PERSONA = await _context.LOG_PERSONA.Where(z => z.Cod_Usuario == codigo && z.Estado == "Creado").FirstOrDefaultAsync();
            return loG_PERSONA;
        }

        public async Task<PERSONA> SaveUsuario(PERSONA model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            var log = new LOG_PERSONA();
            log.Cod_Usuario = model.Codigo;
            log.Estado = "Creado";
            log.FechaRegistro = DateTime.Now.Date;
            log.HoraRegistro = DateTime.Now.TimeOfDay;
            _context.Add(log);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<List<CONSULTAR_PERFIL_Q01>> ConsultaValidador(string perfil)
        {
            var mensaje = await _context.CONSULTAR_PERFIL_Q01.FromSqlRaw("CONSULTAR_PERFIL_Q01 @p0", perfil).ToListAsync();
            return mensaje;
        }
    }
}
