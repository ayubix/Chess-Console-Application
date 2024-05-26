# Chess-Console-Application

Implemented in **C#** with a focus on **Object-Oriented Programming (OOP) principles**, this chess console application offers a clean and modular design for easy maintenance and extensibility.

This application allows you to play chess in the **command-line interface**. It's a simple yet fun way to enjoy the classic game of chess.

## Introduction

Chess is a two-player strategy board game played on a checkered board with 64 squares arranged in an 8×8 grid. The game is played by millions of people worldwide, and it's known for its deep strategy and complexity. If you're unfamiliar with the rules of chess, you can learn them [here](https://www.chess.com/learn-how-to-play-chess)

## Features

###  Play chess against another human player.
###  Basic chess rules implemented, including pawn promotion, castling, en passant, and check/checkmate detection.
###  Various types of draws (stalemate, threefold repetition, and the fifty-move rule and more) are implemented.
###  ASCII art representation of the chessboard for visualization.
###  Command-line interface for ease of use.



## How to Play

The game is played by entering commands in the command-line interface.
Each player takes turns to make a move by entering the origin and destination squares of the piece they want to move.
Follow the standard algebraic notation for moves **(e.g., "e2e4" to move the pawn from e2 to e4)**.
Example of castling: **"e1g1" (for white kingside castling) or "e8g8" (for black kingside castling)**.



## Piece Representation on the Console Board


In our chess console app, each piece on the board is represented by a two-character code. 
The first character indicates the color of the piece, and the second character denotes the type of the piece.

### Here’s a detailed explanation:
Color of the Piece:

### W - White
### B - Black


 Type of the Piece:

### P - Pawn
### R - Rook
### N - Knight
### B - Bishop
### Q - Queen
### K - King
 Examples:
### WP - White Pawn
### WR - White Rook
### WN - White Knight
### WB - White Bishop
### BR - Black Rook
### BN - Black Knight
### BB - Black Bishop


## Prerequisites
Make sure you have Visual Studio 2022 installed on your system, along with the .NET Core SDK.


## Installation
1.Clone the repository:

![image](https://github.com/ayubix/Chess-Console-Application/assets/86429159/cbf3f76b-ce58-47de-8c26-599438a85c92)


2.Navigate to the project directory:

![image](https://github.com/ayubix/Chess-Console-Application/assets/86429159/7f8d70e9-2d22-4fc1-b124-00f8df0e1001)


3.Open the solution file (chess-console.sln) in Visual Studio 2022.

4.Build and run the application using Visual Studio.

5.Enjoy with your chess game


## Screenshots


The chess board after white first move 

![image](https://github.com/ayubix/Chess-Console-Application/assets/86429159/c04c4d18-7ee0-42a0-ad87-07f4cdde141b)




The chess board after 4 moves , showing the Italian game.


![image](https://github.com/ayubix/Chess-Console-Application/assets/86429159/96d43c11-4a27-44b6-9fab-279b12ccda26)

Example of a game ended in a draw after an offer, and the application menu reloads for the user to make a new selection.


![image](https://github.com/ayubix/Chess-Console-Application/assets/86429159/16a339b0-4cc0-4810-a171-1dd043117366)




Example of a Scholar's mate, game ended in White victory.


![image](https://github.com/ayubix/Chess-Console-Application/assets/86429159/9f347a8c-8168-4067-9174-234cca122c65)












