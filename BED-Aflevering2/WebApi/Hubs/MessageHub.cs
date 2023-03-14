using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Hubs
{
    [HubName("Hub")]
    public class MessageHub : Hub
    {
        public MessageHub() 
        { 
        
        }

    }
}
