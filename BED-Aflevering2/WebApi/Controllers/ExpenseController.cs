using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHubContext<MessageHub> _hub;
        private readonly IMapper _mapper;

        public ExpenseController(DataContext context, IMapper mapper, IHubContext<MessageHub> hub)
        {
            _context = context;
            _hub = hub;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<JobDtoWExpenses>> PostExpense(ExpenseDtoPost expense)
        {
            var model = _context.Find<Model>(expense.ModelId);
            var job = _context.Find<Job>(expense.JobId);
            
            if (model == null || job == null) 
            { 
                if (model == null) 
                {
                    return NotFound("Model not found");
                }
                return NotFound("Job not found");
            }

            _context.Entry(job)
                .Collection(j => j.Models)
                .Load();
            
            if (!job.Models.Contains(model)) 
            {
                return NotFound("Model not assigned to this job");
            }

            _context.Expenses.Add(_mapper.Map<Expense>(expense));
            await _context.SaveChangesAsync();

            var modelNames = from m in _context.Models
                             select m.FirstName;
            List<string> mName = modelNames.ToList();
            var customerName = from j in _context.Jobs
                               where j.JobId == expense.JobId
                               select j.Customer;
            List<string> sCustomer = customerName.ToList();

            await _hub.Clients.All.SendAsync("Notify Message", expense, mName[0], sCustomer[0]);

            _context.Entry(job)
                .Collection(j => j.Expenses)
                .Load();

            return Accepted(_mapper.Map<JobDtoWExpenses>(job));
        }

        
        /*
        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound("Expense not found");
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return Accepted();
        }*/

        private bool ExpenseExists(long id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
