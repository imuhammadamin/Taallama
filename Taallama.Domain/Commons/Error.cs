namespace Taallama.Domain.Commons
{
    public class Error
    {
        public short Code { get; set; }
        public string Message { get; set; }

        public Error(short Code, string Message)
        {
            this.Code = Code;
            this.Message = Message;
        }
    }
}