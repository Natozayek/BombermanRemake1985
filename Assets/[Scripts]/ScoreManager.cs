using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score;

    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        AddScore(0);
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

    public void ResetScore()
    {
        score = 0;
        UserInterfaceManager.instance.UpdateScore(score);
    }

 
}
