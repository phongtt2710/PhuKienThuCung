﻿using DAL.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharp5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangsController : ControllerBase
    {
        private readonly IGioHangService _service;

        public GioHangsController(IGioHangService service)
        {
            _service = service;
        }

        // GET: api/GioHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHang>>> GetgioHangs()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/GioHangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GioHang>> GetGioHang(int id)
        {
            var gioHang = await _service.GetOneAsync(id);

            if (gioHang == null)
            {
                return NotFound();
            }

            return gioHang;
        }

        // PUT: api/GioHangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGioHang(int id, GioHang gioHang)
        {
            if (id != gioHang.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(gioHang);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GioHangExists(id))
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

        // POST: api/GioHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GioHang>> PostGioHang(GioHang gioHang)
        {
            await _service.AddAsync(gioHang);

            return CreatedAtAction("GetGioHang", new { id = gioHang.Id }, gioHang);
        }

        // DELETE: api/GioHangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGioHang(int id)
        {
            var gioHang = await _service.GetOneAsync(id);
            if (gioHang == null)
            {
                return NotFound();
            }

            await _service.RemoveAsync(id);

            return NoContent();
        }

        private bool GioHangExists(int id)
        {
            return _service.EntityExists(id);
        }
    }
}
