using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text gameOverMessage;
    public Text gameOverMessageShadow;
    public Text retryButtonText;
    void Start()
    {
        if (Manager.instance.IsWin())
        {
            gameOverMessage.text = "You win!";
            retryButtonText.text = "Play Again";
        }
        else
        {
            gameOverMessage.text = "Game over";
            retryButtonText.text = "Try Again";
        }
        gameOverMessageShadow.text = gameOverMessage.text;
    } 
    public void Retry()
    {
        Manager.instance.RestartGame();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
