using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Pawn : Piece
    {
            // Fields
            int maxPossibleMovesNumber;
            bool startSquare;
            String[] captureMoves;
            bool enPassantRightLegal;
            bool enPassantLeftLegal;

            // Constructor to initialize the pawn with name, color, row, and column
            public Pawn(String name, String color, int row, int col)
                : base("Pawn", color, row, col)
            {

                // Setting initial values and maximum possible moves
                startSquare = true;
                this.maxPossibleMovesNumber = 4;
                allPossibleMoves = new string[maxPossibleMovesNumber];
                captureMoves = new string[2];
                protectedPieces = new String[maxPossibleMovesNumber];
                enPassantRightLegal = false;
                enPassantLeftLegal = false;
            }
            

            // Getters for enPassant fields
            public bool getEnPassantLeft()
            {
                return enPassantLeftLegal;
            }
            public bool getEnPassantRight()
            {
                return enPassantRightLegal;
            }
            // Getters + Setters for captures list field
            public String[] getCaptureMoves()
            {
                return captureMoves;
            }
            public void setCaptureMoves(String[] moves)
            {
                this.captureMoves = moves;
            }
            // Getters + Setters for starting squre field
            public bool getStartSquare()
            {
                return startSquare;
            }
            public void setStartSquare(bool startSquare)
            {
                this.startSquare = startSquare;
            }

            // Method to check if en passant capture is legal
            public bool isEnPassantEventLegal(Piece[,] board, int capturedColumn, String lastMove) 
            {
                // Check if the pawn is on the correct row for en Passant capture
                if ((color == "White" && row != 4) || (color == "Black" && row != 3)) 
                    return false;

                Piece currPiece = board[row, capturedColumn];
                //  Ensure there is a pawn on the adjacent column
                if (currPiece is not Pawn)  
                return false;

                // Ensure that the adjacent pawn is of the opponent
                if (currPiece.getColor() == color)
                    return false;

                int direction = (color == "White") ? 2 : -2;
                int baseRow, baseColumn, targetRow, targetColumn;
                baseRow = ChessUtility.getRowFromMove(lastMove, 1);
                baseColumn = ChessUtility.getColumnFromMove(lastMove, 0);
                targetRow = ChessUtility.getRowFromMove(lastMove, 3);
                targetColumn = ChessUtility.getColumnFromMove(lastMove, 2);

                // Check if the conditions for en passant are met
                if ((baseRow != row + direction) || (baseColumn != capturedColumn))
                    return false;
                else if ((targetRow != row) || (targetColumn != capturedColumn))
                    return false;
                return true;
            }

            // Method to find all possible moves for the pawn in the current game position
            public override void searchForPossibleMoves(Piece basePiece,Piece[,] theBoard, String lastMove)
            {

                int row = basePiece.getRow();
                int col = basePiece.getCol();
                // Arrays to store possible moves and protected pieces
                String[] possibleMoves = new string[4];
                String[] capturesMoves = new string[2];
                String[] protectedSquares = new string[2];
                int possibleMovesSize = 0, capturesCount = 0, protectedCount = 0;
                int moveDirection = basePiece.getColor() == "White" ? 1 : -1;
                // Check if pawn can move forward
                if (row + moveDirection < 8 && row + moveDirection >= 0)
                {
                    pawnForwardMove(theBoard, moveDirection, possibleMoves, possibleMovesSize);
                    possibleMovesSize = ChessUtility.numberOfStrings(possibleMoves);
                    if (col + 1 < 8) // capture to the right
                    {
                        pawnDiagonalCaptures(true, theBoard, basePiece, moveDirection, possibleMoves, capturesMoves, protectedSquares,
                            capturesCount, possibleMovesSize, protectedCount, lastMove); 
                        possibleMovesSize = ChessUtility.numberOfStrings(possibleMoves);
                        capturesCount = ChessUtility.numberOfStrings(capturesMoves);
                        protectedCount = ChessUtility.numberOfStrings(protectedSquares);
                    }
                    if (col - 1 >= 0) // capture to the left
                    {
                        pawnDiagonalCaptures(false, theBoard, basePiece, moveDirection, possibleMoves, capturesMoves, protectedSquares,
                            capturesCount, possibleMovesSize, protectedCount, lastMove); //Pawn captures to the left side
                        possibleMovesSize = ChessUtility.numberOfStrings(possibleMoves);
                        capturesCount = ChessUtility.numberOfStrings(capturesMoves);
                        protectedCount = ChessUtility.numberOfStrings(protectedSquares);
                    }
                }
                // Setting the found moves and protected pieces
                basePiece.setAllPossibleMove(possibleMoves);
                captureMoves = capturesMoves;
                basePiece.setProtectedPieces(protectedSquares);
            }

            // Check which forward moves are allowed to the pawn
            public void pawnForwardMove(Piece[,] theBoard, int moveDirection, String[] possibleMoves,
                int possibleMovesCounter)
            {
                // Check if one square forward is empty 
                if (theBoard[row + moveDirection, col] == null) 
                {
                    addPossibleMove(row + moveDirection, col, possibleMoves, possibleMovesCounter);
                    possibleMovesCounter++;
                    if (row + (moveDirection * 2) < 8 && startSquare)
                    {

                    // Check if two squares forward  is empty , on the first time of the pawn
                    if (theBoard[row + (moveDirection * 2), col] == null) 
                        {
                            addPossibleMove(row + (moveDirection * 2), col, possibleMoves, possibleMovesCounter);
                            possibleMovesCounter++;
                        }
                    }
                }

            }



            // Method for pawn diagonal captures
            public void pawnDiagonalCaptures(bool capturesToTheRight, Piece[,] theBoard, Piece basePiece,
                int pawnMoveDirection,String[] possibleMoves, String[] captures, String[] protectedSquares,
                int captureCount, int possibleMovesCounter, int protectedCount, String lastMove)
            {
                int columnStepDirection = (capturesToTheRight == true) ? 1 : -1;

                // If there is an opponent's piece diagonally ahead, it can be captured
                if (theBoard[row + pawnMoveDirection, col + columnStepDirection] != null &&
                    theBoard[row + pawnMoveDirection, col + columnStepDirection].getColor() != basePiece.getColor())
                {
                    // Adding all relevant moves to captured list
                    addPossibleMove(row + pawnMoveDirection, col + columnStepDirection, possibleMoves, possibleMovesCounter);
                    addPossibleMove(row + pawnMoveDirection, col + columnStepDirection, captures, captureCount);
                    possibleMovesCounter++;
                    captureCount++;
                }
                else
                {
                    // Adding all relevant moves to protected squares list
                     addPossibleMove(row + pawnMoveDirection, col + columnStepDirection, protectedSquares, protectedCount);
                    protectedCount++;
                }


                // Check for en passant captures
                if (isEnPassantEventLegal(theBoard, col + columnStepDirection, lastMove))
                {
                    if (capturesToTheRight == true)
                        enPassantRightLegal = true;
                    else
                        enPassantLeftLegal = true;
                    addPossibleMove(row + pawnMoveDirection, col + columnStepDirection, possibleMoves, possibleMovesCounter);
                    possibleMovesCounter++;
                }
                else
                {
                    if (capturesToTheRight == true)
                        enPassantRightLegal = false;
                    else
                        enPassantLeftLegal = false;
                }
            }
        }
    }

