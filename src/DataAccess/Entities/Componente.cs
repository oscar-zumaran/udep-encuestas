namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Componente.
    /// </summary>
    public class Componente
    {
        public int iIdComponente { get; set; }
        public string cSiglas { get; set; } = string.Empty;
        public string cNombreComponente { get; set; } = string.Empty;
        public string? cDescripcion { get; set; }
        public bool bEliminarOpcionesEncuesta { get; set; }
        public bool bVisibleReportes { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
