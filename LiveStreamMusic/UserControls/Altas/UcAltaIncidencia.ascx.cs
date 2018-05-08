using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using LiveStreamMusic.Business.Catalogos;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Entities;
using LiveStreamMusic.Web.Funciones;

namespace LiveStreamMusic.Web.UserControls.Altas
{
    public partial class UcAltaIncidencia : UserControl, IControllerModal
    {
        private readonly BusinessAlbum _nAlbum = new BusinessAlbum();
        private readonly BusinessArtista _nArtista = new BusinessArtista();
        private readonly BusinessGeneros _nGeneros = new BusinessGeneros();
        private readonly BusinessIncidencia _nIncidencia = new BusinessIncidencia();
        private readonly BusinessTipoIncidencia _nTipoIncidencia = new BusinessTipoIncidencia();
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

        public int IdCancion
        {
            get { return int.Parse(hfIdCancion.Value); }
            set { hfIdCancion.Value = value.ToString(); }
        }

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private void LlenaCombos()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlTipoIncidencia, _nTipoIncidencia.ObtenerTodos(true));
                Metodos.LlenaComboCatalogo(ddlGeneroSugerido, _nGeneros.ObtenerGeneros(true));
                Metodos.LlenaComboCatalogo(ddlArtistaSugerido, _nArtista.ObtenerTodos(true));
                Metodos.LlenaComboCatalogo(ddlAlbumSugerido, _nAlbum.ObtenerTodos(true));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void Limpiar()
        {
            try
            {
                ddlTipoIncidencia.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                ddlTipoIncidencia_OnSelectedIndexChanged(ddlTipoIncidencia, null);
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
                Alerta = new List<string>();
                if (!IsPostBack)
                {
                    LlenaCombos();
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

        protected void ddlTipoIncidencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlGeneroSugerido.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                ddlArtistaSugerido.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                ddlAlbumSugerido.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;

                divGeneroIncorrecto.Visible = false;
                divArtistaIncorrecto.Visible = false;
                divAlbumIncorrecto.Visible = false;
                divNombre.Visible = false;
                if (ddlTipoIncidencia.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    return;
                switch (int.Parse(ddlTipoIncidencia.SelectedValue))
                {
                    case (int)BusinessVariables.EnumeradoresStreaming.EnumTipoIncidencia.GeneroIncorrecto:
                        divGeneroIncorrecto.Visible = true;
                        break;
                    case (int)BusinessVariables.EnumeradoresStreaming.EnumTipoIncidencia.ArtistaIncorrecto:
                        divArtistaIncorrecto.Visible = true;

                        break;
                    case (int)BusinessVariables.EnumeradoresStreaming.EnumTipoIncidencia.AlbumIncorrecto:
                        divAlbumIncorrecto.Visible = true;
                        break;
                    case (int)BusinessVariables.EnumeradoresStreaming.EnumTipoIncidencia.CancionConNombreIncorrecto:
                        divNombre.Visible = true;
                        break;
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int idTipoIncidencia = int.Parse(ddlTipoIncidencia.SelectedValue);
                int idUsuario = ((Usuario)Session["UserData"]).Id;

                int? idGeneroSugerido = ddlGeneroSugerido.SelectedIndex !=
                                        BusinessVariables.ComboBoxCatalogo.IndexSeleccione
                    ? int.Parse(ddlGeneroSugerido.SelectedValue)
                    : (int?)null;
                int? idArtistaSugerido = ddlArtistaSugerido.SelectedIndex !=
                                         BusinessVariables.ComboBoxCatalogo.IndexSeleccione
                    ? int.Parse(ddlArtistaSugerido.SelectedValue)
                    : (int?)null;
                int? idAlbumSugerido = ddlAlbumSugerido.SelectedIndex !=
                                       BusinessVariables.ComboBoxCatalogo.IndexSeleccione
                    ? int.Parse(ddlAlbumSugerido.SelectedValue)
                    : (int?)null;

                _nIncidencia.CreaiIncidencia(idTipoIncidencia, idUsuario, IdCancion, idGeneroSugerido, idArtistaSugerido, idAlbumSugerido, txtNombreSugerido.Text);
                if (OnAceptarModal != null)
                    OnAceptarModal();
                //<asp:TextBox runat="server" ID="txtNombre" placeholder="nombre" CssClass="form-control obligatorio" onkeydown="return (event.keyCode!=13);"  MaxLength="50" />
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
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
                Alerta = _lstError;
            }
        }
    }
}