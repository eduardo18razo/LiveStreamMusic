using System;
using System.Linq;
using LiveStreamMusic.Business.Operacion;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business
{
    public class BusinessSeguridad : IDisposable
    {
        private readonly bool _proxy;
        public BusinessSeguridad(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        {

        }

        public void GeneraVisita(string ip)
        {
            if (ip.Trim() == string.Empty)
            {
                return;
            }
            ModelDataContext db = new ModelDataContext();
            try
            {
                Visita visita = db.Visita.SingleOrDefault(s => s.Ip == ip);
                if (visita == null)
                {
                    visita = new Visita { Ip = ip, Veces = 1 };
                    db.Visita.AddObject(visita);
                }
                else
                {
                    visita.Veces += 1;
                }
                db.SaveChanges();
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

        public void DesbloqueaUsuarios()
        {
            //ModelDataContext db = new ModelDataContext();
            //try
            //{
            //    DateTime? fecha = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
            //    List<Usuario> usuariosBloqueados = db.Usuario.Where(w => w.FechaBloqueo != null && w.FechaBloqueo <= fecha).ToList();
            //    foreach (Usuario usuario in usuariosBloqueados)
            //    {
            //        usuario.FechaBloqueo = null;
            //        usuario.Tries = 0;
            //    }
            //    db.SaveChanges();
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
        public bool Autenticate(string user, string password)
        {
            DesbloqueaUsuarios();
            bool result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                string hashedPdw = Utils.SecurityUtils.CreateShaHash(password);
                //&& w.FechaBloqueo == null && w.Habilitado && w.Activo
                result = db.Usuario.Any(w => w.Correo == user && w.Pwd == hashedPdw);
                //if (db.ParametrosGenerales.First().StrongPassword)
                //    if (db.ParametroPassword.First().Fail)
                //    {
                //        Usuario usuario = db.Usuario.Single(s => s.NombreUsuario == user);
                //        if (!result && usuario != null)
                //        {
                //            if (usuario.FechaBloqueo == null)
                //            {
                //                if (db.ParametroPassword.First().Fail)
                //                {
                //                    usuario.Tries++;
                //                    if (usuario.Tries >= db.ParametroPassword.First().Tries)
                //                        usuario.FechaBloqueo = DateTime.ParseExact(DateTime.Now.AddMinutes(db.ParametroPassword.First().TimeoutFail).ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                //                }
                //            }
                //            else
                //                throw new Exception(string.Format("Usuario bloqueado espere {0} minutos", db.ParametroPassword.First().TimeoutFail));
                //        }
                //        else if (usuario != null)
                //        {
                //            usuario.Tries = 0;
                //            usuario.FechaBloqueo = null;
                //        }
                //        db.SaveChanges();
                //    }
                //if (db.Usuario.Any(s => s.NombreUsuario == user))
                //    if (db.Usuario.Single(s => s.NombreUsuario == user).FechaBloqueo != null)
                //        throw new Exception(string.Format("Usuario bloqueado espere {0} minutos", db.ParametroPassword.First().TimeoutFail));
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

        public Usuario GetUserDataAutenticate(string user, string password, string ip)
        {
            Usuario result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                string hashedPdw = Utils.SecurityUtils.CreateShaHash(password);
                if (db.Usuario.Count(w => w.Correo == user && w.Pwd == hashedPdw && w.Habilitado && w.Activo) > 1)
                    throw new Exception("Error al obtener informacion consulte a su Administrador");
                result = db.Usuario.SingleOrDefault(w => w.Correo == user && w.Pwd == hashedPdw);
                if (result != null)
                {
                    BitacoraAcceso nuevoAcceso = new BitacoraAcceso
                    {
                        IdUsuario = result.Id,
                        Fecha = DateTime.Now,
                        Ip = ip
                    };

                    db.BitacoraAcceso.AddObject(nuevoAcceso);
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

        public Usuario GetUserDataAutenticateId(int idUsuario, string ip)
        {
            Usuario result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Usuario.SingleOrDefault(w => w.Id == idUsuario && w.Activo && w.Habilitado);
                if (result != null)
                {
                    BitacoraAcceso nuevoAcceso = new BitacoraAcceso
                    {
                        IdUsuario = result.Id,
                        Fecha = DateTime.Now,
                        Ip = ip
                    };

                    db.BitacoraAcceso.AddObject(nuevoAcceso);
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

        public void RecuperarCuenta(int idUsuario, int idTipoNotificacion, string link, string correo, string codigo, string contrasena, string tipoRecuperacion)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                switch (int.Parse(tipoRecuperacion))
                {
                    case 0:
                        new BusinessUsuarios().ValidaCodigoVerificacionCorreo(idUsuario, idTipoNotificacion, link, correo, codigo);
                        new BusinessUsuarios().TerminaCodigoVerificacionCorreo(idUsuario, idTipoNotificacion, link, correo, codigo);
                        break;
                    case 1:
                        new BusinessUsuarios().ValidaCodigoVerificacionSms(idUsuario, idTipoNotificacion, 0, codigo);
                        new BusinessUsuarios().TerminaCodigoVerificacionSms(idUsuario, idTipoNotificacion, 0, codigo);
                        break;
                }

                string hashedPdw = Utils.SecurityUtils.CreateShaHash(contrasena);
                Usuario user = db.Usuario.SingleOrDefault(w => w.Id == idUsuario && w.Habilitado);
                if (user != null)
                {
                    //ParametrosGenerales parametrosG = db.ParametrosGenerales.First();
                    //if (parametrosG.StrongPassword)
                    //{
                    //    if (db.ParametroPassword.First().CaducaPassword)
                    //        user.FechaUpdate = DateTime.ParseExact(DateTime.Now.AddDays(db.ParametroPassword.First().TiempoCaducidad).ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture);
                    //    if (db.UsuarioPassword.Any(a => a.IdUsuario == idUsuario && a.Password == hashedPdw))
                    //        throw new Exception("Contraseña antigua intente con una diferente");
                    //}
                    user.Pwd = hashedPdw;
                    user.PwdDes = contrasena;
                    //user.UsuarioPassword = new List<UsuarioPassword>
                    //    {
                    //        new UsuarioPassword
                    //        {
                    //            Fecha = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture),
                    //            Password = SecurityUtils.CreateShaHash(contrasena)
                    //        }
                    //    };
                    db.SaveChanges();
                }
                //LimpiaPasswordsAntiguos(idUsuario);
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
        public void ValidaPassword(string pwd)
        {
            //DataBaseModelContext db = new DataBaseModelContext();
            //try
            //{
            //    if (db.ParametrosGenerales.First().StrongPassword)
            //    {
            //        ParametroPassword parametros = db.ParametroPassword.FirstOrDefault();
            //        if (parametros != null)
            //        {
            //            if (!(pwd.Length >= parametros.Min && pwd.Length <= parametros.Max))
            //                throw new Exception(string.Format("El password debe contener entre {0} y {1} caracteres", parametros.Min, parametros.Max));
            //            if (parametros.Letras)
            //                if (!(Regex.Matches(pwd, @"[a-zA-Z]").Count > 0))
            //                    throw new Exception(string.Format("El password debe contener caracteres alfanumericos"));

            //            if (parametros.Numeros)
            //                if (!(Regex.Matches(pwd, @"[0-9]").Count > 0))
            //                    throw new Exception(string.Format("El password debe contener caracteres numericos"));

            //            if (parametros.Especiales)
            //                if (!(Regex.Matches(pwd, "[^a-z0-9]", RegexOptions.IgnoreCase).Count > 0))
            //                    throw new Exception(string.Format("El password debe contener caracteres especiales"));

            //            if (parametros.Mayusculas > 0)
            //                if (!(Regex.Matches(pwd, "[A-Z]").Count >= parametros.Mayusculas))
            //                    throw new Exception(string.Format("El password debe contener {0} mayusculas", parametros.Mayusculas));

            //        }
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
