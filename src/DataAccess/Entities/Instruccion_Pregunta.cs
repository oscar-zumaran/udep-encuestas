namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Instruccion_Pregunta.
    /// </summary>
    public class Instruccion_Pregunta
    {
        public int iIdInstruccion { get; set; }
        public int iIdPregunta { get; set; }
        public string cDescripcionInstruccion { get; set; } = string.Empty;
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
