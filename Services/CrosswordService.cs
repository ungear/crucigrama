using Crucigrama.Interfaces;
using Crucigrama.Models;

namespace Crucigrama.Services
{
    public class CrosswordService: ICrosswordService
    {
        public Crossword GenerateCrossword(IEnumerable<string> words) {
            var crossword = new Crossword();
            crossword.AddAnswer(0, 0, "banana", Direction.Horizontal);
            crossword.AddAnswer(1, 0, "apple", Direction.Vertical);
            return crossword;
        }
    }
}
