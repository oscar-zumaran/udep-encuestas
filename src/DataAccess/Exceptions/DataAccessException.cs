using System;

namespace UDEP.Encuestas.DataAccess.Exceptions
{
    /// <summary>
    /// Representa un error proveniente de la capa de acceso a datos.
    /// </summary>
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
