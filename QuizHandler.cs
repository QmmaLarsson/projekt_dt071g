using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using quiz;

namespace Quiz
{
    public class QuizHandler
    {
        private string filename = @"questions.json";
        private List<Question> questions = new List<Question>();

        public QuizHandler()
        {
            if (File.Exists(filename) == true)
            { // If stored json data exists then read
                string jsonString = File.ReadAllText(filename);
                questions = JsonSerializer.Deserialize<List<Question>>(jsonString)!;
            }
        }
        public List<Question> getQuestion(){
            return questions;
        }
    }
}