using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Jeeb.Gateway.Demo.Models;
using Jeeb.Gateway.Models;
using Newtonsoft.Json;

namespace Jeeb.Gateway.Demo.Controllers
{
    public class JeebController : Controller
    {
        private readonly string gatewayBaseUrl = "http://localhost:9876/api/bitcoin/";
        private readonly string signature = "6dfcd123d77d87f4634897098a389a6b165";

        private readonly string baseUrl = "http://localhost:3249/";
        private readonly string token =
            "BEAAAADjPOYyQUWrECqpQkLzSr30WyXTwhceYhl8FqcjhiW2hW0XTjV_Ofc8cX9NEUlqBTUkzaNdG0ZnAEI4iXGsOjZo=";

        private static List<Payment> _paymentsRepo = new List<Payment>();
        // GET: Jeeb
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInvoice(CreateInvoiceModel createInvoice)
        {
            
            var invoice = new JeebApi(signature).IssueInvoice(new IssueInvoiceRequest()
            {
                AllowReject = false,
                OrderNo = createInvoice.OrderNo,
                CallBackUrl = $"{baseUrl}jeeb/callback",
                NotificationUrl = $"{baseUrl}jeeb/notification",
                RequestAmount = createInvoice.RequestAmount,
            });

            _paymentsRepo.Add( new Payment()
            {
                Token = invoice.Token,
                IsConfirmed = false,
                ReferenceNo = invoice.ReferenceNo,
            });
            
            return View(new CreateInvoiceViewModel{ Token = invoice.Token});

        }

       

        [HttpPost]
        public ActionResult CallBack(CallBack callBack)
        {
            if (callBack.StateId == 3) //Means that transaction has been made and we are waiting for confirmation, 
                return View(new CallBackViewModel
                {
                    RequestAmount = callBack.RequestAmount,
                    ReferenceNo = callBack.ReferenceNo,
                    OrderNo = callBack.OrderNo,
                    IsOk = true
                });
            return View(new CallBackViewModel
            {
                RequestAmount = callBack.RequestAmount,
                ReferenceNo = callBack.ReferenceNo,
                OrderNo = callBack.OrderNo,
                IsOk = true
            });
            
        }


        [HttpPost]
        public void Notification(Jeeb.Gateway.Models.Notification notif)
        {
            if (notif.Signature == signature && notif.StateId == 4)
            {
                //Check if signature equals to your signature, since no has this signature, i get to know if this call is from us or not
                //if( everything is okey on your side ) Retrieve invoice record, check its status, if its okey then continue to confirm this payment 

                var payment = _paymentsRepo.First(param => param.ReferenceNo == notif.ReferenceNo);
                
                var jeebApi = new JeebApi(signature);

                var confirmResult = jeebApi.ConfirmInvoice(new ConfirmInvoiceRequest()
                {
                    Token = payment.Token,
                });

                payment.IsConfirmed = true;
            }
        }

        
        private class Payment
        {
            public string ReferenceNo { set; get; }

            public string Token { set; get; }

            public bool IsConfirmed { set; get; }
        }
    }
}