using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class TipoCorreo
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }

        public virtual List<ParametroCorreo> ParametroCorreo { get; set; }
    }
}
