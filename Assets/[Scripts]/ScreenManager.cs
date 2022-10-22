using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    //Functions to traverse between scenes.
   public void MainMenu()
    {
        StartCoroutine(goToMainMenu());
    }

    public void GameLevel()
    {
        StartCoroutine(goToGameScene());
    }

    public void StageTransition()
    {
        StartCoroutine( loadingScene());
    }

    public void InstructionScene()
    {
        StartCoroutine( goToInstructionsMenu());
    }

    public void GameOverScene()
    {
        SceneManager.LoadScene("LoseScreen");
    }
    public IEnumerator loadingScene()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("LoadingScene");

    }

    public IEnumerator goToMainMenu()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MainMenu");

    }
    public IEnumerator goToInstructionsMenu()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Instructions");

    }

    public IEnumerator goToGameScene()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("LevelOne");

    }
}
