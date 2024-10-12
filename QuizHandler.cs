using Newtonsoft.Json;

namespace quiz;

public class QuizHandler
{
    private Random random = new Random();
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
            questions = JsonConvert.DeserializeObject<List<Question>>(jsonString)!;
        }
    }

    //Hämtar en lista med alla frågor
    private List<Question> GetQuestionsForDifficulty(DifficultyLevel difficultyLevel)
    {
        return questions.FindAll(q => q.Difficulty == difficultyLevel); ;
    }

    public int RunQuiz(DifficultyLevel difficultyLevel)
    {
        var filteredQuestions = GetQuestionsForDifficulty(difficultyLevel);

        //Variabler
        int totalScore = 0; //Håller reda på poängen

        //Ställ 10 frågor
        //Foreach-loop som loopar igenom de filtrerade frågorna
        for (int i = 0; i < 10 && filteredQuestions.Count > 0; i++)
        {
            int randomIndex = random.Next(filteredQuestions.Count);
            var question = filteredQuestions[randomIndex];

            //Variabler
            string? answerChoice;
            int answerIndex = -1;
            bool endQuiz = false; //Boolean som används för att bryta den yttre loopen

            //While-loop som körs till det att användaren anger ett giltigt svar
            while (true)
            {
                Console.Clear(); //Konsollen rensas innan resultatet visas
                Console.WriteLine(question.Text);
                Console.WriteLine();
                //For-loop som skriver ut svarsalternativen för frågorna
                for (int j = 0; j < question.Answers.Length; j++)
                {
                    Console.WriteLine($"[{j + 1}] {question.Answers[j]}");
                }

                Console.WriteLine("\n[X] End quiz");

                //Läser in användarens svar
                answerChoice = Console.ReadLine();

                //If-sats som kontrollerar om användaren vill avsluta quizet
                if (answerChoice.Trim().ToUpper() == "X")
                {
                    //Sätt endQuiz till true för att indikera att användaren villa avsluta quizet
                    endQuiz = true;
                    //Bryt loopen
                    break;
                }

                //If-sats som kontrollerar om svaret är giltigt
                if (int.TryParse(answerChoice, out answerIndex) && answerIndex > 0 && answerIndex <= question.Answers.Length)
                {
                    //Loopen bryts om svaret är giltigt
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Invalid choice");
                    Console.WriteLine("Press a key to continue");
                    Console.ReadKey();
                }
            }

            //If-sats som avbryter den yttre loopen om användaren valt detta
            if (endQuiz)
            {
                break;
            }

            //If-sats som kontrollerar om svaret är korrekt
            if (question.CorrectAnswerIndex == answerIndex - 1)
            {
                Console.WriteLine("Correct answer!");
                //Lägg till poäng till totalScore
                switch (difficultyLevel)
                {
                    case DifficultyLevel.Easy:
                        totalScore += 1;
                        break;
                    case DifficultyLevel.Medium:
                        totalScore += 2;
                        break;
                    case DifficultyLevel.Hard:
                        totalScore += 3;
                        break;
                    default:
                        totalScore += 0;
                        break;
                }
            }
            else
            {
                Console.WriteLine("Wrong answer!");
            }

            Console.WriteLine("Press a key to continue to the next question");
            Console.ReadKey();
        }

        return totalScore;
    }
}