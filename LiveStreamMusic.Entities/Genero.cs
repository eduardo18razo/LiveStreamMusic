using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Genero
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public virtual List<Artista> Artista { get; set; }
        [DataMember]
        public virtual List<Incidencia> Incidencia { get; set; }
    }
}
