using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mapster;
using WebApi.Models.Job;
using WebApi.Models.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase 
    {
        private readonly DataContext _context;

        public JobController(DataContext context)
        {
            _context= context;
            TypeAdapterConfig<JobPutDto, Job>.NewConfig().IgnoreNullValues(true);
        }

        // get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var jobs = await _context.Jobs.Include(j => j.Models).ToListAsync();
            List<JobListModelNamesDto> jobsWithModelNames = jobs.Adapt<List<JobListModelNamesDto>>();

            if (jobs == null) return NotFound();
            return Ok(jobsWithModelNames);
        }


        // get api jobs id 
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job.Adapt<JobListExpenseDto>());
        }

        [HttpGet("model/{modelId}")]
        public async Task<IList<ModelDtoFull>> GetJobModel(long modelId)
        {
            var model = await _context.Models.Where(x => x.ModelId == modelId).ProjectToType<ModelDtoFull>().ToListAsync();
            return model;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(long id, JobPutDto job)
        {

            if (id != job.JobId)
            {
                return BadRequest("Id not match a job id");
            }

            _context.Entry(job.Adapt<Job>()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound("Job does not exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }

        //Opret nyt job
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(JobDto job)
        {
            Ok(_context.Jobs.Add(job.Adapt<Job>()));
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostJob", new { id = job.JobId }, job);
        }

        //Opdatere et job – kun StartDate, Days, Location og Comments kan ændres
        [HttpPut("{JobId}/{ModelId}")]
        public async Task<ActionResult<Model>> PutModelToJob(long JobId, long ModelId)
        {
            var job = await _context.Jobs
                .Include(j => j.Models)
                .Where(j => j.JobId == JobId)
                .FirstOrDefaultAsync();

            if (job == null) return NotFound();

            var model = await _context.Models.SingleAsync(m => m.ModelId == ModelId);
            if (model == null) return NotFound(" Model with the specified id does not exist");
            if (job.Models == null) return NotFound("job.Models is null!?");
            if (job.Models.Contains(model) == false) ((List<Model>)(job.Models)).Add(model);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Slette et job
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{JobId}/{ModelId}")]
        public async Task<ActionResult<Model>> DeleteModelToJob(long JobId, long ModelId)
        {
            var job = await _context.Jobs
                .Include(j => j.Models)
                .Where(j => j.JobId == JobId)
                .FirstOrDefaultAsync();

            if (job == null) return NotFound();

            var model = await _context.Models.SingleAsync(m => m.ModelId == ModelId);
            if (model == null) return NotFound("Model with the specified id does not exist");
            if (job.Models == null) return NotFound("job.Models is null!?");
            if (job.Models.Contains(model) == true) ((List<Model>)(job.Models)).Remove(model);
            else return NotFound("Job does not contain Job. Use [PUT] to add");

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}