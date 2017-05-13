using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeeb.Gateway.Demo.Models;
using Microsoft.AspNet.SignalR;

namespace Jeeb.Gateway.Demo.Controllers
{
    public class NotificationHub : Hub
    {
        private static readonly Dictionary<string, List<string>> _clientMap = new Dictionary<string, List<string>>();

        public static Dictionary<string, List<string>> ClientMap => _clientMap;

        public void Register(string orderNo)
        {
            if (!ClientMap.Keys.Any(param => param == orderNo))
                ClientMap.Add(orderNo, new List<string>());

            if (ClientMap[orderNo].All(param => param != Context.ConnectionId))
                ClientMap[orderNo].Add(Context.ConnectionId);
        }


        public static void BroadCastNotif(NotificationModel notifModel)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            if (ClientMap.Keys.Any(param => param == notifModel.OrderNo))
            {
                List<string> clients = ClientMap[notifModel.OrderNo];
                if (clients != null)
                    hubContext.Clients.Clients(clients)
                        //.pushNotif(notifModel.ReferenceNo, notifModel.OrderNo, notifModel.PaymentState,
                        //    notifModel.ValueIn, notifModel.Confirmation);
                        .pushNotif(notifModel);
            }


        }
    }
}
