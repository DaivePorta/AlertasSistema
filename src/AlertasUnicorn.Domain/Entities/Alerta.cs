using System;

namespace AlertasUnicorn.Domain.Entities
{
    public class Alerta : BaseEntity
    {
        public int Id { get; set; }
        public string UsuarioDestino { get; set; }
        public string UsuarioOrigen { get; set; }
        public string Modulo { get; set; }
        public string Contenido { get; set; }
        public string Url { get; set; }
        public string CodReferencia { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
    }
}
