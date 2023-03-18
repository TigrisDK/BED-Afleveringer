using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using WebApi.Models.Model;
using WebApi.Models.Job;
using WebApi.Models.Expense;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHubContext<MessageHub> _hub;

        public ExpenseController(DataContext context, IHubContext<MessageHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }



        // DELETE: api/Expense/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteExpense(long id)
        //{
        //    var expense = await _context.Expenses.FindAsync(id);
        //    if (expense == null)
        //    {
        //        return NotFound("Expense not found");
        //    }

        //    _context.Expenses.Remove(expense);
        //    await _context.SaveChangesAsync();

        //    return Accepted();
        //}

        //private bool ExpenseExists(long id)
        //{
        //    return _context.Expenses.Any(e => e.ExpenseId == id);
        //}
    }
}
