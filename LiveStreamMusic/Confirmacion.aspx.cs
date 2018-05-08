using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using LiveStreamMusic.Business;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web
{
    public partial class Confirmacion : Page
    {
        private readonly BusinessSeguridad _negocioSeguridad = new BusinessSeguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                    Response.Redirect("~/Default.aspx");
                else
                {
                    Usuario user = _negocioSeguridad.GetUserDataAutenticateId(new BusinessUsuarios().ConfirmaCorreo(Request.Params["id"]), Request.ServerVariables["REMOTE_ADDR"]);
                    Session["UserData"] = user;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Correo, DateTime.Now, DateTime.Now.AddMinutes(1000), true, Session["UserData"].ToString(), FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    Response.Redirect("~/Users/Dashboard.aspx");
                }
            }
            catch
            {
            }
        }
    }
}