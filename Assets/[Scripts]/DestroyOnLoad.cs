using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    void Start()
    {
       //Destroy Main Menu BGM
        Destroy(DontDestroyBGM.Instance.gameObject);
        Time.timeScale = 1.0f;
       
    }

}
