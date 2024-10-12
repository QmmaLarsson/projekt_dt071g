using System.Text.Json;

namespace quiz;

public class HighscoreHandler
{
    private string filename = @"highscore.json";
    private List<PlayerScore> highscore = new List<PlayerScore>();

    public HighscoreHandler()
    {
        //If-sats som kontrollerar om filen med spelare finns
        if (File.Exists(filename) == true)
        {
            //Om filen finns, läs hela filen som en JSON-sträng
            string jsonString = File.ReadAllText(filename);
            //Deserialisera JSON-strängen till en lista
            highscore = JsonSerializer.Deserialize<List<PlayerScore>>(jsonString)!;
        }
    }

    //Hämtar en lista med alla spelare
    public List<PlayerScore> GetPlayerScore()
    {
        return highscore;
    }

    //Lägger till en ny spelare samt poäng till listan och sparar den
    public void AddPlayerScore(string playerName, int score)
    {
        highscore.Add(new PlayerScore(playerName, score));
        SaveScores();
    }

    //Raderar hela highscore-listan
    public void DeleteAllScores()
    {
        highscore.Clear();
        SaveScores();
    }

    //Hämtar de bästa poängen, sorterade i fallande ordning
    public List<PlayerScore> GetTopScores(int count)
    {
        return highscore
        .OrderByDescending(ps => ps.Score) //Sortera poängen i fallande ordning
        .Take(count) //Ta de första poängen
        .ToList(); //Konvertera till en lista
    }

    //Sparar listan till JSON-filen
    public void SaveScores()
    {
        //Spara som som JSON-string
        string jsonString = JsonSerializer.Serialize(highscore);
        File.WriteAllText(filename, jsonString);
    }
}