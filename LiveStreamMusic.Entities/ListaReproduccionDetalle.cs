using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class ListaReproduccionDetalle
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdListaReproduccion { get; set; }
        [DataMember]
        public int IdCancion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual ListaReproduccion ListaReproduccion { get; set; }
        [DataMember]
        public virtual Cancion Cancion { get; set; }
    }
}
