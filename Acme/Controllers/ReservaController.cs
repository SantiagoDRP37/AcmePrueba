using Acme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public ReservaController(AplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ReservaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listReservas = await _context.Reservas.ToListAsync();
                return Ok(listReservas);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ReservaController>/5
        /*[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST api/<ReservaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reserva reserva)
        {
            try
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ReservaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Reserva reserva)
        {
            try
            {
                if(id != reserva.Id)
                {
                    return NotFound();
                }
                _context.Update(reserva);
                await _context.SaveChangesAsync();
                return Ok(new { message = "la reserva fue actualizada"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reserva = await _context.Reservas.FindAsync(id);
                if (reserva == null)
                {
                    return NotFound();
                }
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
                return Ok(new {message="La reserva a sido eliminada" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }

    public class AlarmaController : ControllerBase 
    {
        private readonly AplicationDbContext _context;
        public AlarmaController(AplicationDbContext context)
        {
            _context = context;
        }
        // GET api/<ReservaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var reserva = await _context.Reservas.FindAsync(id);
                if (reserva == null) return NotFound();
                var present = DateTime.Now;
                var comparacion = reserva.HoraFin.Hour - present.Hour;
                if (comparacion < 0)
                
                return Ok(new { message ="la sala debe estar vacia" });
                else if(comparacion == 0) { 
                    return Ok(new { message = "Por favor desalojar sala" });
                }
                else
                {
                    return Ok(new { message = "Desalojar sala en "+comparacion+" horas" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
    }
}
