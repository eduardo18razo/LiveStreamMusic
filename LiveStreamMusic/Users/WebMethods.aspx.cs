using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.Users
{
    public partial class WebMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<CancionHelper> ObtenerDetalleLista(int id)
        {
            BusinessListaReproduccion negocio = new BusinessListaReproduccion();
            return negocio.ObtenerListaReproduccionById(id).ListaReproduccionDetalle.Select(s => new CancionHelper { Id = s.IdCancion, Descripcion = s.Cancion.Descripcion }).ToList();
        }

        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public static List<Cancion> ObtenerCancionesDeLista()
        {
            List<Cancion> result = null;
            try
            {
                BusinessListaReproduccion negocio = new BusinessListaReproduccion();
                result = negocio.ObtenerListaReproduccionById(3).ListaReproduccionDetalle.Select(s => s.Cancion).ToList();

            }
            catch (Exception ex)
            {
                //if (_lstError == null)
                //{
                //    _lstError = new List<string>();
                //}
                //_lstError.Add(ex.Message);
                //AlertaGeneral = _lstError;
            }
            return result;
        }
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public static bool CorreoValido(string correo)
        {
            bool result = false;
            try
            {
                BusinessUsuarios busines = new BusinessUsuarios();
                result = busines.ValidaCorreo(correo);
            }
            catch (Exception ex)
            {
                //if (_lstError == null)
                //{
                //    _lstError = new List<string>();
                //}
                //_lstError.Add(ex.Message);
                //AlertaGeneral = _lstError;
            }
            return result;
        }
    }
}