namespace Taallama.Domain.Commons
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }
    }
}