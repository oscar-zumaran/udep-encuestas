namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Dimension.
    /// </summary>
    public class Dimension
    {
        public int iIdDimension { get; set; }
        public string cNombreDimension { get; set; } = string.Empty;
        public string cDescripcion { get; set; } = string.Empty;
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
