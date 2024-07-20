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

        public Crossword GenerateCrossword(IEnumerable<string> words) {
            var crossword = new Crossword();
            crossword.AddAnswer(0, 0, "banana", Direction.Horizontal);
            crossword.AddAnswer(0, 2, "or", Direction.Horizontal);
            crossword.TryAddAnswer("apple", Direction.Vertical);

            _exportService.ExportToCsvOnDisc(crossword);
            return crossword;
        }
    }
}
