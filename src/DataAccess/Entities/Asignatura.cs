namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Asignatura.
    /// </summary>
    public class Asignatura
    {
        public int iIdAsignatura { get; set; }
        public int iIdDepartamento { get; set; }
        public int? iIdComponente { get; set; }
        public string cSiglas { get; set; } = string.Empty;
        public string cNombreAsignatura { get; set; } = string.Empty;
        public int iCiclo { get; set; }
        public int iAnio { get; set; }
        public int iCreditos { get; set; }
        public bool bTieneCapitulos { get; set; }
        public int iNumeroCapitulos { get; set; }
        public bool bTieneSedeHospitalaria { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
