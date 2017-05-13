using System.Threading.Tasks;
using Jeeb.Gateway.Models;

namespace Jeeb.Gateway
{
    public class JeebApi
    {
        private readonly string _signature;
        
        public JeebApi(string signature)
        {
            _signature = signature;
        }


        public async Task<IssueInvoice> IssueInvoiceAsync(IssueInvoiceRequest issueInvoiceRequest)
        {
            var api = new RequestBuilder();
            return await api.MakeRequestAsync<IssueInvoice>(new RequestBuilder.RequestBuilderOptions
            {
                Method = "POST",
                Url = $"bitcoin/issue/{_signature}"
            }, issueInvoiceRequest);
        }

        public IssueInvoice IssueInvoice(IssueInvoiceRequest issueInvoiceRequest)
        {
            var api = new RequestBuilder();
            return api.MakeRequest<IssueInvoice>(new RequestBuilder.RequestBuilderOptions
            {
                Method = "POST",
                Url = $"bitcoin/issue/{_signature}"
            }, issueInvoiceRequest);
        }

        public async Task<ConfirmInvoiceResult> ConfirmInvoiceAsync(ConfirmInvoiceRequest confirmInvoiceRequest)
        {
            var api = new RequestBuilder();
            return await api.MakeRequestAsync<ConfirmInvoiceResult>(
                new RequestBuilder.RequestBuilderOptions
                {
                    Method = "POST",
                    Url = $"bitcoin/confirm/{_signature}"
                }, confirmInvoiceRequest);
        }

        public ConfirmInvoiceResult ConfirmInvoice(ConfirmInvoiceRequest confirmInvoiceRequest)
        {
            var api = new RequestBuilder();
            return api.MakeRequest<ConfirmInvoiceResult>(
                new RequestBuilder.RequestBuilderOptions
                {
                    Method = "POST",
                    Url = $"bitcoin/confirm/{_signature}"
                }, confirmInvoiceRequest);
        }


        public async Task<InvoiceStatusResult> InvoiceStatusAsync(InvoiceStatusRequest invoiceStatusRequest)
        {
            var api = new RequestBuilder();
            return await api.MakeRequestAsync<InvoiceStatusResult>(
                new RequestBuilder.RequestBuilderOptions
                {
                    Method = "POST",
                    Url = $"bitcoin/status/{_signature}"
                }, invoiceStatusRequest);
        }

        public InvoiceStatusResult InvoiceStatus(InvoiceStatusRequest invoiceStatusRequest)
        {
            var api = new RequestBuilder();
            return api.MakeRequest<InvoiceStatusResult>(new RequestBuilder.RequestBuilderOptions
            {
                Method = "POST",
                Url = $"bitcoin/status/{_signature}"
            }, invoiceStatusRequest);
        }

        public async Task<decimal> CurrencyConvertAsync(CurrencyConvertRequest currencyConvertRequest)
        {
            var api = new RequestBuilder();
            return await api.MakeRequestAsync<decimal>(new RequestBuilder.RequestBuilderOptions
            {
                Url =
                    $"convert/{_signature}/{currencyConvertRequest.Value}/{currencyConvertRequest.BaseCurrency.ToUpper()}/{currencyConvertRequest.TargetCurrency.ToUpper()}",
                Method = "GET"
            });
        }

        public decimal CurrencyConvert(CurrencyConvertRequest currencyConvertRequest)
        {
            var api = new RequestBuilder();
            return api.MakeRequest<decimal>(new RequestBuilder.RequestBuilderOptions
            {
                Url =
                    $"convert/{_signature}/{currencyConvertRequest.Value}/{currencyConvertRequest.BaseCurrency.ToUpper()}/{currencyConvertRequest.TargetCurrency.ToUpper()}",
                Method = "GET"
            });
        }
    }
}