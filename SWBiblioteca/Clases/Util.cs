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

        internal static string EmailBody(string content)
        {
            var body = "<!DOCTYPE html>"
                + "<html lang=\'en\' xmlns=\'http://www.w3.org/1999/xhtml\' xmlns:o=\'urn:schemas-microsoft-com:office:office\'>"
                + "<head>"
                    + "<meta charset='UTF-8'>"
                    + "<meta name='viewport' content='width=device-width,initial-scale=1'>"
                    + "<meta name='x-apple-disable-message-reformatting'>"
                    + "<style>"
                        + "table, td, div, h1, p {font-family: Arial, sans-serif;}"
                    + "</style>"
                + "</head>"
                + "<body style='margin:0;padding:0;'>"
                    + "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>"
                        + "<tr>"
                            + "<td align='center' style='padding:0;'>"
                                + "<table role='presentation' style='width:602px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'>"
                                    + "<tr>"
                                        + "<td align='center' style='padding:40px 0 30px 0;background:#70bbd9;'>"
                                            + "<img src='https://assets.codepen.io/210284/h1.png' alt='' width='300' style='height:auto;display:block;' />"
                                        + "</td>"
                                    + "</tr>"
                                    + "<tr>"
                                        + "<td style='padding:36px 30px 42px 30px;'>"
                                            + "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>"
                                                + "<tr>"
                                                    + "<td style='padding:0 0 36px 0;color:#153643;'>"
                                                        + $"<h3 style='font-size:20px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>{content}</h1>"
                                                    + "</td>"
                                                + "</tr>"
                                            + "</table>"
                                        + "</td>"
                                    + "</tr>"
                                    + "<tr>"
                                        + "<td style='padding:30px;background:#ee4c50;'>"
                                            + "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>"
                                                + "<tr>"
                                                    + "<td style='padding:0;width:50%;' align='left'>"
                                                        + "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>&copy; <a href='https://alexandergomez.netlify.app/' style='color:#ffffff;text-decoration:underline;'>Alexander Gomez 2023</a></p>"
                                                    + "</td>"
                                                + "</tr>"
                                            + "</table>"
                                        + "</td>"
                                     + "</tr>"
                                + "</table>"
                            + "</td>"
                        + "</tr>"
                    + "</table>"
                + "</body>"
            + "</html>";

            return body;
        }
    }
}
