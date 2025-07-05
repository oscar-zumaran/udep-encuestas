namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Log_Acceso.
    /// </summary>
    public class Log_Acceso
    {
        public int iIdLogAcceso { get; set; }
        public string? cUsuario { get; set; }
        public string cIPAddress { get; set; } = string.Empty;
        public DateTime fTimestamp { get; set; }
        public string cResultado { get; set; } = string.Empty;
        public string? cUserAgent { get; set; }
        public string? cDetallesError { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
