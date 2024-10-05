using Quiz;

namespace quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            QuizHandler quizhandler = new QuizHandler();
            int i = 0;

            //While-loop som gör att programmet fortsätter köras till användaren väljer att avsluta programmet
            while (true)
            {
                Console.Clear(); //Rensa konsollen innan menyn skrivs ut
                Console.WriteLine("E M M A S  Q U I Z\n");
                Console.WriteLine("[1] Start Quiz");
                Console.WriteLine("[X] Close application");

                //Läser in användarens val
                //Trim() tar bort eventuella mellanslag runt användarens inmatning
                //ToUpper() Säkerställer att både "X" och "x" är giltiga val
                string choice = Console.ReadLine().Trim().ToUpper();

                //Konsollen rensas innan resultatet visas
                Console.Clear();
                //Switch-sats som hanterar användarens val
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Du har valt alternativ 1");

                        i = 0;
                        foreach (Question question in quizhandler.getQuestion())
                        { // List all cars in carstore
                            Console.WriteLine("[" + i++ + "] " + question.Text);
                        }

                        break;

                    case "2":
                        Console.WriteLine("Information om spelet");
                        return;

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