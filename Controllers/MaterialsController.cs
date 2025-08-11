using inventoryapp.Data;
using inventoryapp.Models;
using inventoryapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventoryapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly inventoryappContext _context;
        private readonly AuthApiService _postApiService;

        public MaterialsController(inventoryappContext context, AuthApiService postApiService)
        {
            _context = context;
            _postApiService = postApiService;
        }

        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterial()
        {
            return await _context.Material.ToListAsync();
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(int id)
        {
            var material = await _context.Material.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // PUT: api/Materials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(int id, Material material)
        {

            var headers = Request.Headers;
            var response = await _postApiService.LoginAsync(headers["Authorization"].ToString());
            response.EnsureSuccessStatusCode();

            if (id != material.MaterialID)
            {
                return BadRequest();
            }

            _context.Entry(material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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


        // POST: api/Materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Material>> PostMaterial(Material material)
        {

            var headers = Request.Headers;
            var response = await _postApiService.LoginAsync(headers["Authorization"].ToString());
            response.EnsureSuccessStatusCode();
            
            _context.Material.Add(material);
            await _context.SaveChangesAsync();

            return Ok(new 
            { 
                message="PostMaterial", Body = material 
            }
            );
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            var headers = Request.Headers;
            var response = await _postApiService.LoginAsync(headers["Authorization"].ToString());
            response.EnsureSuccessStatusCode();

            var material = await _context.Material.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Material.Remove(material);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialExists(int id)
        {
            return _context.Material.Any(e => e.MaterialID == id);
        }
    }
}
