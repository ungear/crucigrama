using Crucigrama.Db;
using Crucigrama.Interfaces;
using Crucigrama.Models;
using Crucigrama.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crucigrama.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrosswordController : Controller
    {
        private ICrosswordService _crosswordService;
        private readonly CrucigramaContext _crucigramaContext;

        public CrosswordController(
            ICrosswordService crosswordService, 
            CrucigramaContext crucigramaContext) {
            _crosswordService = crosswordService;
            _crucigramaContext = crucigramaContext;
        }

        [HttpGet(Name = "GetCrossword")]
        public async Task<Crossword> GetCrossword()
        {
            var a = await _crucigramaContext.Answers.ToListAsync();

            Crossword crossword = _crosswordService.GenerateCrossword(new string[] { "banana", "apple", "milk", "execution", "success", "exit", "similar"});
            return crossword;
        }
    }
}
