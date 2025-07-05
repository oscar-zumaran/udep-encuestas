namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Periodo.
    /// </summary>
    public class Periodo
    {
        public int iIdPeriodo { get; set; }
        public int iAnioAcademico { get; set; }
        public string cNumeroPeriodo { get; set; } = string.Empty;
        public string cNombrePeriodo { get; set; } = string.Empty;
        public DateTime fFechaInicio { get; set; }
        public DateTime fFechaCulminacion { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
