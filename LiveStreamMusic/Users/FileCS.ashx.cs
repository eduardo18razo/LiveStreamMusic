using System;
using System.IO;
using System.Web;
using System.Web.Security;
using LiveStreamMusic.Business.Catalogos;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.Users
{
    /// <summary>
    /// Descripción breve de FileCS
    /// </summary>
    public class FileCS : IHttpHandler
    {
        private readonly BusinessCancion _negociCancion = new BusinessCancion();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //if (context.Session == null || context.Request.Cookies.Count > 2 || context.Session["UserData"] == null)
                //    context.Response.Redirect("~/Default.aspx");
                int id = int.Parse(context.Request.QueryString["id"]);
                Cancion archivo = _negociCancion.ObtenerCancion(id);
                if (archivo != null)
                {
                    var file = new FileInfo(archivo.FilePath);
                    context.Response.Clear();
                    context.Response.AddHeader("content-disposition", string.Format("attachment; filename=\"{0}\"", archivo.Descripcion + archivo.Extension));
                    context.Response.ContentType = archivo.ContentType;
                    context.Response.WriteFile(file.FullName, false);
                }
            }
            catch (Exception)
            {
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}