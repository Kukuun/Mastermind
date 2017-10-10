using System;

namespace MasterMind {
    public enum colors : int { Red = 0, Blue = 1, Green = 2, Yellow = 3, Error = 10000 }

    class Program {
        // Variables.
        private static int[] slaveMind = new int[4];
        private static int[] masterMind = new int[4];
        private static int[] lastTry = new int[4] { 10, 10, 10, 10 };
        private static int selectedColumn = 0;
        private static bool win = false;
        private static bool isTrying = false;
        private static int lives = 5;
        private static bool lost = false;
        
        // ConsoleKey inputs for loops.
        private static ConsoleKey GetKey() {
            return Console.ReadKey().Key;
        }
        
        // Main loop.
        static void Main(string[] args) {
            #region masterStart
            string[] inputAnswers = new string[4];
            bool running = true;

            UpdateMastermind();

            while (running) {
                switch (GetKey()) {
                    case ConsoleKey.UpArrow:
                        masterMind[selectedColumn]++;
                        CheckBordersMastermind();
                        UpdateMastermind();
                        break;
                    case ConsoleKey.DownArrow:
                        masterMind[selectedColumn]--;
                        CheckBordersMastermind();
                        UpdateMastermind();
                        break;
                    case ConsoleKey.LeftArrow:
                        selectedColumn--;
                        CheckBordersMastermind();
                        UpdateMastermind();
                        break;
                    case ConsoleKey.RightArrow:
                        selectedColumn++;
                        CheckBordersMastermind();
                        UpdateMastermind();
                        break;
                    case ConsoleKey.Enter:
                        running = false;
                        break;
                }
            }

            Console.WriteLine("\nYou chose the following color combination: ");

            for (int i = 0; i < 4; i++) {
                switch (masterMind[i]) {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("██");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("██");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("██");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("██");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;

                if (i != 3) {
                    Console.Write(" - ");
                }
            }

            Console.ReadKey();
            Console.Clear();
            #endregion
            #region slaveStart
            UpdateSlavemind();

            while (win == false && lost == false) {
                switch (GetKey()) {
                    case ConsoleKey.UpArrow:
                        slaveMind[selectedColumn]++;
                        CheckBordersSlavemind();
                        UpdateSlavemind();
                        isTrying = false;
                        break;
                    case ConsoleKey.DownArrow:
                        slaveMind[selectedColumn]--;
                        CheckBordersSlavemind();
                        UpdateSlavemind();
                        isTrying = false;
                        break;
                    case ConsoleKey.LeftArrow:
                        selectedColumn--;
                        CheckBordersSlavemind();
                        UpdateSlavemind();
                        isTrying = false;
                        break;
                    case ConsoleKey.RightArrow:
                        selectedColumn++;
                        CheckBordersSlavemind();
                        UpdateSlavemind();
                        isTrying = false;
                        break;
                    case ConsoleKey.Enter:
                        if (isTrying == false) {
                            for (int i = 0; i < 4; i++) {
                                lastTry[i] = slaveMind[i];
                            }
                            UpdateSlavemind();
                            CheckWin();
                            isTrying = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.ReadKey();
            return;
            #endregion
        }
        
        // Update for Mastermind.
        private static void UpdateMastermind() {
            Console.Clear();
            Console.WriteLine("Welcome to Mastermind");
            Console.WriteLine("\nChoose a combination among the colors.");
            Console.WriteLine("\nUse the Up, Down, Left and Right arrow keys to navigate\nbetween columns and colors.");
            Console.Write("\n|");

            for (int i = 0; i < 4; i++) {
                switch (masterMind[i]) {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("██");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("██");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("██");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("██");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;

                if (i != 3) {
                    Console.Write(" - ");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|\n");

            switch (selectedColumn) {
                case 0:
                    Console.WriteLine("|** -    -    -   |");
                    break;
                case 1:
                    Console.WriteLine("|   - ** -    -   |");
                    break;
                case 2:
                    Console.WriteLine("|   -    - ** -   |");
                    break;
                case 3:
                    Console.WriteLine("|   -    -    - **|");
                    break;
                default:
                    break;
            }
        }

        // Update for Slavemind.
        private static void UpdateSlavemind() {
            Console.Clear();
            Console.WriteLine("Welcome to Mastermind");
            Console.WriteLine("\nYour opponent have chosen four color combinations for you to guess");
            Console.WriteLine("\nUse the Up, Down, Left and Right arrow keys to navigate\nbetween columns and colors.");
            Console.Write("\n|");

            for (int i = 0; i < 4; i++) {
                switch (slaveMind[i]) {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("██");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("██");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("██");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("██");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;

                if (i != 3) {
                    Console.Write(" - ");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("| ");

            for (int i = 0; i < 4; i++) {
                if (lastTry[i] == masterMind[i]) {
                    Console.Write("[X]");
                }
                else {
                    Console.Write("[ ]");
                }

                if (i != 3) {
                    Console.Write(" ");
                }
            }

            Console.Write("\n");

            switch (selectedColumn) {
                case 0:
                    Console.WriteLine("|** -    -    -   |");
                    break;
                case 1:
                    Console.WriteLine("|   - ** -    -   |");
                    break;
                case 2:
                    Console.WriteLine("|   -    - ** -   |");
                    break;
                case 3:
                    Console.WriteLine("|   -    -    - **|");
                    break;
                default:
                    break;
            }

            Console.WriteLine("\nLives remaining: " + lives);
        }

        // Check if you've won.
        private static void CheckWin() {
            byte correctAmount = 0;

            for (int i = 0; i < 4; i++) {
                if (slaveMind[i] == masterMind[i]) {
                    correctAmount++;
                }
            }

            if (correctAmount >= 4) {
                Console.WriteLine("\nYou've won.");
                win = true;
            }
            else {
                Console.WriteLine("\nTry again.");
                lives--;

                if (lives <= 0) {
                    Console.Clear();
                    Console.WriteLine("You've lost the game.");
                    lost = true;
                }
            }
        }

        // Borders for Mastermind.
        private static void CheckBordersMastermind() {
            if (selectedColumn > 3) {
                selectedColumn = 3;
            }
            else if (selectedColumn < 0) {
                selectedColumn = 0;
            }

            if (masterMind[selectedColumn] > 3) {
                masterMind[selectedColumn] = 3;
            }
            else if (masterMind[selectedColumn] < 0) {
                masterMind[selectedColumn] = 0;
            }
        }

        // Borders for Slavemind.
        private static void CheckBordersSlavemind() {
            if (selectedColumn > 3) {
                selectedColumn = 3;
            }
            else if (selectedColumn < 0) {
                selectedColumn = 0;
            }

            if (slaveMind[selectedColumn] > 3) {
                slaveMind[selectedColumn] = 3;
            }
            else if (slaveMind[selectedColumn] < 0) {
                slaveMind[selectedColumn] = 0;
            }
        }
    }
}