namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Capitulo.
    /// </summary>
    public class Capitulo
    {
        public int iIdCapitulo { get; set; }
        public int iIdAsignatura { get; set; }
        public string cNumeroCapitulo { get; set; } = string.Empty;
        public string cNombreCapitulo { get; set; } = string.Empty;
        public string? cDescripcionCapitulo { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
