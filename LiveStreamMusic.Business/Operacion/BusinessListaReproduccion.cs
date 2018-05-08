using System;
using System.Collections.Generic;
using System.Linq;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Operacion
{
    public class BusinessListaReproduccion : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessListaReproduccion(bool proxy = false)
        {
            _proxy = proxy;
        }

        public List<ListaReproduccion> ObtenerListaReproduccionUsuario(int idUsuario, bool insertarSeleccion)
        {
            List<ListaReproduccion> result = null;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ListaReproduccion.Where(w => w.IdUsuario == idUsuario).OrderBy(o => o.Descripcion).ToList();
                foreach (ListaReproduccion lista in result)
                {
                    db.LoadProperty(lista, "ListaReproduccionDetalle");
                    foreach (ListaReproduccionDetalle detalle in lista.ListaReproduccionDetalle)
                    {
                        db.LoadProperty(detalle, "Cancion");
                    }
                }
                if (insertarSeleccion)
                    result.Insert(0,
                        new ListaReproduccion
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

        public ListaReproduccion ObtenerListaReproduccionById(int idLista)
        {
            ListaReproduccion result = null;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ListaReproduccion.SingleOrDefault(w => w.Id == idLista);
                db.LoadProperty(result, "ListaReproduccionDetalle");
                foreach (ListaReproduccionDetalle detalle in result.ListaReproduccionDetalle)
                {
                    db.LoadProperty(detalle, "Cancion");
                }
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

        public void AgregarCancionLista(int idListaReproduccion, int idCancion)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                ListaReproduccionDetalle detalle = new ListaReproduccionDetalle
                {
                    IdListaReproduccion = idListaReproduccion,
                    IdCancion = idCancion
                };
                db.ListaReproduccionDetalle.AddObject(detalle);
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

        public void CrearLista(string descripcion, int idUsuario)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ListaReproduccion.AddObject(new ListaReproduccion { IdUsuario = idUsuario, Descripcion = descripcion.ToUpper().Trim(), Habilitada = true });
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
