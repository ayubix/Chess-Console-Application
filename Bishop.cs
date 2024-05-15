using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Bishop : Piece
    {

        // Field to track the maximum possible moves for the bishop
        int maxPossibleMovesNumber;

        // Constructor to initialize the bishop with name, color, row, and column
        public Bishop(string name, string color, int row, int col)
            : base("Bishop", color, row, col)
        {
            // Setting initial values and maximum possible moves
            this.maxPossibleMovesNumber = 13;
            allPossibleMoves = new string[maxPossibleMovesNumber];
            protectedPieces = new string[maxPossibleMovesNumber];
        }

        // Method to find all possible moves for the bishop in the current game position
        public override void searchForPossibleMoves(Piece basePiece, Piece[,] theBoard, string lastMove)
        {
            // Arrays to store possible moves and protected pieces
            string[] possibleMoves = new string[13];
            string[] protectedPieces = new string[13];

            // Calling move methods for diagonal directions
            basePiece.downLeftMove(theBoard, possibleMoves, protectedPieces);
            basePiece.downRightMove(theBoard, possibleMoves, protectedPieces);
            basePiece.upLeftMove(theBoard, possibleMoves, protectedPieces);
            basePiece.upRightMove(theBoard, possibleMoves, protectedPieces);

            // Setting the found moves and protected pieces
            basePiece.setAllPossibleMove(possibleMoves);
            basePiece.setProtectedPieces(protectedPieces);
        }
    }
}
