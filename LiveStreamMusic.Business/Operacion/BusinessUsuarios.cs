

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using System.Linq;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Operacion
{
    public class BusinessUsuarios : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessUsuarios(bool proxy = false)
        {
            _proxy = proxy;
        }
        public Usuario BuscarUsuario(string usuario)
        {
            Usuario result = null;
            ModelDataContext db = new ModelDataContext();
            try
            {
                int idUsuario;
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                if (db.Usuario.Any(w => w.Correo == usuario))
                {
                    idUsuario = db.Usuario.First(w => w.Correo == usuario).Id;
                    result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                }
                //else if (db.TelefonoUsuario.Any(w => w.Numero == usuario && w.Obligatorio))
                //{
                //    idUsuario = db.TelefonoUsuario.First(w => w.Numero == usuario).IdUsuario;
                //    result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                //}
                //else if (db.Usuario.Any(w => w.NombreUsuario == usuario))
                //{
                //    idUsuario = db.Usuario.First(w => w.NombreUsuario == usuario).Id;
                //    result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
                //}
                //if (result != null)
                //{

                //    db.LoadProperty(result, "PreguntaReto");
                //    db.LoadProperty(result, "TelefonoUsuario");
                //    db.LoadProperty(result, "CorreoUsuario");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public bool ValidaCorreo(string correo)
        {
            bool result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = !db.Usuario.Any(s => s.Correo == correo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public void CrearCuenta(string nombre, string apellido, string correo, string pwd)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                if (!ValidaCorreo(correo))
                    throw new Exception("Correo ya se se encuentra en uso");
                if (db.Usuario.Any(a => a.Correo == correo.Trim().ToLower()))
                    throw new Exception("Ya se registro este correo");
                Usuario usr = new Usuario();
                usr.Nombre = nombre.ToUpper();
                usr.Apellido = apellido.ToUpper();
                usr.Correo = correo.ToLower().Trim();
                usr.Pwd = Utils.SecurityUtils.CreateShaHash(pwd);
                usr.PwdDes = pwd;
                usr.Sexo = true;
                usr.Activo = false;
                usr.FechaNacimiento = DateTime.Now;
                usr.FechaAlta = DateTime.Now;
                usr.Confirmado = false;
                usr.Habilitado = true;
                Guid g = Guid.NewGuid();
                usr.UsuarioLinkPassword = new List<UsuarioLinkPassword>
                {
                    new UsuarioLinkPassword
                    {
                        Activo = true,
                        Link = g,
                        Fecha = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                        IdTipoLink = (int) BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Confirmacion
                    }
                };
                db.Usuario.AddObject(usr);
                db.SaveChanges();
                BusinessFile.CreaRepositorio(usr.Correo);
                EnviaCorreo(usr.Nombre, usr.Correo, g.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public int ConfirmaCorreo(string guid)
        {
            int result = 0;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                Guid g = new Guid(guid);
                UsuarioLinkPassword usr = db.UsuarioLinkPassword.SingleOrDefault(s => s.Link == g && s.Activo);
                if (usr != null)
                {
                    usr.Usuario.Activo = true;
                    usr.Activo = false;
                    result = usr.Id;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        private void EnviaCorreo(string nombre, string correo, string link)
        {
            try
            {
                string url = string.Format("{0}/{1}?id={2}", ConfigurationManager.AppSettings["siteUrl"], "Confirmacion.aspx", link);
                String body = string.Format("Hola {0}<br><br>Gracias por registrarte en Kiininet<br><br>Tu nombre de usuario es: {1}<br><br>Para completar su registro, <a href='{2}'>haz clic aquí para verificar tu dirección de correo electrónico </a><br><br>Una vez que hayas sido verificado, podrás iniciar sesión en tu cuenta de Kiininet.<br><br>Si tienes preguntas, comunícate con nuestro equipo de atención al cliente.<br><br>El equipo de Kiininet",
                nombre, correo, link);
                BusinessCorreo.SendMail(correo, "Confirma tu registro", body);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario ObtenerDetalleUsuario(int idUsuario)
        {
            Usuario result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Usuario.SingleOrDefault(s => s.Id == idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public List<Usuario> BuscarUsuarios(string usuario)
        {
            List<Usuario> result = null;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Usuario.Where(w => w.Correo == usuario).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public void TerminaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, string link, string idCorreo, string codigo)
        {
            //ModelDataContext db = new ModelDataContext();
            //try
            //{
            //    CorreoUsuario telefono = db.CorreoUsuario.Single(s => s.Id == idCorreo);
            //    Guid guidLink = Guid.Parse(link);
            //    List<UsuarioLinkPassword> links = db.UsuarioLinkPassword.Where(a => a.IdUsuario == idUsuario && a.Activo).ToList();
            //    foreach (UsuarioLinkPassword linkValue in links)
            //    {
            //        linkValue.Activo = false;
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
        }

        public string EnviaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion)
        {
            ModelDataContext db = new ModelDataContext();
            string result = null;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Random generator = new Random();
                String codigo = generator.Next(0, 99999).ToString("D5");
                Guid g = Guid.NewGuid();
                ParametroCorreo correo = db.ParametroCorreo.SingleOrDefault(s => s.IdTipoCorreo == (int)BusinessVariables.EnumeradoresStreaming.EnumTipoCorreo.RecuperarCuenta && s.Habilitado);
                if (correo != null)
                {
                    db.LoadProperty(correo, "TipoCorreo");
                    Usuario usuario = db.Usuario.Single(u => u.Id == idUsuario);
                    String body = string.Format(correo.Contenido, usuario.NombreCompleto, ConfigurationManager.AppSettings["siteUrl"] + "/FrmRecuperar.aspx?confirmacionCodigo=" + BusinessQueryString.Encrypt(idUsuario + "_" + g) + "&correo=" + BusinessQueryString.Encrypt(usuario.Correo) + "&code=" + BusinessQueryString.Encrypt(codigo), codigo);
                    BusinessCorreo.SendMail(usuario.Correo, correo.TipoCorreo.Descripcion, body);
                    usuario.UsuarioLinkPassword = new List<UsuarioLinkPassword>
                    {
                        new UsuarioLinkPassword
                        {
                            Activo = true,
                            Link = g,
                            Fecha = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                            IdTipoLink = (int) BusinessVariables.EnumeradoresStreaming.EnumTipoLink.Reset,
                            Codigo = codigo
                        }
                    };
                    db.SaveChanges();
                    result = g.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public void EnviaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono)
        {
            try
            {
                //Random generator = new Random();
                //String codigo = generator.Next(0, 99999).ToString("D5");
                //switch (idTipoNotificacion)
                //{
                //    case (int)BusinessVariables.EnumTipoLink.Confirmacion:
                //        new BusinessDemonioSms().InsertarMensaje(idUsuario, idTipoNotificacion, idTelefono, codigo);
                //        break;
                //    case (int)BusinessVariables.EnumTipoLink.Reset:
                //        new BusinessDemonioSms().InsertarMensaje(idUsuario, idTipoNotificacion, idTelefono, codigo);
                //        break;
                //}

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ValidaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono, string codigo)
        {
            string result = string.Empty;
            //DataBaseModelContext db = new DataBaseModelContext();
            //try
            //{
            //    db.ContextOptions.ProxyCreationEnabled = _proxy;
            //    TelefonoUsuario telefono = db.TelefonoUsuario.Single(s => s.Id == idTelefono);
            //    if (!db.SmsService.Any(a => a.IdUsuario == idUsuario && a.IdTipoLink == idTipoNotificacion && a.Numero == telefono.Numero && a.Mensaje == codigo && a.Enviado && a.Habilitado))
            //    {
            //        throw new Exception(string.Format("Codigo incorrecto para Numero Telefonico {0}\n", telefono.Numero));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
            return result;
        }

        public void ValidaCodigoVerificacionCorreo(int idUsuario, int idTipoNotificacion, string link, string idCorreo, string codigo)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Guid guidLink = Guid.Parse(link);
                if (!db.UsuarioLinkPassword.Any(a => a.IdUsuario == idUsuario && a.IdTipoLink == idTipoNotificacion && a.Link == guidLink && a.Codigo == codigo && a.Activo))
                {
                    throw new Exception(string.Format("Codigo incorrecto {0}\n", idCorreo));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public string TerminaCodigoVerificacionSms(int idUsuario, int idTipoNotificacion, int idTelefono, string codigo)
        {
            string result = string.Empty;
            ModelDataContext db = new ModelDataContext();
            try
            {
                //TelefonoUsuario telefono = db.TelefonoUsuario.Single(s => s.Id == idTelefono);
                //List<SmsService> sms = db.SmsService.Where(a => a.IdUsuario == idUsuario && a.Habilitado).ToList();
                //foreach (SmsService mensaje in sms)
                //{
                //    mensaje.Habilitado = false;
                //    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public bool ValidaConfirmacion(int idUsuario, string guid)
        {
            bool result = false;
            //DataBaseModelContext db = new DataBaseModelContext();
            //try
            //{
            //    db.ContextOptions.ProxyCreationEnabled = _proxy;
            //    Guid guidParam = Guid.Parse(guid);
            //    result =
            //        db.UsuarioLinkPassword.Any(s => s.IdUsuario == idUsuario && s.Link == guidParam && s.IdTipoLink == (int)BusinessVariables.EnumTipoLink.Confirmacion && s.Activo);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
            return result;
        }

        public void ValidaRespuestasReto(int idUsuario, Dictionary<int, string> preguntasReto)
        {
            //DataBaseModelContext db = new DataBaseModelContext();
            //try
            //{
            //    foreach (KeyValuePair<int, string> pregunta in preguntasReto)
            //    {
            //        string respuesta = SecurityUtils.CreateShaHash(pregunta.Value);
            //        if (!db.PreguntaReto.Any(w => w.IdUsuario == idUsuario && w.Id == pregunta.Key && w.Respuesta == respuesta))
            //            throw new Exception("Verifique respuestas");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    db.Dispose();
            //}
        }
    }
}
