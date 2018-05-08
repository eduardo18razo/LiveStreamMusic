using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Cancion
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdAlbum { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public byte[] Data { get; set; }
        [DataMember]
        public bool TieneReporte { get; set; }
        [DataMember]
        public virtual Album Album { get; set; }
        [DataMember]
        public virtual List<Incidencia> Incidencia { get; set; }

        [DataMember]
        public virtual List<ListaReproduccionDetalle> ListaReproduccionDetalle { get; set; }
    }
}
