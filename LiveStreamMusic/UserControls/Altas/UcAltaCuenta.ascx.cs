using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Business.Utils;

namespace LiveStreamMusic.Web.UserControls.Altas
{
    public partial class UcAltaCuenta : UserControl, IControllerModal
    {

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        private readonly BusinessUsuarios _negocioUsuario = new BusinessUsuarios();
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

        private void ValidaCaptura()
        {
            if (txtNombre.Text.Trim() == string.Empty)
                throw new Exception("Nombre es campo obligatorio.");
            if (txtApellido.Text.Trim() == string.Empty)
                throw new Exception("Apellido es campo obligatorio.");

            if (txtCorreo.Text.Trim() == string.Empty)
                throw new Exception("Correo es campo obligatorio.");
            if (!BusinessCorreo.IsValid(txtCorreo.Text.Trim()) || txtCorreo.Text.Trim().Contains(" "))
            {
                throw new Exception(string.Format("Correo {0} con formato invalido", txtCorreo.Text.Trim()));
            }
            if (txtConfimacionCorreo.Text.Trim() == string.Empty)
                throw new Exception("Debes confirmar tu correo.");
            if (!BusinessCorreo.IsValid(txtConfimacionCorreo.Text.Trim()) || txtConfimacionCorreo.Text.Trim().Contains(" "))
            {
                throw new Exception(string.Format("Correo {0} con formato invalido", txtCorreo.Text.Trim()));
            }
            if (txtCorreo.Text.Trim() != txtConfimacionCorreo.Text.Trim())
                throw new Exception("Los correos no coinciden.");
            if (txtPassword.Text.Trim() == string.Empty)
                throw new Exception("Contraseña es campo obligatorio.");
            if (txtConfirmaPassword.Text.Trim() == string.Empty)
                throw new Exception("Debes Confirmar tu contraseña.");
            if (txtPassword.Text.Trim() != txtConfirmaPassword.Text.Trim())
                throw new Exception("Las contraseñas no coinciden.");

        }

        private void Limpiar()
        {
            try
            {
                txtNombre.Text = string.Empty;
                txtApellido.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtCorreo.Text = string.Empty;
                txtConfimacionCorreo.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtConfirmaPassword.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ValidaUsuarioExiste()
        {
            try
            {
                ValidaCaptura();
                if (_negocioUsuario.BuscarUsuario(txtCorreo.Text.Trim()) != null)
                {
                    throw new Exception("Ya existe una cuenta con estos datos");
                }
                //if (!string.IsNullOrEmpty(txtTelefonoCelularRapido.Text.Trim()))
                //    if (_negocioUsuario.BuscarUsuario(txtTelefonoCelularRapido.Text.Trim()) != null)
                //    {
                //        throw new Exception("Ya existe una cuenta con estos datos");
                //    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public void RegistraUsuario()
        {
            try
            {
                ValidaCaptura();
                GuardarUsuario();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

        public void GuardarUsuario()
        {
            ValidaCaptura();
            _negocioUsuario.CrearCuenta(txtNombre.Text, txtApellido.Text, txtCorreo.Text, txtPassword.Text);

            Limpiar();

            if (OnAceptarModal != null)
                OnAceptarModal();
        }

        protected void btnFace_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("https://www.facebook.com/v2.4/dialog/oauth/?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&display=popup&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/Users/Dashboard.aspx&response_type=code&state=1");
        }

    }
}