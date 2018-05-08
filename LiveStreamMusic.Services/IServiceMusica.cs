using System.Collections.Generic;
using System.ServiceModel;
using LiveStreamMusic.Business;

namespace LiveStreamMusic.Services
{
    [ServiceContract]
    public interface IServiceMusica
    {
        [OperationContract]
        List<CancionesHelper> ObtenerListaCancionesPalabra(string descripcion);
    }
}
