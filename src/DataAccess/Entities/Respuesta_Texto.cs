namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Respuesta_Texto.
    /// </summary>
    public class Respuesta_Texto
    {
        public int iIdRespuesta { get; set; }
        public int iIdSesion { get; set; }
        public int iIdPreguntaEncuesta { get; set; }
        public string? cTextoRespuesta { get; set; }
        public string? cUsuarioRespuesta { get; set; }
        public DateTime fFechaRespuesta { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
