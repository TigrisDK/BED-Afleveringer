using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;


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


        // get delen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDtoWExpenses>>> GetJobs()
        {
            var job = await context_.Jobs.ToListAsync();
            if(job == null)
            {
                return NotFound();
            }

            var allJobs = new List<JobDtoWExpenses>();
            foreach (var Item in job)
            {
                context_.Entry(Item).Collection(job => job.Models).Load();

                var alljobs = Item.Adapt<JobDtoWExpenses>();

                alljobs.JobId = Item.JobId;

                if (Item.Models != null)
                {
                    alljobs.ModelNames = new List<string>();
                    foreach (var model in Item.Models)
                    {
                        string modelfullname = model.FirstName + " " + model.LastName;
                        alljobs.ModelNames.Add(modelfullname);
                    }
                }
                allJobs.Add(alljobs);
            }

            return allJobs;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(long id)
        {
            var job = await context_.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }


        // put delen



    }
}