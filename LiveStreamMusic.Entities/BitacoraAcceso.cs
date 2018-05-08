using System;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class BitacoraAcceso
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string Ip { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
    }
}
