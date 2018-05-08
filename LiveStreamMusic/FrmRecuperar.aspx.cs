﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using LiveStreamMusic.Business;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Entities;
using LiveStreamMusic.Web.Funciones;

namespace LiveStreamMusic.Web
{
    public partial class FrmRecuperar : System.Web.UI.Page
    {
        private readonly BusinessUsuarios _servicioUsuario = new BusinessUsuarios();
        private readonly BusinessSeguridad _servicioSeguridad = new BusinessSeguridad();
        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorGeneral.DataBind();
            }
        }
        private void ValidaCampos()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (txtContrasena.Text.Trim() == string.Empty)
                    sb.AppendLine("<li>Contraseña nueva es campo obligatorio</li>");
                if (txtContrasena.Text.Trim() != txtConfirmar.Text.Trim())
                    sb.AppendLine("<li>Las contraseñas no coinciden</li>");
                if (sb.ToString() != string.Empty)
                {
                    sb.Append("</ul>");
                    sb.Insert(0, "<ul>");
                    sb.Insert(0, "<h3>Reestablecer Contraseña</h3>");
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ChangeOption()
        {
            try
            {
                Usuario userData = _servicioUsuario.ObtenerDetalleUsuario(int.Parse(QueryString.Decrypt(Request.Params["ldata"])));
                if (rbtnCorreo.Checked)
                {
                    hfValueNotivicacion.Value = _servicioUsuario.EnviaCodigoVerificacionCorreo(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset);
                    //rbtnList.DataSource = userData.Correo.ToList();
                    //rbtnList.DataTextField = "Correo";
                    //rbtnList.DataValueField = "Id";
                    //rbtnList.DataBind();
                    divCodigoVerificacion.Visible = true;
                }
                //else if (rbtnSms.Checked)
                //{
                //    foreach (TelefonoUsuario telefonoUsuario in userData.TelefonoUsuario.Where(w => w.Obligatorio))
                //    {

                //    }
                //    rbtnList.DataSource = userData.TelefonoUsuario.Where(w => w.Obligatorio).ToList();
                //    rbtnList.DataTextField = "Numero";
                //    rbtnList.DataValueField = "Id";
                //    rbtnList.DataBind();
                //    divCodigoVerificacion.Visible = true;
                //}
                //else if (rbtnPreguntas.Checked)
                //{
                //    rptPreguntas.DataSource = userData.PreguntaReto;
                //    rptPreguntas.DataBind();
                //    divPreguntas.Visible = true;
                //}
                divQuestion.Visible = false;
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
                AlertaGeneral = new List<string>();
                if (!IsPostBack)
                    if (Request.Params["ldata"] != null)
                    {
                        hfEsLink.Value = false.ToString();
                    }
                    else if (Request.Params["confirmacionCodigo"] != null && Request.Params["correo"] != null && Request.Params["code"] != null)
                    {
                        rbtnCorreo.Checked = true;
                        txtCodigo.Text = BusinessQueryString.Decrypt(Request.Params["code"]);
                        hfEsLink.Value = true.ToString();
                        btncontinuar_OnClick(null, null);
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

        protected void btncontinuar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (bool.Parse(hfEsLink.Value))
                {
                    //string[] values = QueryString.Decrypt(Request.Params["confirmacionCodigo"]).Split('_');
                    //if (btncontinuar.CommandArgument == string.Empty)
                    //{
                    //    if (rbtnCorreo.Checked)
                    //    {
                    //        btncontinuar.CommandArgument = "0";
                    //        _servicioUsuario.ValidaCodigoVerificacionCorreo(int.Parse(values[0]), (int)BusinessVariables.EnumTipoLink.Reset, values[1], int.Parse(QueryString.Decrypt(Request.Params["correo"])), txtCodigo.Text.Trim());
                    //        hfParametrosConfirmados.Value = true.ToString();
                    //    }
                    //    divQuestion.Visible = false;
                    //    divCodigoVerificacion.Visible = false;
                    //    divPreguntas.Visible = false;
                    //    divChangePwd.Visible = true;
                    //}
                    //else
                    //{
                    //    _servicioSeguridad.RecuperarCuenta(int.Parse(values[0]), (int)BusinessVariables.EnumTipoLink.Reset, values[1], int.Parse(QueryString.Decrypt(Request.Params["correo"])), txtCodigo.Text.Trim(), txtContrasena.Text, "0");
                    //    Response.Redirect("~/Default.aspx");
                    //}
                }
                else
                {
                    hfEsLink.Value = false.ToString();
                    if (!bool.Parse(hfParametrosConfirmados.Value))
                    {
                        Usuario userData = _servicioUsuario.ObtenerDetalleUsuario(int.Parse(QueryString.Decrypt(Request.Params["ldata"])));
                        string tiporecuperacion = rbtnCorreo.Checked ? "0" : rbtnSms.Checked ? "1" : rbtnPreguntas.Checked ? "2" : "fail";
                        if (rbtnCorreo.Checked)
                        {
                            _servicioUsuario.ValidaCodigoVerificacionCorreo(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset, hfValueNotivicacion.Value, userData.Correo, txtCodigo.Text.Trim());
                            btncontinuar.CommandArgument = "0";
                            hfParametrosConfirmados.Value = true.ToString();
                            divQuestion.Visible = false;
                            divCodigoVerificacion.Visible = false;
                            divPreguntas.Visible = false;
                            divChangePwd.Visible = true;
                        }
                        else if (rbtnSms.Checked)
                        {
                            _servicioUsuario.ValidaCodigoVerificacionSms(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset, int.Parse(rbtnList.SelectedValue), txtCodigo.Text.Trim());
                            btncontinuar.CommandArgument = "1";
                            hfValueNotivicacion.Value = string.Empty;
                            hfParametrosConfirmados.Value = true.ToString();
                            divQuestion.Visible = false;
                            divCodigoVerificacion.Visible = false;
                            divPreguntas.Visible = false;
                            divChangePwd.Visible = true;
                        }
                        else if (rbtnPreguntas.Checked)
                        {
                            Dictionary<int, string> preguntaRespuesta = new Dictionary<int, string>();
                            foreach (RepeaterItem item in rptPreguntas.Items)
                            {
                                preguntaRespuesta.Add(int.Parse(((Label)item.FindControl("lblId")).Text), ((TextBox)item.FindControl("txtRespuesta")).Text);
                            }
                            _servicioUsuario.ValidaRespuestasReto(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), preguntaRespuesta);
                            btncontinuar.CommandArgument = "2";
                            hfValueNotivicacion.Value = string.Empty;
                            hfParametrosConfirmados.Value = true.ToString();
                            divQuestion.Visible = false;
                            divCodigoVerificacion.Visible = false;
                            divPreguntas.Visible = false;
                            divChangePwd.Visible = true;
                        }
                    }
                    else
                    {
                        if (divChangePwd.Visible)
                        {
                            Usuario userData = _servicioUsuario.ObtenerDetalleUsuario(int.Parse(QueryString.Decrypt(Request.Params["ldata"])));
                            ValidaCampos();
                            _servicioSeguridad.ValidaPassword(txtContrasena.Text.Trim());
                            switch (btncontinuar.CommandArgument)
                            {
                                case "0":
                                    _servicioSeguridad.RecuperarCuenta(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset, hfValueNotivicacion.Value, userData.Correo, txtCodigo.Text, txtContrasena.Text.Trim(), "0");
                                    break;
                                case "1":
                                    _servicioSeguridad.RecuperarCuenta(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset, hfValueNotivicacion.Value, rbtnList.SelectedItem.Text, txtCodigo.Text, txtContrasena.Text.Trim(), "1");
                                    break;
                                case "2":
                                    _servicioSeguridad.RecuperarCuenta(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset, hfValueNotivicacion.Value, "-1", txtCodigo.Text, txtContrasena.Text, "2");
                                    break;
                            }
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Request.Params["confirmacionCodigo"] != null && Request.Params["correo"] != null &&
                    Request.Params["code"] != null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rbtnCorreo_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeOption();
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

        protected void rbtnSms_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeOption();
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

        protected void rbtnPreguntas_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeOption();
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

        protected void rbtnList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnCorreo.Checked)
                {
                    hfValueNotivicacion.Value = _servicioUsuario.EnviaCodigoVerificacionCorreo(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset);
                }
                else if (rbtnSms.Checked)
                {
                    _servicioUsuario.EnviaCodigoVerificacionSms(int.Parse(QueryString.Decrypt(Request.Params["ldata"])), (int)BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset, int.Parse(rbtnList.SelectedValue));
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ResolveUrl("~/Default.aspx"));
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