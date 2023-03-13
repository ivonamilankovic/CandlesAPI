using CandlesAPI.Data;
using CandlesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace CandlesAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class CandlesController : ControllerBase
    {
        private readonly CandlesContext _context;

        public CandlesController(CandlesContext candlesContext)
        {
            _context = candlesContext;
        }

        /// <summary>
        /// Returns a list of candles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllCandles()
        {
            IList<Candle> candles = await _context.Candles.ToListAsync();
            if(candles == null)
            {
                return NotFound();
            }
            return Ok(candles);
        }
        
        /// <summary>
        /// Returns a candle by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Candle>> GetOneCandle(int id)
        {
            Candle candle = await _context.Candles.FirstOrDefaultAsync(x => x.Id == id);
            if (candle == null)
            {
                return NotFound();
            }
            return Ok(candle);
        }

        /// <summary>
        /// Creates a new candle.
        /// </summary>
        /// <param name="newCandle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewCandle([FromBody] Candle newCandle)
        {
            if(newCandle == null)
            {
                return BadRequest();
            }
            _context.Candles.Add(newCandle);
            await _context.SaveChangesAsync();
            return Created("", newCandle);
        }

        /// <summary>
        /// Modify existing candle data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="candleUpdate"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCandle(int id, JsonPatchDocument<Candle> candleUpdate)
        {
            Candle existingCandle = await _context.Candles.FirstOrDefaultAsync(x => x.Id == id);
            if(existingCandle == null)
            {
                return NotFound();
            }
            candleUpdate.ApplyTo(existingCandle);
            await _context.SaveChangesAsync();
            return Ok(existingCandle);

        }

        /// <summary>
        /// Deletes candle data by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteOneCandle([FromQuery] int id)
        {
            Candle candleToDelete = await _context.Candles.FirstOrDefaultAsync(x => x.Id == id);
            if (candleToDelete == null)
            {
                return BadRequest();
            }
            
            _context.Candles.Remove(candleToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
