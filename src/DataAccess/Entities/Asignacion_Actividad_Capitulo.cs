namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Asignacion_Actividad_Capitulo.
    /// </summary>
    public class Asignacion_Actividad_Capitulo
    {
        public int iIdAsignacion { get; set; }
        public int iIdAsignatura { get; set; }
        public int? iIdCapitulo { get; set; }
        public int iIdActividad { get; set; }
        public string cUsuarioAsignador { get; set; } = string.Empty;
        public DateTime fFechaAsignacion { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
