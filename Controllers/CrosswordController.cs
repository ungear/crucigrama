using Crucigrama.Interfaces;
using Crucigrama.Models;
using Crucigrama.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crucigrama.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrosswordController : Controller
    {
        private ICrosswordService _crosswordService;
        public CrosswordController(ICrosswordService crosswordService) {
            _crosswordService = crosswordService;
        }

        [HttpGet(Name = "GetCrossword")]
        public Crossword GetCrossword()
        {
            Crossword crossword = _crosswordService.GenerateCrossword(new string[] { "banana", "apple"});
            return crossword;
        }
    }
}
