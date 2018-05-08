using System.ServiceModel;

namespace LiveStreamMusic.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServiceUsuarios" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceSeguridad
    {
        [OperationContract]
        bool Autenticate(string user, string password);
    }
}
