using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using LiveStreamMusic.Business;

namespace LiveStreamMusic.Web
{
    public partial class Publico : System.Web.UI.MasterPage
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
                ucLogin.OnCancelarModal += UcLoginOnOnCancelarModal;
                if (!IsPostBack)
                {
                    if (Request.QueryString["code"] != null && Request.QueryString["code"] != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('hola');", true);
                    }
                    new BusinessSeguridad().GeneraVisita(Request.ServerVariables["REMOTE_ADDR"]);
                }
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

        private void UcLoginOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSingIn\");", true);
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
    }
}