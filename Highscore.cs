namespace quiz
{
    public class PlayerScore
    {
        public string? PlayerName { get; set; }
        public int Score { get; set; }
        //Konstruktor för deserialisering
        public PlayerScore() { }
        //Konstruktor för att skapa ett nytt objekt
        public PlayerScore(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }
}