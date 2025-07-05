namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Pregunta.
    /// </summary>
    public class Pregunta
    {
        public int iIdPregunta { get; set; }
        public int iIdComponente { get; set; }
        public char cTipoPregunta { get; set; }
        public string cDescripcion { get; set; } = string.Empty;
        public bool? bVariasRespuestas { get; set; }
        public bool? bRespuestaLarga { get; set; }
        public int? iNivelCalificacion { get; set; }
        public bool bObligatoria { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
