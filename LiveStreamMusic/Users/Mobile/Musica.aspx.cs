using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LiveStreamMusic.Business.Catalogos;

namespace LiveStreamMusic.Web.Users.Mobile
{
    public partial class Musica : Page
    {
        private readonly BusinessCancion _negocioCancion = new BusinessCancion();
        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlerta.Visible = value.Any();
                if (!panelAlerta.Visible) return;
                rptErrorGeneral.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorGeneral.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                ucAltaIncidencia.OnCancelarModal += UcAltaIncidenciaOnOnCancelarModal;
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
    }
}