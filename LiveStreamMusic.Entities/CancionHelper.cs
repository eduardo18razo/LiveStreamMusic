using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    public class CancionHelper
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
    }
}
