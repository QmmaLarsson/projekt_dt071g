using Quiz;

namespace quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instans av klassen QuizHandler
            QuizHandler quizhandler = new QuizHandler();

            //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
            while (true)
            {
                Console.Clear();
                Console.WriteLine("E M M A S  Q U I Z\n");
                Console.WriteLine("[1] Start game");
                Console.WriteLine("[2] Information about the game\n");
                Console.WriteLine("[X] Close application");

                //Läser in användarens val
                string? input = Console.ReadLine();
                //Trim() tar bort eventuella mellanslag runt användarens inmatning
                //ToUpper() Säkerställer att både "X" och "x" är giltiga val
                //Om input är null sätts värdet till en tom sträng
                string choice = input?.Trim().ToUpper() ?? string.Empty;

                //Konsollen rensas innan resultatet visas
                //Console.Clear();
                //Switch-sats som hanterar användarens val
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter your name: ");
                        //Läser in användarens val
                        string? input2 = Console.ReadLine();
                        //Trimma anvndarens input, om input är null sätts värdet till en tom sträng
                        string playerName = input2?.Trim() ?? string.Empty;

                        Console.Clear();
                        Console.WriteLine("Choose level of difficulty:\n");
                        Console.WriteLine("[1] Easy");
                        Console.WriteLine("[2] Medium");
                        Console.WriteLine("[3] Hard");

                        //Läser in anändarens val
                        string? difficultyChoice = Console.ReadLine();
                        string? difficulty;
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
                                difficulty = "Error: Invalid choice";
                                break;
                        }

                        //Filtrerar frågor baserat på svårighetsgrad
                        var filteredQuestions = quizhandler.getQuestion().FindAll(q => q.Difficulty == difficulty);

                        //Ställ 10 frågor
                        Random random = new Random();

                        //Variabel för att hålla reda på poängen
                        int totalScore = 0;

                        //Foreach-loop som loopar igenom de filtrerade frågorna
                        for (int i = 0; i < 10 && filteredQuestions.Count > 0; i++)
                        {
                            int randomIndex = random.Next(filteredQuestions.Count);
                            var question = filteredQuestions[randomIndex];

                            Console.Clear();
                            Console.WriteLine(question.Text);
                            Console.WriteLine();
                            //For-loop som skriver ut svarsalternativen för frågorna
                            for (int j = 0; j < question.Answers.Length; j++)
                            {
                                Console.WriteLine($"[{j + 1}] {question.Answers[j]}");
                            }

                            //Läser in användarens svar
                            string? answerChoice = Console.ReadLine();

                            //If-sats som kontrollerar om svaret är giltigt
                            if (int.TryParse(answerChoice, out int answerIndex) && answerIndex > 0 && answerIndex <= question.Answers.Length)
                            {
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
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid choice");
                            }
                            Console.WriteLine("Press a key to continue to the next question");
                            Console.ReadKey();
                        }
                        Console.Clear();
                        Console.WriteLine("Quiz finished!");
                        Console.WriteLine($"Your total score was {totalScore} points!");
                        Console.WriteLine("Press a key to return to the startpage");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Information about the game");
                        break;

                    case "X":
                        return;

                    default:
                        Console.WriteLine("Error: Invalid choice");
                        break;

                }
                //Vänta med att rensa konsollen
                Console.WriteLine("");
                Console.WriteLine("Press a key to continue");
                Console.ReadKey();
            }
        }
    }
}