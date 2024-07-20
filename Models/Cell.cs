namespace Crucigrama.Models
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Letter { get; set; }
        public List<int> WordIds { get; set; }

        public Cell(int x, int y, char letter, int wordId )
        {
            X = x;
            Y = y;
            Letter = letter;
            WordIds = new List<int> { wordId };
        }
    }
}
