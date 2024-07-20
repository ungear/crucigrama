using System.Drawing;

namespace Crucigrama.Models
{
    public enum Direction { 
        Horizontal,
        Vertical
    }

    public class Crossword
    {
        public List<Answer> Answers { get; set; }

        public List<Cell> Cells { get; set; }

        public List<Cell> VerticalLinkingTargets { get; set; }
        public List<Cell> HorizontalLinkingTargets { get; set; }

        public Crossword() {
            Answers = new List<Answer>();
            Cells = new List<Cell>();
            VerticalLinkingTargets = new List<Cell>();
            HorizontalLinkingTargets = new List<Cell>();
        }

        public void AddAnswer(int startX, int startY, string word, Direction direction) {
            Answer answer = new Answer(word, direction);
            Answers.Add(answer);
            var answerIndex = Answers.Count - 1;

            int currentX = startX;
            int currentY = startY;
            var crossingCells = new List<Cell>();

            for(var index = 0; index < word.Length; index++) {
                var existingCellWithSameCoodinates = Cells.Find(c => c.X == currentX && c.Y == currentY);
                if (existingCellWithSameCoodinates != null) {
                    if (existingCellWithSameCoodinates.Letter != word[index]) 
                        throw new Exception("These coordinates are occupied with a cell with a different letter");
                    existingCellWithSameCoodinates.WordIds.Add(answerIndex);
                    crossingCells.Add(existingCellWithSameCoodinates);
                } else {
                    var cell = new Cell(currentX, currentY, word[index], answerIndex );
                    Cells.Add(cell);
                    if(direction == Direction.Horizontal)
                        VerticalLinkingTargets.Add(cell);
                    else
                        HorizontalLinkingTargets.Add(cell);
                }

                if (direction == Direction.Horizontal)
                {
                    currentX++;
                }
                else
                {
                    currentY++;
                }
            }

            // make a crossing cell and neibourg cells unavailable for future crossing
            foreach (var crossingCell in crossingCells) {
                HorizontalLinkingTargets.Remove(crossingCell);
                VerticalLinkingTargets.Remove(crossingCell);

                var cellUp = Cells.FirstOrDefault(c => c.X == crossingCell.X && c.Y == crossingCell.Y - 1);
                if (cellUp != null) HorizontalLinkingTargets.Remove(cellUp);
                        
                var cellDown = Cells.FirstOrDefault(c => c.X == crossingCell.X && c.Y == crossingCell.Y + 1);
                if (cellDown != null) HorizontalLinkingTargets.Remove(cellDown);
                
                var cellLeft = Cells.FirstOrDefault(c => c.X == crossingCell.X - 1 && c.Y == crossingCell.Y);
                if (cellLeft != null) VerticalLinkingTargets.Remove(cellLeft);

                var cellRight = Cells.FirstOrDefault(c => c.X == crossingCell.X + 1 && c.Y == crossingCell.Y);
                if (cellRight != null) VerticalLinkingTargets.Remove(cellRight);
            }
        }

        public bool TryAddAnswer(string word, Direction direction)
        {
            var linkingTargets = direction == Direction.Horizontal
                ? HorizontalLinkingTargets
                : VerticalLinkingTargets;

            foreach (var cell in linkingTargets) {
                var firstCrossingIndex = word.IndexOf(cell.Letter);
                if (firstCrossingIndex >= 0) {
                    var startX = direction == Direction.Horizontal
                        ? cell.X - firstCrossingIndex
                        : cell.X;
                    var startY = direction == Direction.Horizontal
                        ? cell.Y
                        : cell.Y - firstCrossingIndex;

                    // cells which will be occupied by the word if we start it from the selecetd point
                    var potentialCells = word.Select((char letter, int index) => {
                        var x = direction == Direction.Horizontal
                            ? startX + index
                            : startX;
                        var y = direction == Direction.Horizontal
                            ? startY
                            : startY + index;
                        return new { X = x, Y = y, Letter = letter }; 
                    });

                    // existing cells on the word path
                    var potentialCrossings = potentialCells
                        .Select(pc => new { existingCell = Cells.FirstOrDefault(c => c.X == pc.X && c.Y == pc.Y), potentialCell = pc })
                        .Where(x => x.existingCell != null);

                    // check if ALL potentialCrossings are eligible
                    var isExistingCellsOk = potentialCrossings.All(x => 
                        linkingTargets.Contains(x.existingCell) && x.existingCell.Letter == x.potentialCell.Letter);

                    if (isExistingCellsOk) { 
                        AddAnswer(startX, startY, word, direction);
                        return true;
                    }
                }
            }

            return false;

        }

        public void NormalizeCoords() { 
            var minX = Cells.Select(x => x.X).Min();
            var minY = Cells.Select(x => x.Y).Min();
            Cells.ForEach(c => {
                c.X = c.X - minX;
                c.Y = c.Y - minY;
            });
        }
    }
}
