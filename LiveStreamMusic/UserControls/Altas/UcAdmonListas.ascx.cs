using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Entities;
using LiveStreamMusic.Web.Funciones;

namespace LiveStreamMusic.Web.UserControls.Altas
{
    public partial class UcAdmonListas : UserControl
    {
        private readonly BusinessListaReproduccion _nListas = new BusinessListaReproduccion();
        private List<string> _lstError = new List<string>();

        private List<string> Alerta
        {
            set
            {
                panelAlerta.Visible = value.Any();
                if (!panelAlerta.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }
        private void ObtenerListas()
        {
            try
            {
                Metodos.LimpiarRepeater(rptContenidoLista);
                Metodos.LlenaComboCatalogo(ddlListas, _nListas.ObtenerListaReproduccionUsuario(((Usuario)Session["UserData"]).Id, true));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void UcAltaListaReproduccionOnOnAceptarModal()
        {
            try
            {
                ObtenerListas();
                ObtenerListas();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaLista\");", true);
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

        private void UcAltaListaReproduccionOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaLista\");", true);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
                ucAltaListaReproduccion.OnAceptarModal += UcAltaListaReproduccionOnOnAceptarModal;
                ucAltaListaReproduccion.OnCancelarModal += UcAltaListaReproduccionOnOnCancelarModal;
                if (!IsPostBack)
                {
                    ObtenerListas();
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

        protected void ddlListas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarRepeater(rptContenidoLista);
                if (ddlListas.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    return;
                }
                rptContenidoLista.DataSource = _nListas.ObtenerListaReproduccionById(int.Parse(ddlListas.SelectedValue)).ListaReproduccionDetalle;
                rptContenidoLista.DataBind();
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

        protected void btnAgregarLista_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAltaLista\");", true);
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