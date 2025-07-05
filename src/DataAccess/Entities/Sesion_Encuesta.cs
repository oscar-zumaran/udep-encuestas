namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Sesion_Encuesta.
    /// </summary>
    public class Sesion_Encuesta
    {
        public int iIdSesion { get; set; }
        public int iIdEncuesta { get; set; }
        public string? cUsuarioRespuesta { get; set; }
        public DateTime fFechaInicio { get; set; }
        public DateTime? fFechaCompletado { get; set; }
        public string cEstado { get; set; } = string.Empty;
        public string? cIPAddress { get; set; }
        public string? cCodigoConfirmacion { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
