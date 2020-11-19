using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Digital.Data;
using Digital.Models;

namespace Digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosController : ControllerBase
    {
        private readonly DigitalContext _context;

        public DadosController(DigitalContext context)
        {
            _context = context;
        }

        // GET: api/Dados
        [HttpGet]
        public IEnumerable<Dados> GetDados()
        {
            return _context.Dados;
        }

        // GET: api/Dados/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDados([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dados = await _context.Dados.FindAsync(id);

            if (dados == null)
            {
                return NotFound();
            }

            return Ok(dados);
        }

        // PUT: api/Dados/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDados([FromRoute] int id, [FromBody] Dados dados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dados.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(dados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DadosExists(id))
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

        // POST: api/Dados
        //[HttpPost]
        //public async Task<IActionResult> PostDados([FromBody] Dados dados)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Dados.Add(dados);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDados", new { id = dados.CustomerId }, dados);
        //}

        // DELETE: api/Dados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDados([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dados = await _context.Dados.FindAsync(id);
            if (dados == null)
            {
                return NotFound();
            }

            _context.Dados.Remove(dados);
            await _context.SaveChangesAsync();

            return Ok(dados);
        }

        private bool DadosExists(int id)
        {
            return _context.Dados.Any(e => e.CustomerId == id);
        }

        [HttpPost]
        public async Task<IActionResult> ApiOne([FromBody] Dados dados)
        {
            var lengthCardNumber = dados.CardNumber.ToString().Length;
            var lengthCVV = dados.CVV.ToString().Length;

            if (lengthCardNumber > 16 || lengthCardNumber < 1 )
            {
                return CreatedAtAction("GetDados", new { id = dados.CustomerId });
            }

            else if (lengthCVV > 5 || lengthCardNumber < 1)
            {
                return CreatedAtAction("GetDados", new { id = dados.CustomerId });
            }


            else
            {
                var dadosRetorno = new DadosRetorno(dados);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Dados.Add(dados);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDados", new { id = dados.CustomerId }, dadosRetorno);
            }

            
        }
    }

   
}