using System;
using System.Globalization;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Operacion
{
    public class BusinessIncidencia : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessIncidencia(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void CreaiIncidencia(int idTipoIncidencia, int idUsuario, int idCancion, int? idGeneroSugerido, int? idArtistaSugerido, int? idAlbumSugerido, string nombreSugerido)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                Incidencia incidencia = new Incidencia
                {
                    IdTipoIncidencia = idTipoIncidencia,
                    IdUsuario = idUsuario,
                    IdCancion = idCancion,
                    IdGeneroSugerido = idGeneroSugerido,
                    IdArtistaSugerido = idArtistaSugerido,
                    IdAlbumSugerido = idAlbumSugerido,
                    NombreCancionSugerido = nombreSugerido.ToUpper().Trim(),
                    FechaIncidencia = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture)
                };

                db.Incidencia.AddObject(incidencia);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
