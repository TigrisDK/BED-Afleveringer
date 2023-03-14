using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ModelController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ModelController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Models
        [HttpGet]
        public List<ModelDto> GetModels()
        {
            var models = from m in _context.Models
                         select m;
            List<ModelDto> modelDtos = new List<ModelDto>();
            foreach (Model m in models)
            {
                ModelDto mDto = _mapper.Map<ModelDto>(m);
                modelDtos.Add(mDto);
            }
            return modelDtos;
        }

        // GET: api/Models/5
        [HttpGet]
        public async Task<ActionResult<ModelDtoFull>> GetModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound("Model could not be found");
            }

            _context.Entry(model)
                .Collection(m => m.Jobs)
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

        // DELETE: api/Model/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null) 
            {
                return NotFound("Model not found");
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
