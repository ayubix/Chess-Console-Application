using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class ChessUtility
    {
        // Method to count the number of non-null strings in an array
        public static int numberOfStrings(String[] stringsArray)
        {
            int numOfStrings = 0;
            foreach (var str in stringsArray)
            {
                if (str == null)
                    return numOfStrings;
                numOfStrings++;
            }
            return numOfStrings;
        }

        // Method to get the row index from the player move
        public static int getRowFromMove(String move, int rowIndex)
        {
            return (move[rowIndex] - '0') - 1;
        }

        // Method to get the column index from the player move
        public static int getColumnFromMove(String move, int columnIndex)
        {
            Char columnChar = move[columnIndex];
            columnChar = char.ToUpper(columnChar);
            return (columnChar - 17) - '0';
        }


        // Method to convert row and column indexes to a string representation of a square
        public static String convertSquareToString(int row, int column)
        {
            String pos = "";
            pos += row;
            pos += column;
            return pos;
        }

        // Method to check if a square is within the bounds of the chessboard
        public static bool squareIsValid(int row, int col)
        {
            if (row < 0 || col < 0 || row > 7 || col > 7)
                return false;
            return true;
        }

        // Method to count the number of non-null moves in an array
        public static int numOfCurrentMoves(String[] moves)
        {
            int count = 0;
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] != null)
                    count++;
            }
            return count;
        }

        // Method to convert a numerical file representation to a character representation
        public static char convertNumToFile(int file)
        {
            char startFile = 'a';
            while (file > 0)
            {
                startFile++;
                file--;
            }
            return startFile;
        }

        // Method to check if the length of a move string is valid
        public static bool isMoveLengthValid(String userMove)
        {
            if (userMove.Length != 4)
                return false;
            return true;
        }

        // Method to check if user input is empty
        public static bool isUserInputEmpty(String userInput)
        {
            if (userInput == "")
            {
                Console.WriteLine("Empty input, try again");
                return true;
            }
            return false;
        }

        // Check if the given column is valid
        public static bool validFileNumber(char input)
        {
            if (input >= 'A' && input <= 'H')
                return true;
            else if (input >= 'a' && input <= 'h')
                return true;
            return false;
        }

        // Check if the given rank is valid
        public static bool validRankNumber(char input) 
        {
            if (input >= '1' && input <= '8')
                return true;
            return false;
        }
    }
}

