using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using LiveStreamMusic.Business.Catalogos;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.UserControls.Altas
{
    public partial class UcAltaSolicitudCancion : UserControl, IControllerModal
    {
        private readonly BusinessAlbum _nAlbum = new BusinessAlbum();
        private readonly BusinessArtista _nArtista = new BusinessArtista();
        private readonly BusinessCancion _nCancion = new BusinessCancion();
        private readonly BusinessGeneros _nGeneros = new BusinessGeneros();
        private List<string> _lstError = new List<string>();

        private List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert",
                        "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                Alerta = _lstError;
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                _nCancion.GeneraSolicitudCancion(((Usuario) Session["UserData"]).Id, "", txtArtista.Text, txtAlbum.Text,
                    txtCancion.Text, txtComentarios.Text);
                Response.Redirect("~/Users/Musica.aspx");
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
                Response.Redirect("~/Users/Musica.aspx");
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