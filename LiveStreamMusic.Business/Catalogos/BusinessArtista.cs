using System;
using System.Collections.Generic;
using System.Linq;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Catalogos
{
    public class BusinessArtista : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessArtista(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void CrearArtista(int idGenero, string descripcion)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.Artista.AddObject(new Artista { IdGenero = idGenero, Descripcion = descripcion.ToUpper().Trim(), Habilitado = true });
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
        public List<Artista> ObtenerTodos(bool insertarSeleccion)
        {
            List<Artista> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Artista.OrderBy(o => o.Descripcion).ToList(); ;
                if (insertarSeleccion)
                    result.Insert(0,
                        new Artista
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
        public List<Artista> ObtenerArtistaGenero(int idGenero, bool insertarSeleccion)
        {
            List<Artista> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Artista.Where(w => w.IdGenero == idGenero).OrderBy(o => o.Descripcion).ToList(); ;
                if (insertarSeleccion)
                    result.Insert(0,
                        new Artista
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
