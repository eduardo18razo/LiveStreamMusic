using System;
using System.Collections.Generic;
using System.Linq;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Catalogos
{
    public class BusinessGeneros: IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessGeneros(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void CrearGenero(string descripcion)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.Genero.AddObject(new Genero { Descripcion = descripcion.ToUpper().Trim() });
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

        public List<Genero> ObtenerGeneros(bool insertarSeleccion)
        {
            List<Genero> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Genero.OrderBy(o => o.Descripcion).ToList(); ;
                if (insertarSeleccion)
                    result.Insert(0,
                        new Genero
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
