using System;
using System.Collections.Generic;
using LiveStreamMusic.Business;
using LiveStreamMusic.Business.Catalogos;

namespace LiveStreamMusic.Services
{
    public class ServiceMusica : IServiceMusica
    {
        public List<CancionesHelper> ObtenerListaCancionesPalabra(string descripcion)
        {
            try
            {
                using (BusinessCancion negocio = new BusinessCancion())
                {
                    return negocio.ObtenerListaCancionesPalabra(descripcion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
