using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimateBG : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.3)
        {
            GetComponent<Image>().enabled = false;

        }
        if (timer >= 0.6)
        {
            GetComponent<Image>().color = new Color(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f));
            GetComponent<Image>().enabled = true;

            timer = 0;
        }
    }
}
