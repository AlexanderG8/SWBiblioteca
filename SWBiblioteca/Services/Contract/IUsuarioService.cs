using SWBiblioteca.Models;

namespace SWBiblioteca.Services.Contract
{
    public interface IUsuarioService
    {
        Task<PERSONA> GetUsuario(string correo, string clave);
        Task<PERSONA> SaveUsuario(PERSONA model);
    }
}
