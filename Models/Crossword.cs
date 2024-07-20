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

        public Crossword() {
            Answers = new List<Answer>();
            Cells = new List<Cell>();
        }

        public void AddAnswer(int startX, int startY, string word, Direction direction) {
            Answer answer = new Answer(word, direction);
            Answers.Add(answer);
            var answerIndex = Answers.Count - 1;

            int currentX = startX;
            int currentY = startY;
            for(var index = 0; index < word.Length; index++) {
                var existingCellWithSameCoodinates = Cells.Find(c => c.X == currentX && c.Y == currentY);
                if (existingCellWithSameCoodinates != null) {
                    if (existingCellWithSameCoodinates.Letter != word[index]) 
                        throw new Exception("These coordinates are occupied with a cell with a different letter");
                    existingCellWithSameCoodinates.WordIds.Add(answerIndex);
                } else {
                    var cell = new Cell(currentX, currentY, word[index], answerIndex );
                    Cells.Add(cell);
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
        }
    }
}
