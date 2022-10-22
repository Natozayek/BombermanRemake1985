using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //Variables
    public static ScoreManager instance;
    private int score;
    public int finalScore;

    //Instance
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
    //Update final score in game over scene
    private void OnLevelWasLoaded(int level)
    {
        UserInterfaceManager.instance.FinalScore();
    }
    //Return current score
    public int ReturnScore()
    {
        return score;
    }
    //Add score
    public void AddScore(int amount)
    {
            score += amount;
            UserInterfaceManager.instance.UpdateScore(score);
    }
    //Add final score
    public void AddFinalScore(int amount)
    {
        finalScore += amount;
        
    }
    //Reset any score
    public void ResetScore()
    {
        score = 0;
        finalScore = 0;
        UserInterfaceManager.instance.UpdateScore(score);
    }

 
}
