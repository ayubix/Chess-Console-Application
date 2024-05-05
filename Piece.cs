using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Piece
    {
            // Fields
            protected string name;
            protected string color;
            protected int row;
            protected int col;
            protected String[] allPossibleMoves = [];
            protected int possibleMovesCount = 0;
            protected String[] protectedPieces = [];

            // Constructor to initialize the piece with name, color, row, and column
            public Piece(String name, String color, int row, int col)
            {
                this.name = name;
                this.color = color;
                this.row = row;
                this.col = col;
            }

            //Getters + Setters to all the fields
            public void setProtectedPieces(String[] protectedPieces)
            {
                this.protectedPieces = protectedPieces;
            }

            public String[] getProtectedPieces()
            {
                return protectedPieces;
            }

            public void setAllPossibleMove(String[] moves)
            {
                allPossibleMoves = moves;
            }

            public int getPossibleMovesCount()
            {
                return possibleMovesCount;
            }

            public void setPossibleMovesCount(int possibleMovesCount)
            {
                this.possibleMovesCount = possibleMovesCount;
            }

            public String[] getAllPossibleMoves()
            {
                return allPossibleMoves;
            }

            public String getName()
            {
                return name;
            }

            public String getColor()
            {
                return color;
            }
            public int getRow()
            {
                return row;
            }

            public int getCol()
            {
                return col;
            }

            public void setRow(int row)
            {
            this.row = row;
            }

            public void setCol(int col)
            {
                this.col = col;
            }


            // Virtual function , that each of derived classes will override as needed
            public virtual void searchForPossibleMoves(Piece basePiece, Piece[,] theBoard, String lastMove)
            {

            }

            // Adding a square to the piece moves list based on it status
            public bool addSquareToPossibleMoves(Piece[,] board, int squareRow, int squareColumn,
                String[] moves,int movesNumber, String[] protectedSquares, int protectedSquaresCount)
            {
                Piece currPiece = board[squareRow, squareColumn];
                // If the square is empty add move to list
                if (currPiece == null)
                {
                    addPossibleMove(squareRow, squareColumn, moves, movesNumber);

                }
                // If square is occupied by an opponent's piece, add it to list and stop checking this direction
                else if (currPiece.color != color)
                {
                    addPossibleMove(squareRow, squareColumn, moves, movesNumber);
                    return false;
                }
                else
                {
                    // If the square is occupied by a piece of the same color, add it to protected squares
                    addPossibleMove(squareRow, squareColumn, protectedSquares, protectedSquaresCount);
                    return false;
                }
                return true;
            }

           // Checks and adds optional rightward moves for the current chess piece
            public void rightMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row;
                int startCol = this.col + 1;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startCol < 8)
                {
                    if (addSquareToPossibleMoves(board, startRow, startCol, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startCol += 1;
                }
            }
            // Checks and adds optional leftward moves for the current chess piece
            public void leftMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row;
                int startColumn = this.col - 1;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startColumn >= 0)
                {
                    if (addSquareToPossibleMoves(board, startRow, startColumn, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startColumn -= 1;
                }
            }
            // Checks and adds optional upward moves for the current chess piece
            public void upMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row + 1;
                int startColumn = this.col;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startRow < 8)
                {
                    if (addSquareToPossibleMoves(board, startRow, startColumn, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startRow += 1;
                }
            }

            // Checks and adds optional downward moves for the current chess piece
            public void downMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row - 1;
                int startCol = this.col;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startRow >= 0)
                {
                    if (addSquareToPossibleMoves(board, startRow, startCol, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startRow -= 1;
                }
            }

            // Checks and adds optional top-right diagonal moves for the current chess piece
            public void upRightMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row + 1;
                int startColumn = this.col + 1;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startRow < 8 && startColumn < 8)
                {
                    if (addSquareToPossibleMoves(board, startRow, startColumn, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startRow += 1;
                    startColumn += 1;
                }
            }

            // Checks and adds optional top-left diagonal moves for the current chess piece
            public void upLeftMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row + 1;
                int startColumn = this.col - 1;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startRow < 8 && startColumn >= 0)
                {
                    if (addSquareToPossibleMoves(board, startRow, startColumn, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startRow += 1;
                    startColumn -= 1;
                }
            }

            // Checks and adds optional down-left diagonal moves for the current chess piece
            public void downLeftMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row - 1;
                int StartColumn = this.col - 1;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startRow >= 0 && StartColumn >= 0)
                {
                    if (addSquareToPossibleMoves(board, startRow, StartColumn, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startRow -= 1;
                    StartColumn -= 1;
                }

            }

            // Checks and adds optional down-right diagonal moves for the current chess piece
            public void downRightMove(Piece[,] board, String[] moves, String[] protectedSquares)
            {
                int startRow = this.row - 1;
                int startCol = this.col + 1;
                int movesNumber = ChessUtility.numOfCurrentMoves(moves);
                int protectedSquaresCount = ChessUtility.numOfCurrentMoves(protectedSquares);
                while (startRow >= 0 && startCol < 8)
                {
                    if (addSquareToPossibleMoves(board, startRow, startCol, moves, movesNumber, protectedSquares, protectedSquaresCount))
                        movesNumber++;
                    else
                        break;
                    startRow -= 1;
                    startCol += 1;
                }
            }

            // Convert a square position to String representation and add it to piece list
            public void addPossibleMove(int currRow, int currCol, String[] moves, int count)
            {
                String pos = "";
                char row = (char)(currRow + 48);
                char col = (char)(currCol + 48);
                pos += row;
                pos += col;
                moves[count] = pos;
            }
        }
    }

