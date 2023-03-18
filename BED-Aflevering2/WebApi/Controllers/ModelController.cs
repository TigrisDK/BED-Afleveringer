using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly DataContext _context;
        public ModelController(DataContext context)
        {
            _context = context;
            TypeAdapterConfig<ModelDtoFull, Model>.NewConfig().IgnoreNullValues(true);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            return Ok(await _context.Models.ProjectToType<ModelDtoFull>().ToListAsync());
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(long id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPost]
        public async Task<ActionResult<ModelDtoFull>> PostModel(ModelDtoFull model)
        {
            _context.Models.Add(model.Adapt<Model>());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.ModelId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(long id, ModelDtoFull model)
        {

            if (id != model.ModelId)
            {
                return BadRequest("The id in the url is different from the id in the body.");
            }

            _context.Entry(model.Adapt<Model>()).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

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

            return NoContent();
        }

        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.ModelId == id);
        }
    }
}
