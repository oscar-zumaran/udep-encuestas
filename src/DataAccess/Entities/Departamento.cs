namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Departamento.
    /// </summary>
    public class Departamento
    {
        public int iIdDepartamento { get; set; }
        public string cNombreDepartamento { get; set; } = string.Empty;
        public string cCorreoInstitucional { get; set; } = string.Empty;
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
