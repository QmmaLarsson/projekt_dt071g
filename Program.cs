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

            //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
            while (true)
            {
                Console.Clear();
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
                        string playerName;

                        //While-loop som körs tills användaren skriver in ett giltigt användarnamn
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Enter your name: ");
                            //Läser in användarens val
                            string? input2 = Console.ReadLine();
                            //Trimma anvndarens input, om input är null sätts värdet till en tom sträng
                            playerName = input2?.Trim() ?? string.Empty;

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

                        string? difficulty;
                        string? difficultyChoice;

                        //While-loop som kontrollerar att val av svårighetsgrad är korrekt
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Choose level of difficulty:\n");
                            Console.WriteLine("[1] Easy");
                            Console.WriteLine("[2] Medium");
                            Console.WriteLine("[3] Hard");

                            //Läser in anändarens val
                            difficultyChoice = Console.ReadLine();

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

                        //Variabel för att hålla reda på poängen
                        int totalScore = 0;

                        //Foreach-loop som loopar igenom de filtrerade frågorna
                        for (int i = 0; i < 10 && filteredQuestions.Count > 0; i++)
                        {
                            int randomIndex = random.Next(filteredQuestions.Count);
                            var question = filteredQuestions[randomIndex];

                            string? answerChoice;
                            int answerIndex;

                            //While-loop som körs till det att användaren anger ett giltigt svar
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine(question.Text);
                                Console.WriteLine();
                                //For-loop som skriver ut svarsalternativen för frågorna
                                for (int j = 0; j < question.Answers.Length; j++)
                                {
                                    Console.WriteLine($"[{j + 1}] {question.Answers[j]}");
                                }

                                //Läser in användarens svar
                                answerChoice = Console.ReadLine();

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
                        Console.Clear();
                        Console.WriteLine("Quiz finished!");
                        Console.WriteLine($"Your total score was {totalScore} points!");

                        highscorehandler.AddPlayerScore(playerName, totalScore);

                        Console.WriteLine("Press a key to return to the startpage");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("HIGHSCORE\n");
                        //Hämta de fem bästa poängen
                        var topScores = highscorehandler.GetTopScores(5);

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

                        string? deleteChoice = Console.ReadLine();
                        if(deleteChoice?.Trim().ToUpper() == "D")
                        {
                            highscorehandler.DeleteAllScores();
                            Console.WriteLine("Highscore-list has been deleted");
                        }

                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("INFORMATON ABOUT THE GAME\n");
                        Console.WriteLine("This is a quiz game where you can test your knowledge of movies. You will face 10 questions that will put your movie knowledge to the ultimate challenge.\n");
                        Console.WriteLine("HOW TO PLAY");
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
                //Vänta med att rensa konsollen
                Console.WriteLine("");
                Console.WriteLine("Press a key to continue");
                Console.ReadKey();
            }
        }
    }
}