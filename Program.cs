namespace quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instans av klasser
            QuizHandler quizhandler = new QuizHandler();
            HighscoreHandler highscorehandler = new HighscoreHandler();

            //Variabler
            bool returnToHome = false;  //Boolean som används för att bryta den yttre loopen

            //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
            while (true)
            {
                //Läser in texten på startsidan
                TextHandler.IntroText();

                //Variabler
                string? input = Console.ReadLine(); //Läser in användarens val
                //Trim() tar bort eventuella mellanslag runt användarens inmatning
                //ToUpper() Säkerställer att både "X" och "x" är giltiga val
                //Om input är null sätts värdet till en tom sträng
                string choice = input?.Trim().ToUpper() ?? string.Empty;

                //Switch-sats som hanterar användarens val
                switch (choice)
                {
                    case "1":
                        Console.Clear(); //Konsollen rensas innan resultatet 
                        PlayerScore? result = quizhandler.RunQuiz(); //Kör quizet och spara resultatet i variabeln result

                        Console.Clear(); //Konsollen rensas innan resultatet visas
                        Console.WriteLine("Quiz finished!");
                        Console.WriteLine($"Your total score was {result.Score} points!\n");

                        highscorehandler.AddPlayerScore(result.PlayerName, result.Score);

                        Console.WriteLine("Press a key to return to continue");
                        Console.ReadKey();
                        continue;

                    case "2":
                        while (true)
                        {
                            Console.Clear(); //Konsollen rensas innan resultatet visas
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
                        //Läser in informationstext
                        TextHandler.InfoText();
                        break;

                    case "X":
                        return;

                    default:
                        Console.WriteLine("Error: Invalid choice");
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
    }
}