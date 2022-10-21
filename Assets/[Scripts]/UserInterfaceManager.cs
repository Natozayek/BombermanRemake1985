using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;
    public Text LiveAmmount, scoreText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
    public void ShowGameOver()
    {
        Invoke(nameof(LoseScreen), 1.25f);
    }
    public void UpdateLivesAmount(int amount)
    {
        LiveAmmount.text = amount.ToString();
    }
    public void UpdateScore(int amount)
    {
        scoreText.text = amount.ToString("D3");
    }

    public void FinalScore()
    {
        scoreText.text = ScoreManager.instance.finalScore.ToString("D3");
    }
    private void LoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

}

