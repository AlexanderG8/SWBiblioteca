namespace SWBiblioteca.Clases
{
    public class Util
    {
        internal static async Task<List<string>> UploadDocument(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IFormFile dOC_ADJUN, string ruta)
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = guid + Path.GetExtension(dOC_ADJUN.FileName);
            var carga = Path.Combine(hostingEnvironment.WebRootPath, string.Format("documents\\{0}", ruta));
            using (var fileStream = new FileStream(Path.Combine(carga, fileName), FileMode.Create))
            {
                await dOC_ADJUN.CopyToAsync(fileStream);
            }
            var lista = new List<string>();
            lista.Add(dOC_ADJUN.FileName);
            lista.Add(fileName);
            return lista;
        }
    }
}
