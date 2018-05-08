using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using LiveStreamMusic.Business;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Web.UserControls
{
    public partial class UcLogin : UserControl
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        private bool _validCaptcha = false;
        private readonly BusinessSeguridad _negocioSeguridad = new BusinessSeguridad();
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
            StringBuilder sb = new StringBuilder();

            if (txtUsuario.Text.Trim() == string.Empty)
                sb.AppendLine("Usuario es un campo obligatorio.");
            if (txtpwd.Text.Trim() == string.Empty)
                sb.AppendLine("Contraseña es un campo obligatorio.");
            if (sb.ToString() != string.Empty)
            {
                sb.Insert(0, "<h3>Acceso</h3>");
                throw new Exception(sb.ToString());
            }
        }

        private void LimpiarPantalla()
        {
            try
            {
                Alerta = new List<string>();
                txtUsuario.Text = string.Empty;
                txtpwd.Text = string.Empty;
                //txtCaptcha.Text = string.Empty;
                txtUsuario.Focus();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AutenticaUsuario(string usuario, string psw, string ip)
        {
            try
            {
                if (!_negocioSeguridad.Autenticate(usuario.Trim(), psw.Trim()))
                    throw new Exception("Usuario y/o contraseña no validos");
                Usuario user = _negocioSeguridad.GetUserDataAutenticate(usuario.Trim().ToLower(), psw.Trim(), ip);
                Session["UserData"] = user;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Correo, DateTime.Now, DateTime.Now.AddMinutes(15), true, Session["UserData"].ToString(), FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                LimpiarPantalla();
                if (OnCancelarModal != null)
                    OnCancelarModal();
                //if (Request.Browser.IsMobileDevice)
                //    Response.Redirect("~/Users/Mobile/Musica.aspx");
                //else
                Response.Redirect("~/Users/Dashboard.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaCaptura();
                if (!_validCaptcha)
                {
                    //txtCaptcha.Text = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    throw new Exception(sb.ToString());
                }
                AutenticaUsuario(txtUsuario.Text.Trim(), txtpwd.Text.Trim(), Request.ServerVariables["REMOTE_ADDR"]);
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

        protected void lnkBtnRecuperar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Identificar.aspx");
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

        protected void btnCacelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarPantalla();
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
        protected void OnServerValidate(object source, ServerValidateEventArgs e)
        {
            try
            {
                if (txtUsuario.Text.Trim() == string.Empty || txtpwd.Text.Trim() == string.Empty || txtCaptcha.Text.Trim() == string.Empty) return;
                Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                e.IsValid = Captcha1.UserValidated;
                _validCaptcha = e.IsValid;
                if (!e.IsValid)
                {
                    List<string> sb = new List<string>();
                    sb.Add("Captcha incorrecto.");
                    if (sb.Count > 0)
                    {
                        _lstError = sb;
                        throw new Exception("");
                    }
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                    _lstError.Add(ex.Message);
                }

                Alerta = _lstError;
            }
        }
    }
}