using Quiz;

namespace quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instans av klassen QuizHandler
            QuizHandler quizhandler = new QuizHandler();
            HighscoreHandler highscorehandler = new HighscoreHandler();

            //Variabler
            bool returnToHome = false;  //Boolean som används för att bryta den yttre loopen

            //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
            while (true)
            {
                Console.Clear(); //Konsollen rensas innan resultatet visas
                Console.WriteLine(
@"····························································
:   __  __  _____     _____ _____    ___  _   _ ___ _____  :
:  |  \/  |/ _ \ \   / /_ _| ____|  / _ \| | | |_ _|__  /  :
:  | |\/| | | | \ \ / / | ||  _|   | | | | | | || |  / /   :
:  | |  | | |_| |\ V /  | || |___  | |_| | |_| || | / /_   :
:  |_|  |_|\___/  \_/  |___|_____|  \__\_\\___/|___/____|  :
····························································");
                Console.WriteLine();
                Console.WriteLine("[1] Start game");
                Console.WriteLine("[2] Highscore");
                Console.WriteLine("[3] Information about the game\n");
                Console.WriteLine("[X] Close application");

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
                        Console.Clear(); //Konsollen rensas innan resultatet visas
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
                        var filteredQuestions = quizhandler.getQuestion().FindAll(q => q.Difficulty == difficulty);

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
                                break;
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
                        Console.Clear(); //Konsollen rensas innan resultatet visas
                        Console.WriteLine("INFORMATION ABOUT THE GAME\n");
                        Console.WriteLine("This is a quiz game where you can test your knowledge of movies. You will face 10 questions that will put your movie knowledge to the ultimate challenge.\n");
                        Console.WriteLine("Start with choosing your difficulty, you can select from three levels of difficulty. You will earn points based on the difficulty level of the questions you answer correctly.");
                        Console.WriteLine("  - Easy (1 point per question)");
                        Console.WriteLine("  - Medium (2 points per question)");
                        Console.WriteLine("  - Hard (3 points per question)\n");
                        Console.WriteLine("At the end of the quiz, your total score will be displayed, and if you perform well, you might earn a spot on the highscore-list.\n");
                        Console.WriteLine("Are you ready to prove your film knowledge? Good luck!");
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