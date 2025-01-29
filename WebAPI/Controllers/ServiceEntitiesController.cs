using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;

namespace WebAPI.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceEntitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiceEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceEntity>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/ServiceEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceEntity>> GetServiceEntity(int id)
        {
            var serviceEntity = await _context.Services.FindAsync(id);

            if (serviceEntity == null)
            {
                return NotFound();
            }

            return serviceEntity;
        }

        // PUT: api/ServiceEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceEntity(int id, ServiceEntity serviceEntity)
        {
            if (id != serviceEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(serviceEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceEntityExists(id))
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

        // POST: api/ServiceEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceEntity>> PostServiceEntity(ServiceEntity serviceEntity)
        {
            _context.Services.Add(serviceEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceEntity", new { id = serviceEntity.Id }, serviceEntity);
        }

        // DELETE: api/ServiceEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceEntity(int id)
        {
            var serviceEntity = await _context.Services.FindAsync(id);
            if (serviceEntity == null)
            {
                return NotFound();
            }

            _context.Services.Remove(serviceEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceEntityExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
