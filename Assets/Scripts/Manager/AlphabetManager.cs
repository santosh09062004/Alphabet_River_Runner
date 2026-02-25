using UnityEngine;

public class AlphabetManager : MonoBehaviour
{
    private char currentTarget = 'A';
    private bool gameOver = false;
    private bool gameWon = false;

    public char GetCurrentTarget()
    {
        return currentTarget;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public bool IsGameWon()
    {
        return gameWon;
    }

    public bool CheckLetter(char selectedLetter)
    {
        if (gameOver || gameWon)
            return false;

        if (selectedLetter == currentTarget)
        {
            if (currentTarget == 'Z')
            {
                gameWon = true;
                Debug.Log("YOU WIN!");
            }
            else
            {
                currentTarget++;
                Debug.Log("Correct! Next letter: " + currentTarget);
            }

            return true;
        }
        else
        {
            gameOver = true;
            Debug.Log("WRONG LETTER! GAME OVER");
            return false;
        }
    }
}