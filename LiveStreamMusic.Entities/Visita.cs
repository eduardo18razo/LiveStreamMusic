using System;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Visita
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ip { get; set; }
        [DataMember]
        public Int64 Veces { get; set; }
    }
}
