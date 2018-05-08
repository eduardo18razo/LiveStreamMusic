using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using LiveStreamMusic.Business.Catalogos;

namespace LiveStreamMusic.Web.UserControls.Altas
{
    public partial class UcAltaArtista : UserControl, IControllerModal
    {
        private readonly BusinessArtista _nArtista = new BusinessArtista();
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

        public int IdGenero
        {
            get { return int.Parse(hfIdGenero.Value); }
            set { hfIdGenero.Value = value.ToString(); }
        }

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
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

        protected void btnGuardarArtista_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtArtista.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese Una descripcion");
                _nArtista.CrearArtista(IdGenero, txtArtista.Text);
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
                Alerta = _lstError;
            }
        }


        protected void btnCancelarArtista_OnClick(object sender, EventArgs e)
        {
            try
            {
                txtArtista.Text = string.Empty;
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