using ChessApp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Queen : Piece
    {
        // Field to track the maximum possible moves for the queen
        int maxPossibleMovesNumber;

        // Constructor to initialize the queen with name, color, row, and column
        public Queen(string name, string color, int row, int col)
                    : base("Queen", color, row, col)
        {
            // Setting initial values and maximum possible moves
            this.maxPossibleMovesNumber = 27;
            allPossibleMoves = new string[maxPossibleMovesNumber];
            protectedPieces = new string[maxPossibleMovesNumber];
        }


        // Method to find all possible moves for the queen in the current game position
        public override void searchForPossibleMoves(Piece basePiece, Piece[,] theBoard, String lastMove)
        {
            // Arrays to store possible moves and protected pieces
            string[] possibleMoves = new string[27];
            string[] protectedPieces = new string[27];

            // Calling move methods for all directions
            basePiece.rightMove(theBoard, possibleMoves, protectedPieces);
            basePiece.leftMove(theBoard, possibleMoves, protectedPieces);
            basePiece.upMove(theBoard, possibleMoves, protectedPieces);
            basePiece.downMove(theBoard, possibleMoves, protectedPieces);
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
