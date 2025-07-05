namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Respuesta_Opcion.
    /// </summary>
    public class Respuesta_Opcion
    {
        public int iIdRespuesta { get; set; }
        public int iIdSesion { get; set; }
        public int iIdPreguntaEncuesta { get; set; }
        public int iIdOpcionSeleccionada { get; set; }
        public string? cUsuarioRespuesta { get; set; }
        public DateTime fFechaRespuesta { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
