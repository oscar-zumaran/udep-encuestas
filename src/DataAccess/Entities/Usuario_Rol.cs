namespace UDEP.Encuestas.DataAccess.Entities
{
    /// <summary>
    /// Entidad que representa la tabla Usuario_Rol.
    /// </summary>
    public class Usuario_Rol
    {
        public int iIdUsuarioRol { get; set; }
        public int iIdUsuario { get; set; }
        public int iIdRol { get; set; }
        public string cRegUser { get; set; } = string.Empty;
        public DateTime fRegDate { get; set; }
        public string cUpdUser { get; set; } = string.Empty;
        public DateTime fUpdDate { get; set; }
        public bool bActive { get; set; }
    }
}
