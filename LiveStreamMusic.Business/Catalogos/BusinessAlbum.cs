using System;
using System.Collections.Generic;
using System.Linq;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Catalogos
{
    public class BusinessAlbum: IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessAlbum(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void CrearAlbum(int idArtista, string descripcion)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.Album.AddObject(new Album { IdArtista = idArtista, Descripcion = descripcion.ToUpper().Trim() });
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
        public List<Album> ObtenerTodos(bool insertarSeleccion)
        {
            List<Album> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Album.OrderBy(o => o.Descripcion).ToList(); ;
                if (insertarSeleccion)
                    result.Insert(0,
                        new Album
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public List<Album> ObtenerAlbumArtista(int idGenero, int idArtista, bool insertarSeleccion)
        {
            List<Album> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Album.Where(w => w.IdArtista == idArtista && w.Artista.Genero.Id == idGenero).OrderBy(o => o.Descripcion).ToList(); ;
                if (insertarSeleccion)
                    result.Insert(0,
                        new Album
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
    }
}
