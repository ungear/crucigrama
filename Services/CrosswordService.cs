using Crucigrama.Interfaces;
using Crucigrama.Models;

namespace Crucigrama.Services
{
    public class CrosswordService: ICrosswordService
    {
        private IExportService _exportService;

        public CrosswordService(IExportService exportService)
        {
            _exportService = exportService;
        }

        public Crossword GenerateCrossword(string[] words) {
            var crossword = new Crossword();
            var wordsList = words.ToList();

            crossword.AddAnswer(0, 0, words[0], Direction.Horizontal);
            wordsList.RemoveAt(0);
            var round = 1;
            var attemptsNumber = 2;
            while (round <= attemptsNumber && wordsList.Count > 0) {
                for (int i = 0; i < wordsList.Count; i++) {
                    var isSuccess = crossword.TryAddAnswer(wordsList[i], Direction.Horizontal);
                    if(!isSuccess) {
                        isSuccess = crossword.TryAddAnswer(wordsList[i], Direction.Vertical);
                    }
                    if (isSuccess)
                    {
                        wordsList.Remove(wordsList[i]);
                        i--;
                    }
                }
                round++;            
            }

            crossword.NormalizeCoords();
            _exportService.ExportToCsvOnDisc(crossword);
            return crossword;
        }
    }
}
