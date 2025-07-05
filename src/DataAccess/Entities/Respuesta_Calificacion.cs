namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Respuesta_Calificacion.
    /// </summary>
    public class Respuesta_Calificacion
    {
        public int iIdRespuesta { get; set; }
        public int iIdSesion { get; set; }
        public int iIdPreguntaEncuesta { get; set; }
        public int iValorCalificacion { get; set; }
        public string? cUsuarioRespuesta { get; set; }
        public DateTime fFechaRespuesta { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
