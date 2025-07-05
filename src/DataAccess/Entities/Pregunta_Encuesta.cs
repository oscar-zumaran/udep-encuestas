namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Pregunta_Encuesta.
    /// </summary>
    public class Pregunta_Encuesta
    {
        public int iIdPreguntaEncuesta { get; set; }
        public int iIdEncuesta { get; set; }
        public int iIdPregunta { get; set; }
        public char cTipoPregunta { get; set; }
        public string cDescripcion { get; set; } = string.Empty;
        public bool? bVariasRespuestas { get; set; }
        public bool? bRespuestaLarga { get; set; }
        public int? iNivelCalificacion { get; set; }
        public bool bObligatoria { get; set; }
        public int iOrden { get; set; }
        public bool bActiva { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
