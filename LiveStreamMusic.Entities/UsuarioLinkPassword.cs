using System;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class UsuarioLinkPassword
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public int IdTipoLink { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public Guid Link { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public bool Activo { get; set; }
        [DataMember]
        public virtual TipoLink TipoLink { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
    }
}
