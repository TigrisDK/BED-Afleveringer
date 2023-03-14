using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase 
    {
        private readonly DataContext context_;
        private readonly IMapper mapper_;

        public JobController(DataContext context, IMapper mapper)
        {
            context_= context;
            mapper_ = mapper;
        }
       
    }
}
