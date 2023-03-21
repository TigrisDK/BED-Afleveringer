using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Expense;
using WebApi.Models.Job;
using WebApi.Models.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ModelController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            TypeAdapterConfig<ModelDtoFull, Model>.NewConfig().IgnoreNullValues(true);
        }

        // GET: api/Models
        [HttpGet]
        public List<ModelDto> GetModels()
        {
            var model = from m in _context.Models
                        select m;
            List<ModelDto> modelDtos = new List<ModelDto>();
            foreach (Model m in model)
            {
                ModelDto modelDto = _mapper.Map<ModelDto>(m);
                modelDtos.Add(modelDto);
            }

            return modelDtos;
        }



        //Hente model med den angivne ModelId inklusiv modellens jobs og udgifter
        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDtoFull>> GetModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _context.Entry(model)
                .Collection(m => m.Jobs)
                .Load();
            _context.Entry(model)
                .Collection(m => m.Expenses)
                .Load();

            ModelDtoFull retModel = _mapper.Map<ModelDtoFull>(model);
            if (model.Jobs != null)
            {
                retModel.Jobs = new List<JobDtoSimple>();

                foreach (Job j in model.Jobs)
                {
                    JobDtoSimple job = _mapper.Map<JobDtoSimple>(j);
                    retModel.Jobs.Add(job);
                }
            }
            if (model.Expenses != null)
            {
                retModel.Expenses = new List<ExpenseDto>();
                foreach (Expense e in model.Expenses)
                {
                    ExpenseDto expense = _mapper.Map<ExpenseDto>(e);
                    retModel.Expenses.Add(expense);
                }
            }

            return retModel;
        }

        // POST: api/Models
        [HttpPost]
        public async Task<ActionResult<ModelDto>> PostModel(ModelDtoNoId model)
        {

            Model newModel = _mapper.Map<Model>(model);
            _context.Models.Add(newModel);
            await _context.SaveChangesAsync();

            ModelDto retModel = _mapper.Map<ModelDto>(newModel);

            return CreatedAtAction("PostModel", retModel);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.ModelId == id);
        }

    }
}
