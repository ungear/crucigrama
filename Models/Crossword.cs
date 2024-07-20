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

            // make crossing a crossing cell and neibourg cells unavailable for future crossing
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

        //public bool TryAddingAnswer(string word, Direction direction) { 
        
        //}
    }
}
