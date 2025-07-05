namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Actividad.
    /// </summary>
    public class Actividad
    {
        public int iIdActividad { get; set; }
        public string cNombreActividad { get; set; } = string.Empty;
        public string? cDescripcionActividad { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
