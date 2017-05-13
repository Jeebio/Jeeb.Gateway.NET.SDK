namespace  Jeeb.Gateway.Demo.Models
{
    public class JeebResult<TResult>
    {
        public TResult Result { set; get; }

        public bool HasError { set; get; }

        public int? ErrorCode { set; get; }

        public string ErrorMessage { set; get; }
    }

}