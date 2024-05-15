using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Knight : Piece
    {
        // Field to track the maximum possible moves for the Knight
        int maxPossibleMovesNumber;

        // Constructor to initialize the knight with name, color, row, and column
        public Knight(string name, string color, int row, int col)
            : base("Night", color, row, col)
        {
            // Setting initial values and maximum possible moves
            this.maxPossibleMovesNumber = 8;
            allPossibleMoves = new String[maxPossibleMovesNumber];
            protectedPieces = new String[maxPossibleMovesNumber];
        }


        // Method to add a move to moves list or protected pieces list, based on the validity
        // and occupancy of the square
        public void addMove(int currRow, int currCol, Piece[,] theBoard, Piece basePiece,
            String[] moves, String[] protectedPieces)
        {
            int movesSize = ChessUtility.numOfCurrentMoves(moves);
            int protectedMovesSize = ChessUtility.numOfCurrentMoves(protectedPieces);

            // Check if the square is valid and occupied by a piece of the opposite color
            if (ChessUtility.squareIsValid(currRow, currCol))
            {
                Piece currPiece = theBoard[currRow, currCol];
                if (currPiece == null || currPiece.getColor() != basePiece.getColor())
                {
                    addPossibleMove(currRow, currCol, moves, movesSize);
                }
                else
                {
                    addPossibleMove(currRow, currCol, protectedPieces, protectedMovesSize);
                }
            }
        }


        // Method to find all possible moves for the knight in the current game position
        public override void searchForPossibleMoves(Piece basePiece, Piece[,] theBoard, String lastMove)
            {
            int row = basePiece.getRow();
            int col = basePiece.getCol();
            // Arrays to store possible moves and protected pieces
            String[] possibleMoves = new string[8];
            String[] protectedPieces = new String[8];

            // Add possible moves for the knight in all eight directions
            addMove(row + 2, col + 1, theBoard, basePiece, possibleMoves, protectedPieces); // top right
            addMove(row + 2, col - 1, theBoard, basePiece, possibleMoves, protectedPieces); // top left
            addMove(row + 1, col + 2, theBoard, basePiece, possibleMoves, protectedPieces); // right top
            addMove(row - 1, col + 2, theBoard, basePiece, possibleMoves, protectedPieces); // right bottom
            addMove(row + 1, col - 2, theBoard, basePiece, possibleMoves, protectedPieces); // left top
            addMove(row - 1, col - 2, theBoard, basePiece, possibleMoves, protectedPieces); // left bottom
            addMove(row - 2, col + 1, theBoard, basePiece, possibleMoves, protectedPieces); // bottom right
            addMove(row - 2, col - 1, theBoard, basePiece, possibleMoves, protectedPieces); // bottom left

            // Set possible moves and protected pieces
            basePiece.setAllPossibleMove(possibleMoves);
            basePiece.setProtectedPieces(protectedPieces);
        }
    }


}
