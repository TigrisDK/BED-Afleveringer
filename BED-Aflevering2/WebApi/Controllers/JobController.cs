using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mapster;
using WebApi.Models.Job;
using WebApi.Models.Model;
using AutoMapper;
using WebApi.Models.Expense;

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
            //TypeAdapterConfig<JobPutDto, Job>.NewConfig().IgnoreNullValues(true);
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobWModelNames>>> GetJobs()
        {
            List<JobWModelNames> Jobs = new List<JobWModelNames>();
            foreach (Job j in _context.Jobs)
            {
                _context.Entry(j).Collection(job => job.Models).Load();
                JobWModelNames jobWModelNames = _mapper.Map<JobWModelNames>(j);
                if (j.Models == null)
                {
                    Jobs.Add(jobWModelNames);
                    break;
                }

                foreach(Model m in j.Models)
                {
                    string s = m.FirstName + " " + m.LastName;
                    jobWModelNames.ModelNames.Add(s);
                }
                Jobs.Add(jobWModelNames);
            }
            return Jobs;
        }


        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDtoWExpenses>> GetJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            _context.Entry(job)
                .Collection(j => j.Models)
                .Load();
            _context.Entry(job)
                .Collection(j => j.Expenses)
                .Load();

            JobDtoWExpenses retJob = _mapper.Map<JobDtoWExpenses>(job);
            retJob.Expenses = new List<ExpenseDto>();
            foreach(Expense e in job.Expenses)
            {
                ExpenseDto eDto = _mapper.Map<ExpenseDto>(e);
                retJob.Expenses.Add(eDto);
            }
            return retJob;
            
        }

        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        public async Task<ActionResult<JobDtoSimple>> PutJob(long id, JobDtoUpdate job)
        {
            Job? target = await _context.Jobs.FindAsync(id);
            if (target == null)
            {
                return BadRequest();
            }

            target.StartDate = job.StartDate;
            target.Days = job.Days;
            target.Location = job.Location;
            target.Comments = job.Comments;

            _context.Entry(target).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                { 
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            JobDtoSimple retJ = _mapper.Map<JobDtoSimple>(target);

            return retJ;
        }

        // POST: api/Jobs
        [HttpPost]
        public async Task<ActionResult<JobDtoNoId>> PostJob(JobDtoNoId job)
        {
            job.Models ??= new List<Model>();
            job.Expenses ??= new List<Expense>();
            _context.Jobs.Add(_mapper.Map<Job>(job));
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostJob", job);
        }

        // PUT: api/Jobs/AssignModel/1/2
        [HttpPut]
        [Route("api/Jobs/AssignModel/{jobId}/{modelId}")]
        public async Task<ActionResult<Job>> PostAssignModel(long jobId, long modelId)
        {
            var model = _context.Models.Single(m => m.ModelId == modelId);
            var job = _context.Jobs.Single(j => j.JobId == jobId);
            _context.Entry(job).
                Collection(j => j.Models)
                .Load();

            if (job.Models.Contains(model))
            {
                return Conflict("Model already assigned");
            }

            job.Models.Add(model);
            await _context.SaveChangesAsync();

            return Accepted($"Model {model.FirstName} assigned to {job.Customer}");
        }

        // PUT: api/Jobs/RemoveModel/1/2
        [HttpPut]
        [Route("api/Jobs/RemoveModel/{jobId}/{modelId}")]
        public async Task<ActionResult<Job>> PostRemoveModel(long jobId, long modelId)
        {
            Job? job = _context.Jobs.Find(jobId);

            Model? model = _context.Models.Find(modelId);
            if (model == null || job == null)
            {
                if (model == null)
                    return NotFound("ModelId not found");
                return NotFound("JobId not found");
            }
            _context.Entry(job)
                .Collection(j => j.Models)
                .Load();

            if (!job.Models.Contains(model))
            {
                return NotFound("Job does not have this ModelId assigned");
            }
            job.Models.Remove(model);
            await _context.SaveChangesAsync();

            return Accepted($"Model {model.FirstName} removed from job {job.Customer}");
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
        /*
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
        // PUT: api/Jobs/AssignModel/1/2
        [HttpPut]
        [Route("api/Jobs/AssignModel/{jobId}/{modelId}")]
        public async Task<ActionResult<Job>> PostAssignModel(long jobId, long modelId)
        {
            var model = _context.Models.Single(m => m.ModelId == modelId);
            var job = _context.Jobs.Single(j => j.JobId == jobId);
            _context.Entry(job).
                Collection(j => j.Models)
                .Load();

            if (job.Models.Contains(model))
            {
                return Conflict("Model already assigned");
            }

            job.Models.Add(model);
            await _context.SaveChangesAsync();

            return Accepted($"Model {model.FirstName} assigned to {job.Customer}");
        }*/

    }
}