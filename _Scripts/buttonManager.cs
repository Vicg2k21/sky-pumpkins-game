using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("s4_game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("s2_menu");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("s3_instructions");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("s5_scores");
    }

     public void QuitGame()
    {
        Debug.Log("Application Quit");

        Application.Quit();
    }
}
