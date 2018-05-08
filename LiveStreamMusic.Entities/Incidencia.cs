using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Incidencia
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdTipoIncidencia { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdCancion { get; set; }
        [DataMember]
        public int? IdGeneroSugerido { get; set; }
        [DataMember]
        public int? IdArtistaSugerido { get; set; }
        [DataMember]
        public int? IdAlbumSugerido { get; set; }
        [DataMember]
        public string NombreCancionSugerido { get; set; }
        [DataMember]
        public DateTime FechaIncidencia { get; set; }
        [DataMember]
        public bool Revisado { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
        [DataMember]
        public virtual Cancion Cancion { get; set; }
        [DataMember]
        public virtual Genero Genero { get; set; }
        [DataMember]
        public virtual Artista Artista { get; set; }
        [DataMember]
        public virtual Album Album { get; set; }
        [DataMember]
        public virtual List<TipoIncidencia> TipoIncidencia { get; set; }
    }
}
