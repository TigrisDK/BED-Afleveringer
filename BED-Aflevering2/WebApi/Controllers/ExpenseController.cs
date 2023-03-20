using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using WebApi.Models.Model;
using WebApi.Models.Job;
using WebApi.Models.Expense;
using System.Diagnostics.Metrics;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHubContext<CountHub> _hub;

        public ExpenseController(DataContext context, IHubContext<CountHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            //adding counterhub
            //await _context.AddAsync(_context.Expenses.Count());

            return CreatedAtAction("PostExpense", new { id = expense.ExpenseId }, expense);

        }

    }
}
