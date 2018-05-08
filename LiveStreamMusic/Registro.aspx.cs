using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveStreamMusic.Web
{
    public partial class Registro : System.Web.UI.Page
    {
        private List<string> _lstError = new List<string>();

        private List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblBrandingModal.Text = WebConfigurationManager.AppSettings["Brand"];
                Alerta = new List<string>();
                ucAltacuenta.OnAceptarModal += ucAltaUsuarioRapida_OnAceptarModal;
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnRegistrar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltacuenta.ValidaUsuarioExiste();
                ucAltacuenta.RegistraUsuario();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        void ucAltaUsuarioRapida_OnAceptarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptOpen", "MostrarPopup(\"#modalRegistroExito\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnCerrarExito_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}