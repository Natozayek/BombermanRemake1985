using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{  
    [Header("Sprites")]
    //public SpriteRendererController spriteAnimUp;
    //public SpriteRendererController spriteAnimLeft;
    //public SpriteRendererController spriteAnimRight;
    public SpriteRendererController spriteAnimDown;
    public SpriteRendererController spriteAnimDeath;
    private SpriteRendererController activeAnimation;
    public static EnemyAI Instance;

    private Vector2 m_Target = Vector2.zero;
    private Vector2 direction;
    private List<Vector2> currentPath = new List<Vector2>();
    private List<Vector2> pathToPlayer = new List<Vector2>();
    private List<Vector2> RandomPath = new List<Vector2>();
    
    private PathFinder PathFinder;
    private bool isMoving;
    public bool canSeePlayer;
    public GameObject Player;
    public float speed = 2f;
    public LayerMask layerName;
    private bool isAlive = true;

    private void Awake()
    {
        Instance = this;
        activeAnimation = spriteAnimDown;
        this.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (Player != null)
        {
            PathFinder = GetComponent<PathFinder>();
            CalculatePossibleWay();
        }
    }

    private void Update()
    {
        if(isAlive)
        {

            if (!canSeePlayer)
            {
                MoveNoAnim();
            }
            else
            {
                Chase();
            }
        }
    }

    private void Chase()
    {
        float velocity = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, velocity);
    }
    private void MoveNoAnim()
    {
        if (currentPath.Count == 0 && Vector2.Distance(transform.position, Player.transform.position) > 1f)
        {
            CalculatePossibleWay();
            isMoving = true;
        }

        if (isMoving)
        {

            if (Vector2.Distance(transform.position, currentPath[currentPath.Count - 1]) >=0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPath[currentPath.Count - 1], speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, currentPath[currentPath.Count - 1]) <= 0.1f)
            {
                isMoving = false;
            }
        }
        else
        {
            CalculatePossibleWay();
            isMoving = true;
        }
    }
    private void setSpriteController( SpriteRendererController spriteController)
    {
        //spriteAnimUp.enabled = spriteController == spriteAnimUp;
        //spriteAnimLeft.enabled = spriteController == spriteAnimLeft; 
        //spriteAnimRight.enabled = spriteController == spriteAnimRight;
        spriteAnimDown.enabled = spriteController == spriteAnimDown;
        activeAnimation = spriteController;
    }
    public void CalculatePossibleWay()
    {
        pathToPlayer = PathFinder.FindAPath(Player.transform.position);

        if (pathToPlayer.Count == 0)
        {
            var r = Random.Range(0, PathFinder.FreeNodes.Count);
            RandomPath = PathFinder.FindAPath(PathFinder.FreeNodes[r].pos);
            currentPath = RandomPath;
        }
    }

    public static Vector2 SetRandomDirection()
    {
        return new Vector2(Random.Range(-1.0f,1.0f), Random.Range(-2.0f, 2.0f));
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Destructible" || other.gameObject.tag == "Indestructible" || other.gameObject.tag == "Bomb" || other.gameObject.tag == "Enemy")
        {
            CalculatePossibleWay();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") || other.tag == "Explosion")
        {
            isAlive = false;    
            DeathSequence();
            ScoreManager.instance.AddScore(100);
            ScoreManager.instance.AddFinalScore(100);
        }
        if (other.gameObject.tag == "PickUp")
        {
            CalculatePossibleWay();
        }

    }

    public void DeathSequence()
    {
       
        //Disable renderers
        //spriteAnimUp.enabled = false;
        //spriteAnimLeft.enabled = false;
        //spriteAnimRight.enabled = false;

        spriteAnimDown.enabled = false;
        GameManager.instance.initialEnemies -= 1;
        activeAnimation = spriteAnimDeath;
        spriteAnimDeath.enabled = true;
        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }
    void OnDeathSequenceEnded()
    {
        this.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject,2);
    }
}
