using Newtonsoft.Json;

namespace Taallama.Domain.Commons
{
    public class BaseResponse<T>
    {
        [JsonIgnore]
        public int? Code { get; set; } = 200;
        public T Data { get; set; }
        public Error Error { get; set; }
    }
}