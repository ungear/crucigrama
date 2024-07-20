using Crucigrama.Models;

namespace Crucigrama.Interfaces
{
    public interface ICrosswordService
    {
        public Crossword GenerateCrossword(string[] words);
    }
}
