namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Oferta_Academica.
    /// </summary>
    public class Oferta_Academica
    {
        public int iIdOfertaAcademica { get; set; }
        public int iIdPeriodo { get; set; }
        public int iIdAsignatura { get; set; }
        public int iNumeroMatriculados { get; set; }
        public string cJefeCurso { get; set; } = string.Empty;
        public int iAprobados { get; set; }
        public int iDesaprobados { get; set; }
        public int iRetirados { get; set; }
        public int iAnulaciones { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
