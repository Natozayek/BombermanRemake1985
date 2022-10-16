using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public static GameObject Object;

    public Text timeText;
    public float timeRemaining = 180;
    public bool timerIsRunning = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
      
        
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
    private void Start()
    {
        Object = GameObject.Find("timeAmmount");
        // Starts the timer automatically
        timeText = Object.GetComponent<Text>();
        timerIsRunning = true;
    }
   
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            timerIsRunning = false;
            GameManager.instance.resetGame();

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 180);
        timeText.text = string.Format("{000}", seconds);
    }
}
