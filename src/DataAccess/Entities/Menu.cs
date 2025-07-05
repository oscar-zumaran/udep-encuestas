namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Menu.
    /// </summary>
    public class Menu
    {
        public int iIdMenu { get; set; }
        public string cNombre { get; set; } = string.Empty;
        public string? cUrl { get; set; }
        public int iNivel { get; set; }
        public int? iIdPadre { get; set; }
        public int iOrden { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
