using Crucigrama.Interfaces;
using Crucigrama.Models;

namespace Crucigrama.Services
{
    public class ExportService : IExportService
    {
        public bool ExportToCsvOnDisc(Crossword crossword) {
            using (var writer = new StreamWriter("test.csv")) {
                var columnsNumber = crossword.Cells.Max(x => x.X);
                var table = crossword.Cells.GroupBy(x => x.Y).OrderBy(x => x.Key);
                foreach (var group in table)
                {
                    group.OrderBy(x => x.X);
                    string line = "";
                    for (var i = 0; i <= columnsNumber; i++) {
                        var ccrosswordCellell = group.FirstOrDefault(c => c.X == i);
                        if (ccrosswordCellell == null)
                        {
                            line += " ,";
                        }
                        else {
                            line += $"{ccrosswordCellell.Letter},";
                        }
                    }
                    writer.WriteLine(line);
                }
                writer.Flush();
            }
            return true;
        }
    }
}
