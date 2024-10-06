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

                        Console.Clear();

                        //Foreach-loop som loopar igenom de filtrerade frågorna
                        foreach (var question in filteredQuestions)
                        {
                            Console.WriteLine(question.Text);
                            Console.WriteLine();
                            //For-loop som skriver ut svarsalternativen för frågorna
                            for (int i = 0; i < question.Answers.Length; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {question.Answers[i]}");
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
                        }
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