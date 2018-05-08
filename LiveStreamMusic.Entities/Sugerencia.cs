using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Sugerencia
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public string Genero { get; set; }
        [DataMember]
        public string Artista { get; set; }
        [DataMember]
        public string Album { get; set; }
        [DataMember]
        public string Cancion { get; set; }
        [DataMember]
        public string Comentarios { get; set; }

        [DataMember]
        public virtual Usuario Usuario { get; set; }
        
    }
}
