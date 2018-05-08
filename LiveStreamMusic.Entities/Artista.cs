using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Artista
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdGenero { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual List<Album> Album { get; set; }
        [DataMember]
        public virtual Genero Genero { get; set; }
        [DataMember]
        public virtual List<Incidencia> Incidencia { get; set; }

    }
}
