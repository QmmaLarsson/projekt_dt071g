using Quiz;

namespace quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instans av klassen QuizHandler
            QuizHandler quizhandler = new QuizHandler();
            int i = 0;

            //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
            while (true)
            {
                Console.Clear(); //Rensa konsollen innan menyn skrivs ut
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
                Console.Clear();
                //Switch-sats som hanterar användarens val
                switch (choice)
                {
                    case "1":
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

                        string? difficultyChoice = Console.ReadLine();

                        i = 0;
                        foreach (Question question in quizhandler.getQuestion())
                        { // List all cars in carstore
                            Console.WriteLine("[" + i++ + "] " + question.Text);
                        }

                        break;

                    case "2":
                        Console.WriteLine("Information about the game");
                        break;

                    case "X":
                        return;

                    default:
                        Console.WriteLine("Error: Invalid choice");
                        break;

                }
                //Vänta med att rensa konsollen
                Console.WriteLine("Press a key to continue");
                Console.ReadKey();
            }
        }
    }
}