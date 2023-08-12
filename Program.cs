﻿internal class Program
{
    static int row = 3;
    static int col = 5;


    static int[] allPossibleInput = new int[9];


    static int[] userOinput = new int[5];
    static int[] userXinput = new int[5];
    static int O = 0;
    static int X = 0;

    static int[] allInput = new int[9];

    static int chooseSingleOrMulti;
    //All possible win conditions
    static int[,] winconditon = new[,]
    { {11,13,15},{21,23,25},{31,33,35},{11,21,31},
      {13,23,33},{15,25,35},{11,23,35 },{15,23,31}
        };
    
    private static void Main(string[] args)
    {
        int singleOrMultiPlayerChoose = AskSingleOrMultiPlayer();
        ShowSingleOrMultiPlayerModeOnTop();
        allPossibleInput = DrawPositionHitsBoard();

        for (int k = 1; k <= 9; k++)
        {

            if (k % 2 == 0)
            {
                if (singleOrMultiPlayerChoose == 1)
                {
                    int aiInput = GetAiInput();
                    Console.Clear();
                    Console.WriteLine("Ai Choose {0} for index to place X \n Press Enter to continue", aiInput);
                    userXinput[X] = aiInput;
                    X++;
                    Console.WriteLine();
                    DrawPlayingBoard();
                    Console.ReadLine();

                }
                else if (singleOrMultiPlayerChoose == 2)
                {
                    int userInput = GetUserInput("Enter the index where Player 2 want X", ref k);
                    userXinput[X] = userInput;
                    X++;

                }
            }
            else
            {
                int userInput = GetUserInput("Enter the index where Player 1 want 0", ref k);
                userOinput[O] = userInput;
                O++;
            }

            Console.Clear();
            ShowSingleOrMultiPlayerModeOnTop();
            DrawPositionHitsBoard();
            Console.WriteLine();
            DrawPlayingBoard();
            bool hasAnyOnewon = CheckingWin();

            if (hasAnyOnewon)
            {
                break;
            }
            if (k == 9)
            {
                Console.WriteLine("Draw");
            }
        }
        Console.WriteLine("Game Finished");
    }

    private static void ShowSingleOrMultiPlayerModeOnTop()
    {
        if (chooseSingleOrMulti == 1)
        {
            Console.WriteLine("Single Player");
            Console.WriteLine();
        }
        else if (chooseSingleOrMulti == 2)
        {
            Console.WriteLine("MultiPlayer");
            Console.WriteLine();
        }
    }

    private static int AskSingleOrMultiPlayer()
    {
        
        Console.WriteLine("Enter 1 for single Player and 2 for multi Player");
        if (int.TryParse(Console.ReadLine(), out chooseSingleOrMulti))
        {
            if (chooseSingleOrMulti == 1 || chooseSingleOrMulti == 2)
            {
                Console.Clear();
                return chooseSingleOrMulti;
            }
            else
            {
                Console.WriteLine("Player enter 1 or 2");
                return AskSingleOrMultiPlayer();
            }
        }
        else
        {
            Console.WriteLine("Player enter 1 or 2");
            return AskSingleOrMultiPlayer();
        }
    }

    private static int GetAiInput()
    {
        Random rand = new Random();
        int aiInput = allPossibleInput[rand.Next(0, 9)];
        for (int i = 0; i < allInput.Length; i++)
        {
            if (aiInput == allInput[i])
            {
                return GetAiInput();
            }

        }
        return aiInput;



    }

    private static void DrawPlayingBoard()
    {
        for (int i = 1; i <= row; i++)
        {
            for (int j = 1; j <= col; j++)
            {
                if (j % 2 == 0)
                {
                    Console.Write("|");
                    continue;
                }

                WriteOandXOnBoard(userXinput, 'X', X, i, j);
                WriteOandXOnBoard(userOinput, 'O', O, i, j);

                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    private static void WriteOandXOnBoard(int[] userInputsOfXOrO, char XorO, int XorOAmount, int activeRow, int activeCol)
    {
        for (int i = 0; i < XorOAmount; i++)
        {
            int rowGiven = (userInputsOfXOrO[i] / 10) % 10;
            int colGiven = userInputsOfXOrO[i] % 10;

            if (activeRow == rowGiven && activeCol == colGiven)
            {
                Console.Write(XorO);
                continue;
            }
        }

    }
    private static int[] DrawPositionHitsBoard()
    {

        int[] allPossibleInput = new int[9];
        int posibleInputVariable = 0;

        for (int i = 1; i <= row; i++)
        {
            for (int j = 1; j <= col; j++)
            {
                if (j % 2 == 0)
                {
                    Console.Write("|");
                    continue;
                }

                Console.Write("" + i + "" + (j));
                allPossibleInput[posibleInputVariable] = i * 10 + j;
                posibleInputVariable++;
            }
            Console.WriteLine();
        }

        return allPossibleInput;
    }
    private static int GetUserInput(string displayString, ref int k)
    {
        Console.WriteLine(displayString);
        if (int.TryParse(Console.ReadLine(), out int userInput))
        {
            bool userSameInput = IsUserInputInValid(userInput, k);
            if (userSameInput)
            {
                
                Console.WriteLine();
                Console.WriteLine("Place Already Taken or Invalid Input enter another one");
                Console.WriteLine();
                return GetUserInput(displayString,ref k); ;
            }

            return userInput;

        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Please Enter a valid index");
            Console.WriteLine();
            return GetUserInput(displayString, ref k);
        }



    }
    private static bool IsUserInputInValid(int userInput, int k)
    {
        if (IsUserInputPossible(userInput, allPossibleInput))
        {
            Console.WriteLine("valid Input");
            return IsUserInputAlreadyGiven(userInput, allInput, k);
        }
        return true;

    }

    private static bool IsUserInputAlreadyGiven(int userInput, int[] allInput, int k)
    {
        for (int j = 0; j < allInput.Length; j++)
        {
            if (allInput[j] == userInput)
            {
                Console.WriteLine("Already given");
                return true;

            }
        }
        allInput[k - 1] = userInput;
        return false;
    }
    private static bool IsUserInputPossible(int userInput, int[] allPossibleInput)
    {
        for (int i = 0; i < allPossibleInput.Length; i++)
        {
            if (allPossibleInput[i] == userInput)
            {
                return true;
            }
        }
        return false;
    }
    private static bool CheckingWin()
    {
        int[] xWithNotZero = new int[X];

        for (int g = 0; g < X; g++)
        {
            xWithNotZero[g] = userXinput[g];

        }

        int[] oWithNotZero = new int[O];

        for (int g = 0; g < O; g++)
        {
            oWithNotZero[g] = userOinput[g];

        }


        Array.Sort(oWithNotZero);
        Array.Sort(xWithNotZero);

        for (int g = 0; g < 8; g++)
        {
            int xwin = 0;
            int owin = 0;

            int[] winconditionRow = GetRow(winconditon, g);
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (xWithNotZero[i] == winconditionRow[j])
                    {
                        xwin++;
                    }

                }

            }

            if (xwin == 3)
            {
                Console.WriteLine("player With X win");
                return true;
            }

            for (int i = 0; i < O; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (oWithNotZero[i] == winconditionRow[j])
                    {
                        owin++;
                    }
                }

            }
            if (owin == 3)
            {
                Console.WriteLine("player With O win");
                return true;

            }
        }
        return false;
    }
    public static int[] GetRow(int[,] array, int row)
    {
        int[] result = new int[3];

        for (int j = 0; j < 3; j++)
        {
            int temp = array[row, j];
            result[j] = temp;
        }

        return result;
    }
}





