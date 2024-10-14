using System.Text.Json;

namespace quiz
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
        public List<Question> getQuestion()
        {
            return questions;
        }

        public PlayerScore? RunQuiz()
        {
            string playerName = string.Empty;

            //While-loop som körs tills användaren skriver in ett giltigt användarnamn
            while (true)
            {
                Console.Clear(); //Konsollen rensas innan resultatet visas
                Console.WriteLine("Enter your name: ");

                //Variabler
                string? input2 = Console.ReadLine(); //Läser in användarens val
                playerName = input2?.Trim() ?? string.Empty; //Trimma anvndarens input, om input är null sätts värdet till en tom sträng

                if (!string.IsNullOrEmpty(playerName))
                {
                    //Gå ur loopen om namnet är giltigt
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Name cannot be empty");
                    Console.WriteLine("Press a key to continue");
                    Console.ReadKey();

                }
            }

            //Variabler
            string? difficulty;
            string? difficultyChoice;

            //While-loop som kontrollerar att val av svårighetsgrad är korrekt
            while (true)
            {
                Console.Clear(); //Konsollen rensas innan resultatet visas
                Console.WriteLine("Choose level of difficulty:\n");
                Console.WriteLine("[1] Easy");
                Console.WriteLine("[2] Medium");
                Console.WriteLine("[3] Hard");

                //Variabler
                difficultyChoice = Console.ReadLine(); //Läser in anändarens val

                //Switch-sats som bestämmer svårighetsgraden utifrån användarens val
                switch (difficultyChoice)
                {
                    case "1":
                        difficulty = "Easy";
                        break;
                    case "2":
                        difficulty = "Medium";
                        break;
                    case "3":
                        difficulty = "Hard";
                        break;
                    default:
                        Console.WriteLine("Error: Invalid choice");
                        Console.WriteLine("Press a key to continue");
                        Console.ReadKey();
                        //Gå tillbaka till början av loopen om valet är ogiltigt
                        continue;
                }
                //Bryt loopen om valet är giltigt
                break;
            }

            //Filtrerar frågor baserat på svårighetsgrad
            var filteredQuestions = getQuestion().FindAll(q => q.Difficulty == difficulty);

            //Ställ 10 frågor
            Random random = new Random();

            //Variabler
            int totalScore = 0; //Håller reda på poängen

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
                    return new PlayerScore(playerName, totalScore);
                }

                //If-sats som kontrollerar om svaret är korrekt
                if (question.CorrectAnswerIndex == answerIndex - 1)
                {
                    Console.WriteLine("Correct answer!");
                    //Lägg till poäng till totalScore
                    switch (difficultyChoice)
                    {
                        case "1":
                            totalScore += 1;
                            break;
                        case "2":
                            totalScore += 2;
                            break;
                        case "3":
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
            return new PlayerScore(playerName, totalScore);
        }
    }
}