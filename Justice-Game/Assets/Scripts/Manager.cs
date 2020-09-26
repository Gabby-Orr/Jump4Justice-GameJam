using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private Manager() {}
    public static int WIN = 1;
    public static int LOSE = 0;
    private bool isGameOver = false;
    public bool IsGameOver()
    {
        return isGameOver;
    }
    private bool isWin = false;
    public bool IsWin()
    {
        return isWin;
    }
    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void GameOver(int status)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            isWin = (status == WIN);
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
        }
    }
    public void RestartGame()
    {
        // do other things to reset the game
        isGameOver = false;
        SceneManager.UnloadSceneAsync("GameOverScene");
    }
}
