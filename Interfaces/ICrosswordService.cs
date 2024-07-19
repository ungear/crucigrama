using Crucigrama.Models;

namespace Crucigrama.Interfaces
{
    public interface ICrosswordService
    {
        public Crossword GenerateCrossword(IEnumerable<string> words);
    }
}
