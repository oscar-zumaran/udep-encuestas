namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Rol.
    /// </summary>
    public class Rol
    {
        public int iIdRol { get; set; }
        public string cNombreRol { get; set; } = string.Empty;
        public string? cDescripcion { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
