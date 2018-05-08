using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Album
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdArtista { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public virtual Artista Artista { get; set; }
        [DataMember]
        public virtual List<Cancion> Cancion { get; set; }
        [DataMember]
        public virtual List<Incidencia> Incidencia { get; set; }
    }
}
