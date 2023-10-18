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

        public async Task<PERSONA> SaveUsuario(PERSONA model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
