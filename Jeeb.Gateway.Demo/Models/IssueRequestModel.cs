namespace Jeeb.Gateway.Demo.Models
{
    public class IssueRequestModel
    {
        public decimal RequestAmount { set; get; }

        public string OrderNo { set; get; }

        public string NotificationUrl { set; get; }

        public bool AllowReject { set; get; }
    }

}