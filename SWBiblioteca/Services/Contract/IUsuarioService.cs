using SWBiblioteca.Models;

namespace SWBiblioteca.Services.Contract
{
    public interface IUsuarioService
    {
        Task<PERSONA> GetUsuario(string correo, string clave);
        Task<LOG_PERSONA> EstadoUsuario(string codigo);
        Task<PERSONA> SaveUsuario(PERSONA model);
        Task<PERSONA> EditUsuario(PERSONA model);
        Task<List<CONSULTAR_PERFIL_Q01>> ConsultaValidador(string perfil);
        Task<string> ValidarUserExistente(string correo, string codigo);
        Task<PERSONA> ValidarCorreo(string correo);
    }
}
