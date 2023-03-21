using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Hubs;
using AutoMapper;
using WebApi.Models.Model;
using WebApi.Models.Job;
using WebApi.Models.Expense;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;

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
            var count = await _context.Expenses.CountAsync();
            var hub= HttpContext.RequestServices.GetService<IHubContext<CountHub>>();
            await hub.Clients.All.SendAsync("countUpdated", count );
            return CreatedAtAction("PostExpense", new { id = expense.ExpenseId }, expense);

        }

       
    }
}
