using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int initialLives = 3;
    int currentLives;

    public GameObject playerPrefab;
    public Transform spawnpoint;

    [SerializeField] AudioSource BGM;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
            
            
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
  

    void Start()
    {
      
        BGM = GetComponent<AudioSource>();

        currentLives = initialLives;
        CreatePlayer();
        BGM.Play();
        
    }

    public void resetGame()
    {


        UserInterfaceManager.instance.UpdateLivesAmount(currentLives);
        ScoreManager.instance.ResetScore();
        StartCoroutine(CreatePlayerCoro());

    }
    private IEnumerator CreatePlayerCoro()
    {
        yield return new WaitForSeconds(0.2f);
        CreatePlayer();
    }
    public void CreatePlayer()
    {
        if (currentLives > 0)
        {
            
            playerPrefab = Instantiate( playerPrefab, spawnpoint.position, Quaternion.identity);
            currentLives--;
            UserInterfaceManager.instance.UpdateLivesAmount(currentLives);
            playerPrefab.SetActive(true);
            
        }
        else
        {

            UserInterfaceManager.instance.ShowGameOver();

        }

    }
    public void resetLevel()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void IncreaseLives()
    {
        currentLives++;
       UserInterfaceManager.instance.UpdateLivesAmount(currentLives);
    }

}
