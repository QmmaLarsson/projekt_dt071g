using System.Text.Json;
using quiz;

namespace Quiz
{
    public class QuizHandler
    {
        private string filename = @"questions.json";
        //Lista där alla frågor lagras
        private List<Question> questions = new List<Question>();

        public QuizHandler()
        {
            //If-sats som kontrollerar om filen med frågor finns
            if (File.Exists(filename) == true)
            { 
                //Om filen finns, läs hela filen som en JSON-sträng
                string jsonString = File.ReadAllText(filename);
                //Deserialisera JSON-strängen till en lista
                questions = JsonSerializer.Deserialize<List<Question>>(jsonString)!;
            }
        }

        //Hämtar en lista med alla frågor
        public List<Question> getQuestion(){
            return questions;
        }
    }
}