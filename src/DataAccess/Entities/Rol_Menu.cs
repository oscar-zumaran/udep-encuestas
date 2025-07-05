namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Rol_Menu.
    /// </summary>
    public class Rol_Menu
    {
        public int iIdRolMenu { get; set; }
        public int iIdRol { get; set; }
        public int iIdMenu { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
