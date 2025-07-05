namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Log_Auditoria.
    /// </summary>
    public class Log_Auditoria
    {
        public int iIdLogAuditoria { get; set; }
        public string cTablaAfectada { get; set; } = string.Empty;
        public int iIdRegistroAfectado { get; set; }
        public string cOperacion { get; set; } = string.Empty;
        public string cUsuario { get; set; } = string.Empty;
        public DateTime fTimestamp { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
        public string? cValoresAnteriores { get; set; }
        public string? cValoresNuevos { get; set; }
        public string? cIPAddress { get; set; }
    }
}
