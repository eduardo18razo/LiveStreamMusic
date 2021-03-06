﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución: 12.0.0.0
//  
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Security.Cryptography;
using System.Text;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Data
{
    public partial class ModelDataContext : ObjectContext
    {
        private readonly EntityConnection _connection;
        private readonly string _connectionString;
        private const string ConnectionString = "name=AudioFilesConn";
        private const string ContainerName = "AudioFilesConn";

        public ModelDataContext()
            : base(ConnectionString, ContainerName)
        {
            try
            {
                _album = CreateObjectSet<Album>();
                _artista = CreateObjectSet<Artista>();
                _cancion = CreateObjectSet<Cancion>();
                _genero = CreateObjectSet<Genero>();
                _parametroCorreo = CreateObjectSet<ParametroCorreo>();

                _tipoCorreo = CreateObjectSet<TipoCorreo>();
                _tipoLink = CreateObjectSet<TipoLink>();
                _usuario = CreateObjectSet<Usuario>();
                _usuarioLinkPassword = CreateObjectSet<UsuarioLinkPassword>();
                _incidencia = CreateObjectSet<Incidencia>();
                _tipoIncidencia = CreateObjectSet<TipoIncidencia>();
                _sugerencia = CreateObjectSet<Sugerencia>();
                _visita = CreateObjectSet<Visita>();
                _bitacoraAcceso = CreateObjectSet<BitacoraAcceso>();
                _listaReproduccion = CreateObjectSet<ListaReproduccion>();
                _listaReproduccionDetalle = CreateObjectSet<ListaReproduccionDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public ModelDataContext(string connectionString)
            : base(ConnectionString, ContainerName)
        {
            _connectionString = connectionString;
        }

        public ModelDataContext(EntityConnection connection)
            : base(ConnectionString, ContainerName)
        {
            _connection = connection;
        }

        public static string DecryptedConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["GastosConnection"].ToString()))
                    throw new Exception("No se encuentra cadena de conexion");

                var toD = Convert.FromBase64String(ConfigurationManager.ConnectionStrings["GastosConnection"].ToString());

                var denc = ProtectedData.Unprotect(toD, null, DataProtectionScope.LocalMachine);
                string cadena = Encoding.ASCII.GetString(denc).Replace("&quot;", "\"");
                //System.Xml.Linq.XDocument x1 = System.Xml.Linq.XDocument.Parse(x);
                return cadena;
            }
        }

        public ObjectSet<Album> Album
        {
            get
            {
                return _album;
            }
        }

        public ObjectSet<Artista> Artista
        {
            get
            {
                return _artista;
            }
        }

        public ObjectSet<Cancion> Cancion
        {
            get
            {
                return _cancion;
            }
        }

        public ObjectSet<Genero> Genero
        {
            get
            {
                return _genero;
            }
        }

        public ObjectSet<ParametroCorreo> ParametroCorreo
        {
            get
            {
                return _parametroCorreo;
            }
        }

        public ObjectSet<TipoCorreo> TipoCorreo
        {
            get
            {
                return _tipoCorreo;
            }
        }

        public ObjectSet<TipoLink> TipoLink
        {
            get
            {
                return _tipoLink;
            }
        }

        public ObjectSet<Usuario> Usuario
        {
            get
            {
                return _usuario;
            }
        }

        public ObjectSet<UsuarioLinkPassword> UsuarioLinkPassword
        {
            get
            {
                return _usuarioLinkPassword;
            }
        }
        public ObjectSet<TipoIncidencia> TipoIncidencia
        {
            get
            {
                return _tipoIncidencia;
            }
        }
        public ObjectSet<Incidencia> Incidencia
        {
            get
            {
                return _incidencia;
            }
        }
        public ObjectSet<Sugerencia> Sugerencia
        {
            get
            {
                return _sugerencia;
            }
        }

        public ObjectSet<Visita> Visita
        {
            get { return _visita; }
        }
        public ObjectSet<BitacoraAcceso> BitacoraAcceso
        {
            get { return _bitacoraAcceso; }
        }

        public ObjectSet<ListaReproduccion> ListaReproduccion
        {
            get { return _listaReproduccion; }
        }
        public ObjectSet<ListaReproduccionDetalle> ListaReproduccionDetalle
        {
            get { return _listaReproduccionDetalle; }
        }

        private readonly ObjectSet<Album> _album;
        private readonly ObjectSet<Artista> _artista;
        private readonly ObjectSet<Cancion> _cancion;
        private readonly ObjectSet<Genero> _genero;
        private readonly ObjectSet<ParametroCorreo> _parametroCorreo;

        private readonly ObjectSet<TipoCorreo> _tipoCorreo;
        private readonly ObjectSet<TipoLink> _tipoLink;
        private readonly ObjectSet<Usuario> _usuario;
        private readonly ObjectSet<UsuarioLinkPassword> _usuarioLinkPassword;

        private readonly ObjectSet<TipoIncidencia> _tipoIncidencia;
        private readonly ObjectSet<Incidencia> _incidencia;
        private readonly ObjectSet<Sugerencia> _sugerencia;
        private readonly ObjectSet<Visita> _visita;
        private readonly ObjectSet<BitacoraAcceso> _bitacoraAcceso;
        private readonly ObjectSet<ListaReproduccion> _listaReproduccion;
        private readonly ObjectSet<ListaReproduccionDetalle> _listaReproduccionDetalle;
    }
}
