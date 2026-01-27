using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
        
    }

    public GameObject loadingScreen; // Your UI Panel with animated text

    public void LoadGameScene()
    {
        loadingScreen.SetActive(true); // Show the loading screen
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation op = SceneManager.LoadSceneAsync("SampleScene"); // Change name accordingly

        while (!op.isDone)
        {
            yield return null;
        }
    }
}
