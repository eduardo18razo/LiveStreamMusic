using System;
using System.Collections.Generic;
using System.Linq;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Catalogos
{
    public class BusinessTipoIncidencia : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessTipoIncidencia(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<TipoIncidencia> ObtenerTodos(bool insertarSeleccion)
        {
            List<TipoIncidencia> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.TipoIncidencia.OrderBy(o => o.Descripcion).ToList(); ;
                if (insertarSeleccion)
                    result.Insert(0,
                        new TipoIncidencia
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
