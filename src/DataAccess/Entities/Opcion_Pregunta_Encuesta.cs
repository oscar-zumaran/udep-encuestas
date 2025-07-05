namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Opcion_Pregunta_Encuesta.
    /// </summary>
    public class Opcion_Pregunta_Encuesta
    {
        public int iIdOpcionPreguntaEncuesta { get; set; }
        public int iIdEncuesta { get; set; }
        public int iIdPreguntaEncuesta { get; set; }
        public int iIdPregunta { get; set; }
        public int iIdOpcionPregunta { get; set; }
        public string cDescripcionOpcion { get; set; } = string.Empty;
        public bool bActiva { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
