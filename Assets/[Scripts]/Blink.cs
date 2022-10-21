using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.5)
        {
            GetComponent<Text>().enabled = false;

        }
        if (timer >= 1)
        {
            GetComponent<Text>().color = new Color(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f));
            GetComponent<Text>().enabled = true;
            
            timer = 0;
        }
    }
}
