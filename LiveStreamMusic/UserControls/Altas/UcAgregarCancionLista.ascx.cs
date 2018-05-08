using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using LiveStreamMusic.Business.Catalogos;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.UserControls.Altas
{
    public partial class UcAgregarCancionLista : UserControl, IControllerModal
    {
        private readonly BusinessListaReproduccion _nLista = new BusinessListaReproduccion();
        private List<string> _lstError = new List<string>();

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

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        public int IdCancion
        {
            get { return int.Parse(hfIdCacnion.Value); }
            set { hfIdCacnion.Value = value.ToString(); }
        }

        private void LLenaListas()
        {
            try
            {
                ddlListas.DataSource = _nLista.ObtenerListaReproduccionUsuario(((Usuario)Session["UserData"]).Id, true);
                ddlListas.DataTextField = "Descripcion";
                ddlListas.DataValueField = "Id";
                ddlListas.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                ucAltaListaReproduccion.OnAceptarModal += UcAltaListaReproduccionOnOnAceptarModal;
                ucAltaListaReproduccion.OnCancelarModal += UcAltaListaReproduccionOnOnCancelarModal;

                if (!IsPostBack)
                {
                    LLenaListas();
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

        private void UcAltaListaReproduccionOnOnAceptarModal()
        {
            try
            {
                LLenaListas();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAltaLista\");", true);
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
                AlertaGeneral = _lstError;
            }
        }

        protected void btnCrearLista_OnClick(object sender, EventArgs e)
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
                AlertaGeneral = _lstError;
            }
        }

        protected void btnAgregarCancion_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlListas.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception("Selecciona una lista para agregar la canción");
                _nLista.AgregarCancionLista(int.Parse(ddlListas.SelectedValue), IdCancion);
                Cancion song = new BusinessCancion().ObtenerCancion(IdCancion);
                CancionHelper newSong = new CancionHelper { Id = song.Id, Descripcion = song.Descripcion };
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptAddSong", "AddNewSongtoList(" + ddlListas.SelectedValue + "," + new JavaScriptSerializer().Serialize(newSong) + " );", true);
                ddlListas.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                if (OnAceptarModal != null)
                    OnAceptarModal();
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ddlListas.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                if (OnCancelarModal != null)
                    OnCancelarModal();
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