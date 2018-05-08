using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiveStreamMusic.Business.Utils;
using LiveStreamMusic.Data;
using LiveStreamMusic.Entities;

namespace LiveStreamMusic.Business.Catalogos
{
    public class BusinessCancion : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessCancion(bool proxy = false)
        {
            _proxy = proxy;
        }


        public void CrearCancion(int idAlbum, string nombreArchivo)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                Cancion newSong = new Cancion();
                FileInfo fileInfo = new FileInfo(BusinessVariables.Directorios.RepositorioTemporal + nombreArchivo);
                BusinessFile.MoverTemporales(BusinessVariables.Directorios.RepositorioTemporal, BusinessVariables.Directorios.Repositorio, nombreArchivo);
                switch (fileInfo.Extension.ToLower())
                {
                    case ".mp3":
                        newSong = new Cancion
                        {
                            IdAlbum = idAlbum,
                            Descripcion = nombreArchivo,
                            Extension = fileInfo.Extension,
                            ContentType = "audio/mpeg3",
                            FilePath = BusinessVariables.Directorios.Repositorio + nombreArchivo,
                            Data = null
                        };
                        break;
                    case ".m4a":
                        newSong = new Cancion
                        {
                            IdAlbum = idAlbum,
                            Descripcion = nombreArchivo,
                            Extension = fileInfo.Extension,
                            ContentType = "audio/mpeg4",
                            FilePath = BusinessVariables.Directorios.Repositorio + nombreArchivo,
                            Data = null
                        };
                        break;
                    case ".m4b":
                        newSong = new Cancion
                        {
                            IdAlbum = idAlbum,
                            Descripcion = nombreArchivo,
                            Extension = fileInfo.Extension,
                            ContentType = "audio/mpeg4",
                            FilePath = BusinessVariables.Directorios.Repositorio + nombreArchivo,
                            Data = null
                        };
                        break;
                }
                db.Cancion.AddObject(newSong);
                db.SaveChanges();
                BusinessFile.LimpiarTemporales(nombreArchivo);
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

        public List<Cancion> ObtenerCancionesAlbum(int idAlbum)
        {
            List<Cancion> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Cancion.Where(w => w.IdAlbum == idAlbum).OrderBy(o => o.Descripcion).ToList();
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

        public List<CancionesHelper> ObtenerListaCanciones(int? idGenero, int? idArtista, int? idAlbum)
        {
            List<CancionesHelper> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                var qry = from cancion in db.Cancion
                          join album in db.Album on cancion.IdAlbum equals album.Id
                          join artista in db.Artista on album.IdArtista equals artista.Id
                          join genero in db.Genero on artista.IdGenero equals genero.Id
                          select new { genero, artista, album, cancion };

                if (idGenero != null)
                    qry = from q in qry
                          where q.genero.Id == idGenero
                          select q;
                if (idArtista != null)
                    qry = from q in qry
                          where q.artista.Id == idArtista
                          select q;
                if (idAlbum != null)
                    qry = from q in qry
                          where q.album.Id == idAlbum
                          select q;
                result = qry.ToList().Select(s => new CancionesHelper
                {
                    IdGenero = s.genero.Id,
                    Genero = s.genero.Descripcion,
                    IdArtista = s.artista.Id,
                    Artista = s.artista.Descripcion,
                    IdAlbum = s.album.Id,
                    Album = s.album.Descripcion,
                    IdCancion = s.cancion.Id,
                    Cancion = s.cancion.Descripcion,
                    TieneReporte = s.cancion.TieneReporte
                }).OrderBy(o => o.Cancion).ToList();
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

        public List<CancionesHelper> ObtenerListaCancionesPalabra(string descripcion)
        {
            List<CancionesHelper> result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                var qry = from cancion in db.Cancion
                          join album in db.Album on cancion.IdAlbum equals album.Id
                          join artista in db.Artista on album.IdArtista equals artista.Id
                          join genero in db.Genero on artista.IdGenero equals genero.Id
                          where cancion.Descripcion.Contains(descripcion) || album.Descripcion.Contains(descripcion) || artista.Descripcion.Contains(descripcion) || genero.Descripcion.Contains(descripcion)
                          select new { genero, artista, album, cancion };

                //if (idGenero != null)
                //    qry = from q in qry
                //          where q.genero.Id == idGenero
                //          select q;
                //if (idArtista != null)
                //    qry = from q in qry
                //          where q.artista.Id == idArtista
                //          select q;
                //if (idAlbum != null)
                //    qry = from q in qry
                //          where q.album.Id == idAlbum
                //          select q;
                result = qry.ToList().Select(s => new CancionesHelper
                {
                    IdGenero = s.genero.Id,
                    Genero = s.genero.Descripcion,
                    IdArtista = s.artista.Id,
                    Artista = s.artista.Descripcion,
                    IdAlbum = s.album.Id,
                    Album = s.album.Descripcion,
                    IdCancion = s.cancion.Id,
                    Cancion = s.cancion.Descripcion,
                    TieneReporte = s.cancion.TieneReporte
                }).OrderBy(o => o.Cancion).ToList();
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

        public Cancion ObtenerCancion(int idCancion)
        {
            Cancion result;
            ModelDataContext db = new ModelDataContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Cancion.Single(w => w.Id == idCancion);
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

        public void GeneraSolicitudCancion(int idUsuario, string idGenero, string idArtista, string idAlbum, string cancion, string comentarios)
        {
            ModelDataContext db = new ModelDataContext();
            try
            {
                Sugerencia sugerencia = new Sugerencia
                {
                    IdUsuario = idUsuario,
                    Genero = idGenero.Trim().ToUpper(),
                    Artista = idArtista.Trim().ToUpper(),
                    Album = idAlbum.Trim().ToUpper(),
                    Cancion = cancion.Trim().ToUpper(),
                    Comentarios = comentarios.Trim().ToUpper()
                };
                db.Sugerencia.AddObject(sugerencia);
                db.SaveChanges();
                BusinessCorreo.SendMail("music4alland2@gmail.com", "Solicitud de Cancion", string.Format("Genero: {0}\nArtista: {1}\nAlbum: {2}\nCanción: {3}\n Comentarios: {4}", sugerencia.Genero, sugerencia.Artista, sugerencia.Album, sugerencia.Cancion, sugerencia.Comentarios));
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
    }
}
