using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScriptureExplorer.Data;
using ScriptureExplorer.DTOs;
using ScriptureExplorer.Models;

namespace ScriptureExplorer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VersesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/verses/genesis/1/1
        [HttpGet("{bookName}/{chapterNumber}/{verseNumber}")]
        public async Task<ActionResult<VerseDto>> GetVerse(string bookName, int chapterNumber, int verseNumber)
        {
            var verse = await _context.Verses
                .Include(v => v.Chapter)
                .ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(v => v.Chapter.Book.Name.ToLower() == bookName.ToLower()
                                       && v.Chapter.Number == chapterNumber
                                       && v.Number == verseNumber);

            if (verse == null)
                return NotFound($"Verse not found: {bookName} {chapterNumber}:{verseNumber}");

            // Convert to DTO to avoid circular reference
            var verseDto = new VerseDto
            {
                Id = verse.Id,
                Number = verse.Number,
                Text = verse.Text,
                BookName = verse.Chapter.Book.Name,
                ChapterNumber = verse.Chapter.Number
            };

            return verseDto;
        }

        // GET: api/verses/genesis/1
        [HttpGet("{bookName}/{chapterNumber}")]
        public async Task<ActionResult<List<VerseDto>>> GetChapter(string bookName, int chapterNumber)
        {
            var verses = await _context.Verses
                .Include(v => v.Chapter)
                .ThenInclude(c => c.Book)
                .Where(v => v.Chapter.Book.Name.ToLower() == bookName.ToLower()
                         && v.Chapter.Number == chapterNumber)
                .OrderBy(v => v.Number)
                .Select(v => new VerseDto
                {
                    Id = v.Id,
                    Number = v.Number,
                    Text = v.Text,
                    BookName = v.Chapter.Book.Name,
                    ChapterNumber = v.Chapter.Number
                })
                .ToListAsync();

            if (!verses.Any())
                return NotFound($"Chapter not found: {bookName} {chapterNumber}");

            return verses;
        }
        // GET: api/verses/search?q=god
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<VerseDto>>> SearchVerses([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search query is required");

            var verses = await _context.Verses
                .Include(v => v.Chapter)
                .ThenInclude(c => c.Book)
                .Where(v => v.Text.ToLower().Contains(q.ToLower()))
                .Take(20)
                .ToListAsync();

            // Convert to DTOs
            var verseDtos = verses.Select(v => new VerseDto
            {
                Id = v.Id,
                Number = v.Number,
                Text = v.Text,
                BookName = v.Chapter.Book.Name,
                ChapterNumber = v.Chapter.Number
            }).ToList();

            return verseDtos;
        }

        // GET: api/verses/books
        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            // Books don't have circular references, so we can return them directly
            return await _context.Books.ToListAsync();
        }


    }
}