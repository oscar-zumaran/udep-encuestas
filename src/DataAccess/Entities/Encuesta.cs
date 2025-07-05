namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Encuesta.
    /// </summary>
    public class Encuesta
    {
        public int iIdEncuesta { get; set; }
        public int iIdOfertaAcademica { get; set; }
        public string cTitulo { get; set; } = string.Empty;
        public string cInstrucciones { get; set; } = string.Empty;
        public DateTime fFechaHoraInicio { get; set; }
        public DateTime fFechaHoraFin { get; set; }
        public bool bEsAnonima { get; set; }
        public bool bActiva { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
