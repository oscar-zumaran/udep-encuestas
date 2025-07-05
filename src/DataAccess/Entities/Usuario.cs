namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Usuario.
    /// </summary>
    public class Usuario
    {
        public int iIdUsuario { get; set; }
        public string cUsuario { get; set; } = string.Empty;
        public string cPasswordHash { get; set; } = string.Empty;
        public string? cCorreo { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
