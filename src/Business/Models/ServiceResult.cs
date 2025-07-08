namespace UDEP.Encuestas.Business.Models
{
    public enum ResultType
    {
        Success,
        SqlError,
        UnexpectedError
    }

    public class ServiceResult<T>
    {
        public ResultType ResultType { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
