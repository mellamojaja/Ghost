namespace Ghost.AI.API.Models
{
    public class GameStateModel
    {
        public string Word { get; set; }

        public GameStateModel (string word)
        {
            Word = word;
        }
    }
}