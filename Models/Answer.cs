namespace Crucigrama.Models
{
    public class Answer
    {   
        public string Word { get; set; }
        public Direction Direction { get; set; }

        public Answer (string word, Direction direction)
        {
            Word = word;
            Direction = direction;
        }
    }
}
