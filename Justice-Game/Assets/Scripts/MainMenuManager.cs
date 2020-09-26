using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private int timeElapsed;
    public Text titleText;
    public Text titleTextShadow;
    public Text startButtonText;
    void Awake()
    {
        titleTextShadow.text = titleText.text;
    }
    void Update()
    {
        timeElapsed++;
        if (timeElapsed < 100)
            startButtonText.color = new Color32(237, 191, 18, 255);
        else
            startButtonText.color = Color.black;
        if (timeElapsed > 200)
            timeElapsed = 0;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
