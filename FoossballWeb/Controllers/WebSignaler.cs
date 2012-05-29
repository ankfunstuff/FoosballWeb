using System;
using FoossballPlayars.QueryContext;
using SignalR.Hubs;

namespace FoossballWeb.Controllers
{
    public class WebSignaler : Hub, ISignaler
    {
        public void Signal()
        {
            //Clients.addMessage("");
        }
    }
}