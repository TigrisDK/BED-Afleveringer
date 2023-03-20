using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebApi.Controllers;
using WebApi.Models;
using Microsoft.Identity.Client;

namespace WebApi.Hubs
{
    [HubName("Hub")]
    public class CountHub : Hub
    {
        public CountHub() 
        {
            
        
        }

    }
}
