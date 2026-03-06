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
        Application.Quit();
        
    }

    public GameObject loadingScreen;
    public void LoadGameScene()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation op = SceneManager.LoadSceneAsync("SampleScene");

        while (!op.isDone)
        {
            yield return null;
        }
    }
}
