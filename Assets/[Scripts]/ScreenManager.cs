using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
   public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameLevel()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void InstructionScene()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void GameOverScene()
    {
        SceneManager.LoadScene("LoseScreen");
    }
    public IEnumerator skipScene()
    {
        yield return new WaitForSeconds(10.0f);

        GameOverScene();

    }
}
