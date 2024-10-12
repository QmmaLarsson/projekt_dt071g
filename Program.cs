namespace quiz;

class Program
{
    static void Main(string[] args)
    {
        //Instans av klassen QuizHandler
        var quizhandler = new QuizHandler();
        var highscorehandler = new HighscoreHandler();

        //Variabler
        bool returnToHome = false;  //Boolean som används för att bryta den yttre loopen

        //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
        while (true)
        {
            TextHandler.IntroText();

            //Variabler
            string? input = Console.ReadLine(); //Läser in användarens val
            //Trim() tar bort eventuella mellanslag runt användarens inmatning
            //ToUpper() Säkerställer att både "X" och "x" är giltiga val
            //Om input är null sätts värdet till en tom sträng
            string choice = input?.Trim().ToUpper() ?? string.Empty;
            Console.Clear(); //Konsollen rensas innan resultatet visas

            //Switch-sats som hanterar användarens val
            switch (choice)
            {
                case "1":
                    
                    var playerName = GetPlayerName();

                    //Variabler
                    var difficulty = GetDifficulty();

                    var totalScore = quizhandler.RunQuiz(difficulty);
                    
                    Console.Clear(); //Konsollen rensas innan resultatet visas
                    Console.WriteLine("Quiz finished!");
                    Console.WriteLine($"Your total score was {totalScore} points!\n");

                    highscorehandler.AddPlayerScore(playerName, totalScore);

                    Console.WriteLine("Press a key to return to continue");
                    Console.ReadKey();
                    continue;

                case "2":
                    while (true)
                    {
                        Console.WriteLine("HIGHSCORE\n");

                        //Variabler
                        var topScores = highscorehandler.GetTopScores(5); //Hämta de fem bästa poängen

                        //If-sats som kontrollerar om det finns några sparade poäng
                        if (topScores.Count == 0)
                        {
                            Console.WriteLine("No scores available yet");
                        }
                        else
                        {
                            //For-loop som skriver ut de fem bästa poängen
                            for (int k = 0; k < topScores.Count; k++)
                            {
                                Console.WriteLine($"{k + 1}. {topScores[k].PlayerName}: {topScores[k].Score} points");
                            }
                        }

                        Console.WriteLine();

                        Console.WriteLine("[D] Delete highscore-list");
                        Console.WriteLine("[X] Return to the homepage");

                        //Variabler
                        string? actionChoice = Console.ReadLine(); //Läser in användarens svar

                        //If-sats som raderar highscore-listan
                        if (actionChoice?.Trim().ToUpper() == "D")
                        {
                            highscorehandler.DeleteAllScores();
                            Console.WriteLine("Highscore-list has been deleted");
                        }
                        else if (actionChoice?.Trim().ToUpper() == "X")
                        {
                            returnToHome = true;
                            //Loopen bryts om användaren väljer alternativ "X"
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid choice");
                            Console.WriteLine("Press a key to continue");
                            Console.ReadKey();
                            continue;
                        }
                    }
                    break;

                case "3":
                    TextHandler.InfoText();
                    break;

                case "X":
                    return;

                default:
                    Console.WriteLine("Error: Invalid choice");
                    Console.WriteLine("Press a key to continue");
                    Console.ReadKey();
                    TextHandler.IntroText();
                    break;

            }

            //If-sats som avbryter den yttre loopen om användaren valt detta
            if (returnToHome)
            {
                returnToHome = false;
                continue;
            }

            //Vänta med att rensa konsollen
            Console.WriteLine("");
            Console.WriteLine("Press a key to continue");
            Console.ReadKey();
        }
    }

    private static DifficultyLevel GetDifficulty()
    {
        //While-loop som kontrollerar att val av svårighetsgrad är korrekt
        while (true)
        {
            Console.Clear(); //Konsollen rensas innan resultatet visas
            Console.WriteLine("Choose level of difficulty:\n");
            Console.WriteLine("[1] Easy");
            Console.WriteLine("[2] Medium");
            Console.WriteLine("[3] Hard");

            //Variabler
            var difficultyChoice = Console.ReadLine(); //Läser in anändarens val

            //Switch-sats som bestämmer svårighetsgraden utifrån användarens val
            switch (difficultyChoice)
            {
                case "1":
                    return DifficultyLevel.Easy;
                case "2":
                    return  DifficultyLevel.Medium;
                case "3":
                    return DifficultyLevel.Hard;
                default:
                    Console.WriteLine("Error: Invalid choice");
                    Console.WriteLine("Press a key to continue");
                    Console.ReadKey();
                    //Gå tillbaka till början av loopen om valet är ogiltigt
                    continue;
            }
        }
    }

    private static string GetPlayerName()
    {
        string playerName;
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

        return playerName;
    }
}