using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessApp
{
    internal class Game
    {
            // Fields
            Piece[,] board;
            int boardRows;
            int boardCols;
            private String currentPlayer;
            private StatusGame gameStatus;
            private bool isCheck;
            private bool isMate;
            private bool drawOffer;
            private bool lastMoveWasPlayed;
            private Piece whiteKing;
            private Piece blackKing;
            private String lastMove;
            private int gameMovesNumber;
            private String[] positionsHistory;
            private int positionsHistoryCounter;
            private int[] boardsRepetitionsCounter;
            
            // Constructor
            public Game()
            {
                // Setting initial values to all game fields 
                boardRows = 8;
                boardCols = 8;
                board = new Piece[boardRows, boardCols];
                positionsHistory = new string[100];
                boardsRepetitionsCounter = new int[100];
                currentPlayer = "White";
                gameStatus = StatusGame.InProgress;
                isCheck = false;
                isMate = false;
                drawOffer = false;
                lastMoveWasPlayed = true;
                lastMove = "";
                gameMovesNumber = 0;
                positionsHistoryCounter = 0;
                settingInitialBoard();
                whiteKing = getKingFromBoard("White");
                blackKing = getKingFromBoard("Black");
            }

            // Setting the starting board position
            public void settingInitialBoard()
            {
                addPieces(new King("King", "White", 0, 4));
                addPieces(new Bishop("Bishop", "Black", 7, 5));
                addPieces(new Bishop("Bishop", "White", 0, 5));
                addPieces(new King("King", "Black", 7, 4));
                addPieces(new Rook("Rook", "White", 0, 0));
                addPieces(new Knight("Knight", "White", 0, 1));
                addPieces(new Bishop("Bishop", "White", 0, 2));
                addPieces(new Queen("Queen", "White", 0, 3));
                addPieces(new King("King", "White", 0, 4));
                addPieces(new Bishop("Bishop", "White", 0, 5));
                addPieces(new Knight("Knight", "White", 0, 6));
                addPieces(new Rook("Rook", "White", 0, 7));
                addPieces(new Pawn("Pawn", "White", 1, 0));
                addPieces(new Pawn("Pawn", "White", 1, 1));
                addPieces(new Pawn("Pawn", "White", 1, 2));
                addPieces(new Pawn("Pawn", "White", 1, 3));
                addPieces(new Pawn("Pawn", "White", 1, 4));
                addPieces(new Pawn("Pawn", "White", 1, 5));
                addPieces(new Pawn("Pawn", "White", 1, 6));
                addPieces(new Pawn("Pawn", "White", 1, 7));
                addPieces(new Rook("Rook", "Black", 7, 0));
                addPieces(new Knight("Knight", "Black", 7, 1));
                addPieces(new Bishop("Bishop", "Black", 7, 2));
                addPieces(new Queen("Queen", "Black", 7, 3));
                addPieces(new King("King", "Black", 7, 4));
                addPieces(new Bishop("Bishop", "Black", 7, 5));
                addPieces(new Knight("Knight", "Black", 7, 6));
                addPieces(new Rook("Rook", "Black", 7, 7));
                addPieces(new Pawn("Pawn", "Black", 6, 0));
                addPieces(new Pawn("Pawn", "Black", 6, 1));
                addPieces(new Pawn("Pawn", "Black", 6, 2));
                addPieces(new Pawn("Pawn", "Black", 6, 3));
                addPieces(new Pawn("Pawn", "Black", 6, 4));
                addPieces(new Pawn("Pawn", "Black", 6, 5));
                addPieces(new Pawn("Pawn", "Black", 6, 6));
                addPieces(new Pawn("Pawn", "Black", 6, 7));
            }
            
            // Getters + Setters to fields
            public Piece[,] getBoard()
            {
                return board;
            }
            
            public void setGameMovesCount(int num)
            {
                 this.gameMovesNumber = num;
            }

            public int getGameMovesCount()
            {
                return this.gameMovesNumber;
            }

            public String getLastMove()
            {
                return lastMove;
            }

            public void setLastMove(String lastMove)
            {
                this.lastMove = lastMove;
            }

            // Add piece to game board
            public void addPieces(Piece addedPiece)
            {
                board[addedPiece.getRow(), addedPiece.getCol()] = addedPiece;
            }

            // check if the current game position is already existed 
            public int comparePositions(String currentPos, int positionsCounter)
            {
                for (int i = 0; i < positionsCounter; i++)
                {
                    if (currentPos.Equals(positionsHistory[i]))
                    {
                        return i;
                    }
                }
                return -1;
            }

            // Retrieve the king position of given player from b
            public Piece getKingFromBoard(String color)
            {
                for (int i = 0; i < boardRows; i++)
                {
                    for (int j = 0; j < boardCols; j++)
                    {
                        Piece currPiece = board[i, j];
                        if (currPiece != null && currPiece.getColor() == color && currPiece.getName() == "King")
                        {
                            return currPiece;
                        }
                    }
                }
                return null;
            }
            
            // Convert the game board to String representation for threefold draw checks
            public String boardToString()
            {
                String boardStr = "";
                boardStr += currentPlayer;
                boardStr += " _: ";
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Piece currPiece = board[i, j];
                        if (currPiece == null)
                            boardStr += "-";
                        else
                        {
                            boardStr += currPiece.getColor()[0];
                            boardStr += currPiece.getName()[0];
                        }
                    }
                }
                boardStr += "_Castling_";
                boardStr += rightToCastle("White");
                boardStr += rightToCastle("Black");
                boardStr += "EnPassant_";
                boardStr += addOptionalEnPassant("White");
                boardStr += addOptionalEnPassant("Black");
                return boardStr;
            }
            

            // Check if the given Rook is already moved
            public bool isRookMovedAlready(Piece rook, String currPlayer)
            {
                if (rook != null)
                {
                    if (rook is Rook && rook.getColor() == currPlayer && ((Rook)rook).getFirstMove() == true)
                        return true;
                }
                return false;
            }


            // Return which castling moves are optional for current player in string format
            public String rightToCastle(String currPlayer)
            {
                String castlingStr = "";
                castlingStr += currPlayer + "_";
                int rookRow = (currPlayer == "White") ? 0 : 7;
                Piece theKing = getKingFromBoard(currPlayer);
                if (((King)theKing).getFirstMove() == false)
                    return castlingStr;
                else
                {
                    Piece queenSideRook = board[rookRow, 0];
                    if (isRookMovedAlready(queenSideRook, currPlayer))
                        castlingStr += "Long";
                    Piece kingSideRook = board[rookRow, 7];
                    if (isRookMovedAlready(kingSideRook, currPlayer))
                        castlingStr += "Short";
                }
                return castlingStr;
            }


            // Update for each of the current pieces their possible moves
            public void updatePiecesPossibleMoves(Piece[,] theBoard, String lastMove) 
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Piece p = theBoard[i, j];
                        if (p != null && p.getName() != "King")
                        {
                            p.searchForPossibleMoves(p, theBoard, lastMove);
                        }
                    }
                }
            }


            // Check if player selected a valid square - not the opponent's one or empty square 
            public bool validBaseSquare(String move)   
            {
                int baseColumn = ChessUtility.getColumnFromMove(move, 0);
                int baseRow = ChessUtility.getRowFromMove(move, 1);
                Piece selectedPiece = getBoard()[baseRow, baseColumn];
                if (selectedPiece == null)
                    return false;
                else if (selectedPiece.getColor() != currentPlayer)
                    return false;
                return true;
            }

            // Check if the player target square is not occupied by his pieces
            public bool validTargetSquare(String move) 
            {

                int targetColumn = ChessUtility.getColumnFromMove(move, 2);
                int targetRow = ChessUtility.getRowFromMove(move, 3);
                Piece selectedPiece = getBoard()[targetRow, targetColumn];
                if (selectedPiece != null)
                {
                    // Can not capture the enemy king
                    if (selectedPiece.getColor() == currentPlayer || selectedPiece.getName() == "King")
                        return false;
                }
                return true;
            }

            // Check whether player typed the same square twice(as base cell and target cell)
            public bool isUserTypeTheSameSquare(String userInput)
            {
                if (userInput[0] == userInput[2] && userInput[1] == userInput[3]) 
                    return false;
                return true;
            }

        // Get the requested move from user
        public String getUserInput()
            {
                String userMsg = (drawOffer == true) ? " player type draw to accept the offer, " +
                "or any legal move to refuse" : " player please enter a move or ask for a draw :";
                Console.WriteLine(currentPlayer + userMsg);
                String userInput = Console.ReadLine();
                return userInput;
            }
           
            // Check if user asked or agreed for draw instead of typing regular chess move
            public bool userInputIsDraw(String userMove)
            {
                if (userMove != "draw" && userMove != "DRAW")
                    return false;
                return true;
            }

            // Check if all input characters are valid 
            public bool isAllMoveCharactersValid(String userMove) 
            {
                if (!ChessUtility.validFileNumber(userMove[0]) || !ChessUtility.validFileNumber(userMove[2]) ||
                        !ChessUtility.validRankNumber(userMove[1]) || !ChessUtility.validRankNumber(userMove[3]))
                    return false;
                return true;
            }

            
           
            public String validMoveFormat() // function validate the user move format
            {
                bool validMove = false;
                String userMove = "";
                String userInput = "";
                do
                {
                    userInput = getUserInput();
                    if (ChessUtility.isUserInputEmpty(userInput))
                        continue;
                    userMove = userInput.Trim();
                    if (!ChessUtility.isMoveLengthValid(userMove))
                        Console.WriteLine("The input has to be 4 characters length , try again");
                    else if (userInputIsDraw(userMove))
                    {
                        return userMove;
                    }
                    else if (!isAllMoveCharactersValid(userMove))
                    {
                        Console.WriteLine("The input contains illegal characters , try again");
                    }
                    else if (!isUserTypeTheSameSquare(userMove))
                    {
                        Console.WriteLine("The input contains the same cell twice , try again");
                    }
                    else
                        validMove = true;
                } while (!validMove);
                return userMove;
            }
            public bool isThreeFoldDrawOccurred()
            {
                String currentPosition = boardToString();
                int comparisonIndex = comparePositions(currentPosition, positionsHistoryCounter);
                if (comparisonIndex != -1)
                {
                    boardsRepetitionsCounter[comparisonIndex]++;
                    if (boardsRepetitionsCounter[comparisonIndex] == 2)
                        return true;
                }
                else
                {
                    positionsHistory[positionsHistoryCounter] = currentPosition;
                    positionsHistoryCounter++;
                }
                return false;
            }
            public void drawGameEvents(String typeOfDraw) //  
            {
                gameStatus = StatusGame.Draw;
                printBoard();
                if (typeOfDraw == "ThreeFold")
                    Console.WriteLine("The game is draw , draw by repetition");
                else if (typeOfDraw == "Stalemate")
                    Console.WriteLine("The game is draw , stalemate position");
                else if (typeOfDraw == "Dead position")
                    Console.WriteLine("The game is draw , Dead position case");
                else if (typeOfDraw == "Offer")
                    Console.WriteLine("Draw offer accepted by player " + currentPlayer); // draw by offer
                else
                    Console.WriteLine("You reached 50 moves without any pawn move or capturing");
                Console.WriteLine("The game result is a draw");
            }
            public bool drawOfferScenario()
            {
                if (drawOffer)
                {
                    drawGameEvents("Offer");
                    return true;
                }
                else
                {
                    Console.WriteLine("Draw offer sent by player " + currentPlayer);
                    drawOffer = true;
                }
                return false;
            }
            public void refuseDrawOfferScenario()
            {
                if (drawOffer)
                {
                    Console.WriteLine("Draw offer refused by player " + currentPlayer + " the turn is going back to second player");
                    lastMoveWasPlayed = false;
                }
                else
                    lastMoveWasPlayed = true;
                drawOffer = false;
            }
            public void updateAllPiecesMoves(String lastMove)
            {
                updatePiecesPossibleMoves(board, lastMove);
                whiteKing.searchForPossibleMoves(whiteKing, board, "");
                blackKing.searchForPossibleMoves(blackKing, board, "");
            }
            public bool staleMateScenario(String currentPlayer)
            {
                if (isCheck == false)
                {
                    if (playerHasAnyLegalMove(board, currentPlayer))
                    {
                        drawGameEvents("Stalemate");
                        return true;
                    }
                }
                return false;
            }
            public bool fiftyMovesScenario()
            {
                if (gameMovesNumber == 100)
                {
                    drawGameEvents("50");
                    return true;
                }
                return false;
            }
            public bool deadPositionScenario()
            {
                if (deadPositionDraw("King") || deadPositionDraw("Bishop") || deadPositionDraw("Night"))
                {
                    drawGameEvents("Dead position");
                    return true;
                }
                return false;
            }
            public bool checkScenario(Piece opponentKing, String opponentPlayer)
            {
                if (searchForCheck(board, opponentKing))
                {
                    Console.WriteLine(opponentPlayer + " player is in check");
                    isCheck = true;
                    return true;
                }
                return false;
            }
            public bool mateScenario(String opponentPlayer)
            {
                if (playerHasAnyLegalMove(board, opponentPlayer))
                {
                    isMate = true;
                    gameStatus = StatusGame.Win;
                    printBoard();
                    Console.WriteLine("Mate for " + currentPlayer + " player");
                    return true;
                }
                return false;
            }
            public Piece getOpponentKing()
            {
                return (currentPlayer == "Black") ? whiteKing : blackKing;
            }
            public String getOpponentPlayer(String currentPlayer)
            {
                if (currentPlayer == "White")
                    return "Black";
                return "White";
            }
            public bool threeFoldScenario()
            {
                if (lastMoveWasPlayed) // Checking the current board position only when a chess move was done and not during draw offer turns.
                {
                    if (isThreeFoldDrawOccurred())
                    {
                        drawGameEvents("ThreeFold");
                        return true;
                    }
                }
                return false;
            }
            public void resetBoardsHistory() // In case of capture or pawn move , the previous boards are not relevant anymore.we can delete them
            {
                positionsHistory = new string[100];
                boardsRepetitionsCounter = new int[100];
                positionsHistoryCounter = 0;
            }
            public void printBoard()
            {
                printBoardFrame(boardRows, 'A');
                Console.WriteLine("  -----------------------------------------");
                printPieces();
                printBoardFrame(boardRows, 'A');
            }
            public void promotionDecision(Piece pawn)
            {
                bool validChoice;
                String input;
                do
                {
                    validChoice = true;
                    Console.WriteLine("Please choose to which piece do you want to promote. " +
                   "Type Queen for queen  ,  Rook for rook , Knight for knight , Bishop for bishop");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "Queen":
                            {
                                addPieces(new Queen("Queen", pawn.getColor(), pawn.getRow(), pawn.getCol()));
                                break;
                            }
                        case "Rook":
                            {
                                addPieces(new Rook("Rook", pawn.getColor(), pawn.getRow(), pawn.getCol()));
                                break;
                            }
                        case "Knight":
                            {
                                addPieces(new Knight("Knight", pawn.getColor(), pawn.getRow(), pawn.getCol()));
                                break;
                            }
                        case "Bishop":
                            {
                                addPieces(new Bishop("Bishop", pawn.getColor(), pawn.getRow(), pawn.getCol()));
                                break;
                            }
                        default:
                            {
                                validChoice = false;
                                break;
                            }
                    }
                } while (!validChoice);
            }
            public void promotion(Piece pawn)
            {
                if ((pawn.getColor() == "White" && pawn.getRow() == 7) || (pawn.getColor() == "Black" && pawn.getRow() == 0))
                {
                    promotionDecision(pawn);
                }
            }
            public void gamePlay() // Manage the game flow - players turns and the game status                   
            {
                bool playerDrawOfferStatus = false;
                Piece opponentKing;
                String opponentPlayer = "";
                printBoard();
                updateAllPiecesMoves(lastMove); // Updates all possible moves for each piece in the board according to the current board status
                while (gameStatus == StatusGame.InProgress)
                {
                    opponentPlayer = getOpponentPlayer(currentPlayer);
                    if (threeFoldScenario()) // Draw by repetitions event
                        break;
                    if (staleMateScenario(currentPlayer)) // StaleMate event
                        break;
                    playerDrawOfferStatus = playerTurn(currentPlayer, isCheck, drawOffer, ""); // Player made his move , in case ask or agreed for draw
                                                                                               // the returned value will be true otherwise false
                    if (fiftyMovesScenario())  // 50 moves draw scenario
                        break;
                    else if (deadPositionScenario()) // Draw by dead position
                        break;
                    else if (playerDrawOfferStatus) // Draw offer event
                    {
                        lastMoveWasPlayed = false;
                        if (drawOfferScenario())
                            break;
                    }
                    else
                        refuseDrawOfferScenario();
                    opponentKing = getOpponentKing();
                    if (checkScenario(opponentKing, opponentPlayer)) // if in last move player gave a check
                        if (mateScenario(opponentPlayer)) // Mate event
                            break;
                        else
                            isCheck = false;
                    printBoard();
                    currentPlayer = getOpponentPlayer(currentPlayer); // switching players turns
                    whiteKing = getKingFromBoard("White");
                    blackKing = getKingFromBoard("Black");
                }
            }
            public String addOptionalEnPassant(String currPlayer)
            {
                String enPassantStr = "";
                int enPassantRow, direction;
                String lastTurn = "";
                enPassantRow = (currentPlayer == "White") ? 4 : 3;
                direction = (currentPlayer == "White") ? 1 : -1;
                Piece targetPiece = null;
                enPassantStr += currPlayer + "_";
                for (int i = 0; i < 8; i++)
                {
                    Piece currPiece = board[enPassantRow, i];
                    if (currPiece is Pawn && currPiece.getColor() == currPlayer) // optional en a passent capture,check if it is valid
                    {
                        if (((Pawn)currPiece).getEnPassantRight()) // check if right capture is valid
                        {
                            enPassantStr += optionsToMakeEnPassant("Right", currPiece, targetPiece, enPassantStr, enPassantRow, lastTurn, direction, i);
                            lastTurn = "";
                        }
                        else if (((Pawn)currPiece).getEnPassantLeft()) // check if left capture is valid
                        {
                            enPassantStr += optionsToMakeEnPassant("Left", currPiece, targetPiece, enPassantStr, enPassantRow, lastTurn, direction, i);
                        }
                    }
                }
                return enPassantStr;
            }
            public String optionsToMakeEnPassant(String enPassantSide, Piece pawn, Piece targetPiece, String enPassantStr, int enPassantRow, String lastTurn, int moveDirection
               , int baseColumn)
            {
                int captureDirection = (enPassantSide == "Right") ? 1 : -1; // adding one to the base column when capturing to the right
                targetPiece = board[enPassantRow, baseColumn + captureDirection];
                lastTurn += ChessUtility.convertNumToFile(baseColumn);
                lastTurn += enPassantRow;
                lastTurn += ChessUtility.convertNumToFile(baseColumn + captureDirection);
                lastTurn += enPassantRow + moveDirection;
                if (simulateEnPassant(pawn, targetPiece, enPassantRow + moveDirection, baseColumn + captureDirection, enPassantRow, baseColumn, lastTurn))
                {
                    enPassantStr += ChessUtility.convertNumToFile(baseColumn) + "_"; // add the file name to the string
                    enPassantStr += "Right";
                    pawn.setRow(enPassantRow);
                    pawn.setCol(baseColumn);
                    addPieces(pawn);
                    board[enPassantRow + moveDirection, baseColumn + captureDirection] = null;
                    board[enPassantRow, baseColumn + captureDirection] = targetPiece;
                    updateAllPiecesMoves(lastMove);
                }
                return enPassantStr;
            }
            public bool isSimulateMoveValid(String targetMove, Piece basePiece) // return true if the move dealt with current check position,otherwise return false
            {
                int targetRow = targetMove[0] - '0';
                int targetCol = targetMove[1] - '0';
                int baseRow = basePiece.getRow();
                int baseCol = basePiece.getCol();
                Piece targetPiece = getBoard()[targetRow, targetCol];
                bool askForEnPassant = false;
                String moveStr = "";
                if (basePiece is Pawn)
                {
                    if (targetPiece == null && baseCol != targetCol)
                    askForEnPassant = true;
                }
                makeSimulateMove(basePiece, null, baseRow, baseCol, targetRow, targetCol, askForEnPassant);
                moveStr += ChessUtility.convertNumToFile(baseCol);
                moveStr += (char)(baseRow + '0');
                moveStr += ChessUtility.convertNumToFile(targetCol);
                moveStr += (char)(targetRow + '0');
                updateAllPiecesMoves(moveStr);
                bool isCheck = searchForCheck(board, getKingFromBoard(basePiece.getColor()));
                makeSimulateMove(basePiece, targetPiece, targetRow, targetCol, baseRow, baseCol, askForEnPassant);
                updateAllPiecesMoves(lastMove);
                if (isCheck)
                    return false;
                return true;
            }
            public void makeSimulateMove(Piece thePiece, Piece targetPiece, int pieceRow, int pieceColumn, int targetRow,
               int targetColumn, bool askForEnPassent)
            {
                thePiece.setRow(targetRow);
                thePiece.setCol(targetColumn);
                addPieces(thePiece);
                getBoard()[pieceRow, pieceColumn] = targetPiece;
                if (askForEnPassent)
                    getBoard()[pieceRow, targetColumn] = targetPiece;
            }
            public bool enPassantIsAllowed(Piece pieceToMove, Piece targetPiece, int baseRow, int baseColumn, int targetRow
               , int targetColumn, String currentMove)
            {
                if ((targetColumn > pieceToMove.getCol()) && ((Pawn)pieceToMove).getEnPassantRight()) // enpassent to the right
                {
                    if (simulateEnPassant(pieceToMove, targetPiece, targetRow, targetColumn, baseRow, baseColumn, currentMove))
                    {
                        return true;
                    }
                    return false;
                }
                else if ((targetColumn < pieceToMove.getCol()) && ((Pawn)pieceToMove).getEnPassantLeft()) // enpassent to the left
                {

                    if (simulateEnPassant(pieceToMove, targetPiece, targetRow, targetColumn, baseRow, baseColumn, currentMove))
                    {
                        return true;
                    }
                }
                return false;
            }
            public bool castlingIsAllowed(Piece pieceToMove, int baseRow, int baseColumn, int kingCol, String currentMove,
               bool kingFirstMove)
            {
                if (!simulateKingMove(pieceToMove, baseRow, kingCol, baseRow, baseColumn, pieceToMove.getCol(), currentMove))
                {
                    ((King)pieceToMove).setFirstMove(kingFirstMove);
                    return false;
                }
                return true;
            }
            public void moveRookForCastling(int baseRow, int targetColumn, bool isShortCastling)
            {
                int rookBaseColumnDifference = (isShortCastling == true) ? 1 : -2;
                int rookTargetColOffset = (isShortCastling == true) ? -1 : 1;
                Piece rook = getBoard()[baseRow, targetColumn + rookBaseColumnDifference];
                rook.setCol(targetColumn + rookTargetColOffset);
                addPieces(rook);
                getBoard()[baseRow, targetColumn + rookBaseColumnDifference] = null;
                ((Rook)rook).setFirstMove(false);
            }
            public bool castlingMoveScenario(Piece pieceToMove, int baseRow, int baseColumn, int targetRow
                , int targetColumn, int columnDifference, String currentMove, bool kingFirstMove)
            {
                bool CheckStatus = searchForCheck(board, pieceToMove);
                if (CheckStatus)
                {
                    ((King)pieceToMove).setFirstMove(kingFirstMove);
                    return false; // the king is in check , castle is not valid
                }
                else if (columnDifference == -2) // short castle case
                {
                    for (int kingCol = baseColumn + 1; kingCol <= targetColumn; kingCol++) // iterate over the squares that the king is
                                                                                           // moving in the castle,and make sure none of them is in check
                    {
                        if (!castlingIsAllowed(pieceToMove, baseRow, baseColumn, kingCol, currentMove, kingFirstMove))
                            return false;
                    } // the short castle is valid , switching the rook and clearing his old position
                    moveRookForCastling(baseRow, targetColumn, true);
                    return true;
                }
                else                                            // long castle case
                {
                    for (int kingCol = baseColumn - 1; kingCol >= targetColumn; kingCol--)
                    {
                        if (!castlingIsAllowed(pieceToMove, baseRow, baseColumn, kingCol, currentMove, kingFirstMove))
                            return false;
                    }
                    moveRookForCastling(baseRow, targetColumn, false);
                    return true;
                }
            }
            public bool regularMoveIsAllowed(Piece pieceToMove, int baseRow, int baseColumn, int targetRow
                , int targetColumn, String currentMove)
            {
                makeSimulateMove(pieceToMove, null, baseRow, baseColumn, targetRow, targetColumn, false);
                updateAllPiecesMoves(currentMove);
                isCheck = searchForCheck(board, getKingFromBoard(pieceToMove.getColor()));
                if (isCheck == true)
                    return false;
                return true;
            }
            public void resetPiecesFirstMove(Piece pieceToMove, bool pawnStatus, bool kingStatus, bool rookStatus)
            {
                if (pieceToMove is Pawn)
                    ((Pawn)pieceToMove).setStartSquare(pawnStatus);
                else if (pieceToMove is King)
                    ((King)pieceToMove).setFirstMove(kingStatus);
                else if (pieceToMove is Rook)
                    ((Rook)pieceToMove).setFirstMove(rookStatus);
            }
            public void printPieces()
            {
                for (int i = boardRows - 1; i >= 0; i--)
                {
                    Console.Write((i + 1) + " ");
                    for (int j = 0; j < boardRows; j++)
                    {
                        String pos = "";
                        Console.Write("| ");
                        if (board[i, j] == null)
                        {
                            Console.Write("   ");
                            continue;
                        }
                        Piece currPiece = board[i, j];
                        if (currPiece != null)
                        {
                            pos += currPiece.getColor()[0];
                            pos += currPiece.getName()[0];
                        }
                        Console.Write(pos + " ");
                    }
                    Console.Write("| ");
                    Console.Write((i + 1) + " ");
                    Console.WriteLine("");
                    Console.WriteLine("  -----------------------------------------");
                }
            }
            public void printBoardFrame(int rows, char tav)
            {
                Console.Write("  ");
                for (int i = rows; i >= 0; i--)
                {
                    if (i == rows)
                    {
                        Console.Write($"{"",-2}");
                        continue;
                    }
                    else
                    {
                        Console.Write($"{tav,-5}");
                        tav++;
                    }
                }
                Console.WriteLine("");
            }
            public bool deadPositionDraw(String pieceName) // draw by insufficient material
            {
                int whitePieces = 0, blackPieces = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Piece currPiece = getBoard()[i, j];
                        if (currPiece != null)
                        {
                            if (currPiece.getName() != pieceName && currPiece.getName() != "King")
                                return false;
                            else
                            {
                                if (currPiece.getName() == pieceName)
                                {
                                    if (currPiece.getColor() == "White")
                                        whitePieces++;
                                    else
                                        blackPieces++;
                                }
                            }
                        }
                    }
                }

                if (whitePieces != 1 || blackPieces != 1)
                    return false;
                return true;
            }
            public bool simulateKingMove(Piece basePiece, int targetRow, int targetCol, int baseRow,
                int baseCol, int currntKingCol, String currMove)
            {
                makeSimulateMove(basePiece, null, baseRow, currntKingCol, targetRow, targetCol, false);
                getBoard()[baseRow, currntKingCol] = null;
                updateAllPiecesMoves(currMove);
                isCheck = searchForCheck(board, getKingFromBoard(basePiece.getColor()));
                if (isCheck == true)
                {
                    makeSimulateMove(basePiece, null, targetRow, targetCol, baseRow, baseCol, false); // undo the previous move
                    updateAllPiecesMoves(lastMove);
                    return false;
                }
                return true;
            }
            public bool simulateEnPassant(Piece basePiece, Piece targetPiece, int targetRow, int targetCol, int baseRow,
                int baseCol, String currMove)
            {
                makeSimulateMove(basePiece, null, baseRow, baseCol, targetRow, targetCol, true);
                updateAllPiecesMoves(currMove);
                isCheck = searchForCheck(board, getKingFromBoard(basePiece.getColor()));
                if (isCheck == true) // this move is not dealing with the check
                {
                    makeSimulateMove(basePiece, null, targetRow, targetCol, baseRow, baseCol, true);
                    getBoard()[targetRow, targetCol] = null;
                    updateAllPiecesMoves(lastMove); // update the board pieces with the current possible moves
                    return false;
                }
                return true;
            }
            public bool searchForCheck(Piece[,] theBoard, Piece king)
            {
                String kingLocation = ChessUtility.convertSquareToString(king.getRow(), king.getCol());
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Piece p = getBoard()[i, j];
                        if (p != null && p.getColor() != king.getColor())
                        {
                            if (p.getName() == "Pawn")
                            {
                                if (((Pawn)p).getCaptureMoves().Contains(kingLocation))
                                {
                                    return true;
                                }
                            }
                            else if (p.getAllPossibleMoves().Contains(kingLocation))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            public bool playerHasAnyLegalMove(Piece[,] theBoard, String currPlayer)
            {
                for (int i = 0; i < 8; i++) // iterate over the board looking for opponent pieces
                {
                    for (int j = 0; j < 8; j++)
                    {

                        Piece currPiece = theBoard[i, j];
                        if (currPiece != null && currPiece.getColor() == currPlayer)
                        {
                            for (int movesIndex = 0; movesIndex < currPiece.getAllPossibleMoves().Length; movesIndex++)
                            { // running on all piece possible moves
                                String targetMoveString = currPiece.getAllPossibleMoves()[movesIndex];
                                if (targetMoveString == null || targetMoveString == "")
                                    break;
                                if (currPiece is King)
                                {
                                    int kingMoveColumn = targetMoveString[1] - '0';
                                    if (currPiece.getCol() - kingMoveColumn == 2 || currPiece.getCol() - kingMoveColumn == -2) // castle move
                                        continue;
                                }
                                if (isSimulateMoveValid(targetMoveString, currPiece))
                                    return false;
                            }
                        }
                    }
                }
                return true;
            }
            public bool playerTurn(String currPlayer, bool isCheck, bool drawOffer, String str) //Manages player move and check it's validation
            {
                bool userInput = false;
                String move = "";
                while (!userInput)
                {
                    move = validMoveFormat(); //check the format of the move to add option for offering a draw
                    if (move == "draw" || move == "DRAW")
                    {
                        return true;
                    }
                    else if (drawOffer) // player did not agree to draw offer , skipping on his move in order to get back to opponent turn
                        return false;
                    if (!validBaseSquare(move))
                        Console.WriteLine("The base square is empty or taken by the opponent");

                    else if (!validTargetSquare(move)) // check if player selected one of his pieces and not the opponent's one
                                                       // or empty cell
                    {
                        Console.WriteLine("The target square is already taken by you, or taken by the enemy king");
                    }
                    else if (!pieceMoveIsAllowed(move, isCheck)) // handling check scenarios and pinned moves
                    {
                        if (isCheck)
                            Console.WriteLine("You have to response to the check");
                        Console.WriteLine("The selected piece can not reach the target square");
                    }
                    else
                        userInput = true;
                }
                lastMove = move;
                return false;
            }
            public void setGameStatusAfterMove(Piece pieceToMove, Piece targetPiece)
            {
                if (pieceToMove is Pawn)
                {
                    setGameMovesCount(0);
                    resetBoardsHistory();
                    promotion(pieceToMove);
                }
                else if (targetPiece != null)
                {
                    setGameMovesCount(0);
                    resetBoardsHistory();
                }
                else
                    gameMovesNumber++;
            }
            public bool pieceMoveIsAllowed(string move, bool isCheck) // check if the movement of selected piece can reach the target square
            {

                int baseRow = ChessUtility.getRowFromMove(move, 1);
                int baseCol = ChessUtility.getColumnFromMove(move, 0);
                int targetRow = ChessUtility.getRowFromMove(move, 3);
                int targetCol = ChessUtility.getColumnFromMove(move, 2);
                String targetPositionStr = ChessUtility.convertSquareToString(targetRow, targetCol);
                bool kingFirstMove = false, rookFirstMove = false, pawnMove = false;
                Piece targetPiece = getBoard()[targetRow, targetCol];
                Piece pieceToMove = getBoard()[baseRow, baseCol];
                if (pieceToMove.getAllPossibleMoves().Contains(targetPositionStr))
                {
                    if (pieceToMove is Pawn)
                    {
                        pawnMove = ((Pawn)pieceToMove).getStartSquare();
                        ((Pawn)pieceToMove).setStartSquare(false);
                        if (targetPiece == null && (targetCol != baseCol)) // if asks for en a passant
                        {
                            if (enPassantIsAllowed(pieceToMove, targetPiece, baseRow, baseCol, targetRow, targetCol, move))
                            {
                                setGameMovesCount(0);
                                resetBoardsHistory();
                                return true;
                            }
                            else
                                return false;
                        }
                    }
                    else if (pieceToMove is King)
                    {
                        kingFirstMove = ((King)pieceToMove).getFirstMove();
                        ((King)pieceToMove).setFirstMove(false);
                        int columnDifference = pieceToMove.getCol() - targetCol;
                        if (columnDifference == 2 || columnDifference == -2) // asking for one of the castling options
                        {
                            if (castlingMoveScenario(pieceToMove, baseRow, baseCol, targetRow, targetCol, columnDifference, move, kingFirstMove))
                            {
                                updateAllPiecesMoves(move);
                                gameMovesNumber++;
                                return true;
                            }
                            else
                                return false;
                        }
                    }
                    else if (pieceToMove is Rook)
                    {
                        rookFirstMove = ((Rook)pieceToMove).getFirstMove();
                        ((Rook)pieceToMove).setFirstMove(false);
                    }
                    if (!regularMoveIsAllowed(pieceToMove, baseRow, baseCol, targetRow, targetCol, move))
                    {
                        makeSimulateMove(pieceToMove, targetPiece, targetRow, targetCol, baseRow, baseCol, false);
                        resetPiecesFirstMove(pieceToMove, pawnMove, kingFirstMove, rookFirstMove);
                        updateAllPiecesMoves(lastMove); // update the board pieces with the current possible moves
                        return false;
                    }
                    else                     // the simulated move is valid and saved in the board for the next turn
                    {
                        setGameStatusAfterMove(pieceToMove, targetPiece);
                        return true;
                    }
                }
                return false;
            }
        }
    }

