using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    private int currentPlayer; // 1 za X, 2 za O
    private int[] board; // Ploča za igru
    private bool gameFinished;

    void Start()
    {
        board = new int[9];
        ResetBoard();
    }

    void OnGUI()
    {
        if (gameFinished)
        {
            GUI.Label(new Rect(100, 10, 200, 100), "Igra završena!");
            if (GUI.Button(new Rect(100, 120, 100, 40), "Nova igra"))
            {
                ResetBoard();
            }
            return;
        }

        for (int i = 0; i < 9; i++)
        {
            int row = i / 3;
            int col = i % 3;
            int value = board[i];

            if (value == 1)
                GUI.Button(new Rect(100 + col * 60, 100 + row * 60, 60, 60), "X");
            else if (value == 2)
                GUI.Button(new Rect(100 + col * 60, 100 + row * 60, 60, 60), "O");
            else if (GUI.Button(new Rect(100 + col * 60, 100 + row * 60, 60, 60), ""))
            {
                if (currentPlayer == 1)
                {
                    board[i] = 1;
                    currentPlayer = 2;
                }
                else
                {
                    board[i] = 2;
                    currentPlayer = 1;
                }
                CheckForWinner();
            }
        }
    }

    void CheckForWinner()
    {
        // Provjera pobjednika
        for (int i = 0; i < 3; i++)
        {
            if (board[i] != 0 && board[i] == board[i + 3] && board[i] == board[i + 6])
            {
                gameFinished = true;
                return;
            }

            int j = i * 3;
            if (board[j] != 0 && board[j] == board[j + 1] && board[j] == board[j + 2])
            {
                gameFinished = true;
                return;
            }
        }

        if (board[0] != 0 && board[0] == board[4] && board[0] == board[8])
        {
            gameFinished = true;
            return;
        }

        if (board[2] != 0 && board[2] == board[4] && board[2] == board[6])
        {
            gameFinished = true;
            return;
        }

        // Provjera na remi
        bool draw = true;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                draw = false;
                break;
            }
        }

        if (draw)
        {
            gameFinished = true;
        }
    }

    void ResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            board[i] = 0;
        }
        currentPlayer = 1;
        gameFinished = false;
    }
}