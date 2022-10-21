using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score;
    public int finalScore;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if( instance != this )
        {
            Destroy(gameObject);
        }
        
        

    }
    private void OnLevelWasLoaded(int level)
    {
        UserInterfaceManager.instance.FinalScore();
    }

    public int ReturnScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
            score += amount;
            UserInterfaceManager.instance.UpdateScore(score);
    }

    public void AddFinalScore(int amount)
    {
        finalScore += amount;
        
    }

    public void ResetScore()
    {
        score = 0;
        UserInterfaceManager.instance.UpdateScore(score);
    }

 
}
