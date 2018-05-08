using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LiveStreamMusic.Business.Catalogos;

namespace LiveStreamMusic.Web.Users
{
    public partial class Musica : Page
    {
        private readonly BusinessCancion _negocioCancion = new BusinessCancion();
        private List<string> _lstError = new List<string>();

        [WebMethod]
        public static string AgregarLista(string name, string address)
        {
            string result = null;
            try
            {
                BusinessCancion negocio = new BusinessCancion();
                result = negocio.ObtenerCancion(1).Descripcion;

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

        private List<string> AlertaGeneral
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
                AlertaGeneral = new List<string>();
                ucAltaIncidencia.OnAceptarModal += ucAltaIncidencia_OnAceptarModal;
                ucAltaIncidencia.OnCancelarModal += UcAltaIncidenciaOnOnCancelarModal;

                ucAgregarCancionLista.OnAceptarModal += UcAgregarCancionListaOnOnAceptarModal;
                ucAgregarCancionLista.OnCancelarModal += UcAgregarCancionListaOnOnCancelarModal;
                if (!IsPostBack)
                {
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

        private void UcAgregarCancionListaOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAgregarCancion\");", true);
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

        private void UcAgregarCancionListaOnOnAceptarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAgregarCancion\");", true);
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

        private void ucAltaIncidencia_OnAceptarModal()
        {
            try
            {
                btnBuscar_OnClick(null, null);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaincidencia\");", true);
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

        private void UcAltaIncidenciaOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaincidencia\");", true);
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

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtFilter.Text.Trim() == string.Empty)
                    throw new Exception("Dime que buscar");
                rptMusica.DataSource = _negocioCancion.ObtenerListaCancionesPalabra(txtFilter.Text.Trim().ToUpper());
                rptMusica.DataBind();

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

        protected void btnReportar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAltaIncidencia.IdCancion = int.Parse(((Button)sender).CommandArgument);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaincidencia\");", true);
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

        protected void btnAddToList_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucAgregarCancionLista.IdCancion = int.Parse(((Button)sender).CommandArgument);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAgregarCancion\");", true);
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