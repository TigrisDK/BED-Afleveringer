using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly DataContext _Context;
        private readonly IHubContext<MessageHub> _hub;
        private readonly IMapper _mapper;

        public ExpenseController(DataContext context, IMapper mapper, IHubContext<MessageHub> hub)
        {
            _Context = context;
            _hub = hub;
            _mapper = mapper;
        }
    }
}
