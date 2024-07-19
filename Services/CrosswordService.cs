using Crucigrama.Interfaces;
using Crucigrama.Models;

namespace Crucigrama.Services
{
    public class CrosswordService: ICrosswordService
    {
        public Crossword GenerateCrossword(IEnumerable<string> words) {
            var crossword = new Crossword();
            crossword.AddAnswer(0,0,words.ToList()[0], Direction.Horizontal);
            return crossword;
        }
    }
}
