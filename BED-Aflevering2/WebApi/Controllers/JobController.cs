using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase 
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public JobController(DataContext context, IMapper mapper)
        {
            _context= context;
            _mapper = mapper;
        }


        // get delen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDtoWExpenses>>> GetJobs()
        {
            var job = await _context.Jobs.ToListAsync();
            if(job == null)
            {
                return NotFound();
            }

            var all_Job = new List<JobDtoWExpenses>();
            foreach (var Item in job)
            {
                _context.Entry(Item).Collection(job => job.Models).Load();

                var all_job = Item.Adapt<JobDtoWExpenses>();

                all_job.JobId = Item.JobId;

                if (Item.Models != null)
                {
                    all_job.Model_Name = new List<string>();
                    foreach (var model in Item.Models)
                    {
                        string modelfullname = model.FirstName + " " + model.LastName;
                        all_job.Model_Name.Add(modelfullname);
                    }
                }
                all_Job.Add(all_job);
            }

            return all_Job;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }


        // put delen





    }
}