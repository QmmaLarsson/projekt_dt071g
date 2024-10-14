namespace quiz
{
    public static class TextHandler
    {
        public static void IntroText()
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
        }

        public static void InfoText()
        {
            Console.Clear(); //Konsollen rensas innan resultatet visas
            Console.WriteLine("INFORMATION ABOUT THE GAME\n");
            Console.WriteLine("This is a quiz game where you can test your knowledge of movies. You will face 10 questions that will put your movie knowledge to the ultimate challenge.\n");
            Console.WriteLine("Start with choosing your difficulty, you can select from three levels of difficulty. You will earn points based on the difficulty level of the questions you answer correctly.");
            Console.WriteLine("  - Easy (1 point per question)");
            Console.WriteLine("  - Medium (2 points per question)");
            Console.WriteLine("  - Hard (3 points per question)\n");
            Console.WriteLine("At the end of the quiz, your total score will be displayed, and if you perform well, you might earn a spot on the highscore-list.\n");
            Console.WriteLine("Are you ready to prove your film knowledge? Good luck!");
        }
    }
}