using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FulaOrdAPI.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace FulaOrdAPI.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class FulaOrdController : ControllerBase
    {
        private readonly FulaOrdAPIContext _context;

        public FulaOrdController(FulaOrdAPIContext context)
        {
            _context = context;
        }

        // GET: api/FulaOrd
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FulaOrd>>> GetFulaOrd()
        {
            return await _context.FulaOrd.ToListAsync();
        }

        // GET: api/FulaOrd/message
        [HttpPut]
        public async Task<ActionResult<FulaOrd>> GetFulaOrd(FulaOrd message)
        {
            IEnumerable<FulaOrd> fulaOrdList = await _context.FulaOrd.ToListAsync();

            string replacement = "******";

            foreach (var ord in fulaOrdList)
            {
                message.Word = Regex.Replace(message.Word, ord.Word, replacement, RegexOptions.IgnoreCase);
            }

            return message;
        }

        //POST: api/FulaOrd
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FulaOrd>> PostFulaOrd(FulaOrd fulaOrd)
        {
            if (!FulaOrdExists(fulaOrd.Word))
            {
                _context.FulaOrd.Add(fulaOrd);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetFulaOrd", new { id = fulaOrd.Id }, fulaOrd);

            }

            return NotFound();
        }

        //DELETE: api/FulaOrd/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FulaOrd>> DeleteTodoItem(int id)
        {
            var fultOrd = await _context.FulaOrd.FindAsync(id);
            if (fultOrd == null)
            {
                return NotFound();
            }

            _context.FulaOrd.Remove(fultOrd);
            await _context.SaveChangesAsync();

            return fultOrd;
        }

        private bool FulaOrdExists(string word)
        {
            return _context.FulaOrd.Any(e => e.Word.ToLower() == word.ToLower());
        }
    }
}
