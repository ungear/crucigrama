using Crucigrama.Models;

namespace Crucigrama.Interfaces
{
    public interface IExportService
    {
        public bool ExportToCsvOnDisc(Crossword crossword);
    }
}
