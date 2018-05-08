using System;
using LiveStreamMusic.Business;

namespace LiveStreamMusic.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceUsuarios" en el código y en el archivo de configuración a la vez.
    public class ServiceSeguridad : IServiceSeguridad
    {
        public bool Autenticate(string user, string password)
        {
            try
            {
                using (BusinessSeguridad negocio = new BusinessSeguridad())
                {
                    return negocio.Autenticate(user, password);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
