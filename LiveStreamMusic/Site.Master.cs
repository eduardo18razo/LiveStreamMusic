using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using LiveStreamMusic.Business;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web
{
    public partial class SiteMaster : MasterPage
    {
        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlert.Visible = value.Any();
                if (!panelAlert.Visible) return;
                rptHeaderError.DataSource = value;
                rptHeaderError.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //if (!Request.Browser.IsMobileDevice)
                    //    Response.Redirect("~/Default.aspx");
                }
                HttpCookie myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (myCookie == null || Session["UserData"] == null)
                {
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                }

                if (!IsPostBack && Session["UserData"] != null)
                {
                    Usuario usuario = ((Usuario)Session["UserData"]);
                    lblUsuario.Text = usuario.NombreCompleto + "<br>" + usuario.Correo;
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }


        protected void btnsOut_OnClick(object sender, EventArgs e)
        {

            try
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }
    }
}