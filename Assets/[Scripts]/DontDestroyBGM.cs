using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBGM : MonoBehaviour
{
    //Script to keep Main menu music while the player is traversing through scenes
    [SerializeField] AudioSource clip;
    public bool isPaused = false;
    private static DontDestroyBGM instance = null;
    public static DontDestroyBGM Instance
    {
        get { return instance; }
    }
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (isPaused == true)
        {
            clip.Play();

        }

    }
}
