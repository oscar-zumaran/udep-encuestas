namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Opcion_Pregunta.
    /// </summary>
    public class Opcion_Pregunta
    {
        public int iIdOpcion { get; set; }
        public int iIdPregunta { get; set; }
        public string cDescripcionOpcion { get; set; } = string.Empty;
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
