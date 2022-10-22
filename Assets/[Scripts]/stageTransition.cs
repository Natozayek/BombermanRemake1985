using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Scrip to load game scene after the loading scene
public class stageTransition : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 2.8)
        {
            SceneManager.LoadScene("LevelOne");

        }
       
    }
}
