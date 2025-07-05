namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Asignacion_Componente_Actividad.
    /// </summary>
    public class Asignacion_Componente_Actividad
    {
        public int iIdAsignacion { get; set; }
        public int iIdActividad { get; set; }
        public int iIdComponente { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
