using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int initialLives = 3;
    public int currentLives;
    public int initialEnemies;
    public int currentEnemies;
    

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform spawnpoint;

    public List<Transform> spawnPoints;
    public List<GameObject> enemyPrefabs;
    [SerializeField] AudioSource BGM;

    //Singleton for game scene
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton 
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.

        
    }
    void Start()
    {
        //Initial parameters for game scene
        initialEnemies = 5;
        currentEnemies = initialEnemies;
        currentLives = initialLives;
        ScoreManager.instance.ResetScore();
        CreatePlayer();
        CreateEnemies();
    }
    public void Update()
    {
        if(currentEnemies <= 0)
        {
            UserInterfaceManager.instance.ShowGameOver();
        }
    }
    public void resetGame()
    {
        if(currentLives >0)
        {
            UserInterfaceManager.instance.UpdateLivesAmount(currentLives);
            
            StartCoroutine(CreatePlayerCoro());
        }
        else
        {
            Debug.Log("Calling line 65");
            UserInterfaceManager.instance.ShowGameOver();
        }
      
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
            Debug.Log("Calling line 85");
            UserInterfaceManager.instance.ShowGameOver();
        }

    }
    private void CreateEnemies()
    {
        for (int i = 0; i < initialEnemies; i++)
        {
            if (i > initialEnemies)
            {
                return;
            }
            enemyPrefab = Instantiate(enemyPrefabs[i], spawnPoints[i].position, Quaternion.identity);
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
