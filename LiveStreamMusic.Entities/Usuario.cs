using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LiveStreamMusic.Entities
{
    [DataContract(IsReference = true)]
    public class Usuario
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public string Correo { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public DateTime FechaNacimiento { get; set; }
        [DataMember]
        public string Pwd { get; set; }
        [DataMember]
        public string PwdDes { get; set; }
        [DataMember]
        public bool Sexo { get; set; }
        [DataMember]
        public bool Activo { get; set; }
        [DataMember]
        public DateTime? FechaAlta { get; set; }
        [DataMember]
        public DateTime? FechaConfirmacion { get; set; }
        [DataMember]
        public bool Confirmado { get; set; }
        [DataMember]
        public bool Habilitado { get; set; }
        [DataMember]
        public virtual List<UsuarioLinkPassword> UsuarioLinkPassword { get; set; }
        [DataMember]
        public virtual List<Sugerencia> Sugerencia { get; set; }
        [DataMember]
        public virtual List<BitacoraAcceso> BitacoraAcceso { get; set; }
        [DataMember]
        public virtual List<Incidencia> Incidencia { get; set; }
        [DataMember]
        public virtual List<ListaReproduccion> ListaReproduccion { get; set; }

        public string NombreCompleto
        {
            get { return string.Format("{0} {1}", Nombre, Apellido); }
        }
    }
}
