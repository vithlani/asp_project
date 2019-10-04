using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practicse1.Models;

namespace Practicse1
{
    [Route("api/[InventoryItems]")]
    [ApiController]
    public class InventoryItemsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public InventoryItemsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/InventoryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItems>>> GetInventoryItems()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        // GET: api/InventoryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItems>> GetInventoryItems(long id)
        {
            var inventoryItems = await _context.InventoryItems.FindAsync(id);

            if (inventoryItems == null)
            {
                return NotFound();
            }

            return inventoryItems;
        }

        // PUT: api/InventoryItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryItems(long id, InventoryItems inventoryItems)
        {
            if (id != inventoryItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventoryItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemsExists(id))
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

        // POST: api/InventoryItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InventoryItems>> PostInventoryItems(InventoryItems inventoryItems)
        {
            _context.InventoryItems.Add(inventoryItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInventoryItems)    , new { id = inventoryItems.Id }, inventoryItems);
        }

        // DELETE: api/InventoryItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InventoryItems>> DeleteInventoryItems(long id)
        {
            var inventoryItems = await _context.InventoryItems.FindAsync(id);
            if (inventoryItems == null)
            {
                return NotFound();
            }

            _context.InventoryItems.Remove(inventoryItems);
            await _context.SaveChangesAsync();

            return inventoryItems;
        }

        private bool InventoryItemsExists(long id)
        {
            return _context.InventoryItems.Any(e => e.Id == id);
        }
    }
}
