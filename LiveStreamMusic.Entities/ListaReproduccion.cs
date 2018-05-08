using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class ListaReproduccion
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitada { get; set; }
        [DataMember]
        public virtual List<ListaReproduccionDetalle> ListaReproduccionDetalle { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
    }
}
