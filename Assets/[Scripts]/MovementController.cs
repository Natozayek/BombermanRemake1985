using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using System.Text;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    
    [Header("Movement")]
    public Rigidbody2D rigidbody;
    private Vector2 direction = Vector2.down;
    public float speed = 4f;

    [Header("Input")]
    private KeyCode inputUp = KeyCode.W;
    private KeyCode inputDown = KeyCode.S;
    private KeyCode inputLeft = KeyCode.A;
    private KeyCode inputRight = KeyCode.D;


    [Header("Sprites")]
    public SpriteRendererController spriteAnimUp;
    public SpriteRendererController spriteAnimDown;
    public SpriteRendererController spriteAnimLeft;
    public SpriteRendererController spriteAnimRight;
    public SpriteRendererController spriteAnimDeath;
    private SpriteRendererController activeAnimation;


   [Header("Camera")]
    Camera cam;
    [Range(0f, 2f)]
    float moveCam = 0;
    public bool usingMobileInput;

    //Movement
    private bool moveUp;
    private bool moveDown;
    private bool moveLeft; 
    private bool moveRight;
    private float horizontalMove;
    private float verticalMove;
    private bool isMoving;

    //Instance
    public static MovementController Instance;

    //Sound effects

    [SerializeField] AudioSource walkSFX;
    [SerializeField] AudioSource updateBGM;
    [SerializeField] AudioSource stopBGM;
    [SerializeField] AudioSource deathSFX;
   
    private void Awake()
    {
       Instance = this;
       cam = Camera.main;
       activeAnimation =  spriteAnimDown;
       this.gameObject.SetActive(true);
    }
    private void Start()
    {
        
        spriteAnimDeath.enabled = false;
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveRight = false;

        rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<BombController>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<MovementController>().enabled = true;
        cam.transform.position = new Vector3(-2.27999997f, 0f, -5.00235558f);

        // Platform Detection for input
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;
    }

 
    void Update()
    {
        MoveCamera();
        GetMobileInput();
        if (isMoving)
        {
            if(!walkSFX.isPlaying)
            {
                walkSFX.Play();
                
            }
        }
        else
        {
            walkSFX.Stop();
        }
    }

    //Functions to move player through mobile or conventional input
    public void GetMobileInput()
    {

        if (moveUp)
        {
            SetDirection(Vector2.up, spriteAnimUp);
            isMoving = true;
        }
        else if (moveDown)
        {
            SetDirection(Vector2.down, spriteAnimDown);
            isMoving = true;
          

        }
        else if (moveLeft)
        {
            SetDirection(Vector2.left, spriteAnimLeft);
            isMoving = true;
        }
        else if (moveRight)
        {
            SetDirection(Vector2.right, spriteAnimRight);
            isMoving = true;
        }
        else
        {
            SetDirection(Vector2.zero, activeAnimation);
            isMoving = false;
        }

    }
    private void getConventionalInput()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteAnimUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteAnimDown);

        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteAnimLeft);

        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteAnimRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeAnimation);
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }
    private void MoveCamera()
    {
        if (transform.position.x < -0.1f && moveCam >= 0 && Input.GetKey(inputLeft))
        {
            MoveCamToLeft();
        }

        if (transform.position.x > 0.1f && moveCam <= 1.5 && Input.GetKey(inputRight))
        {
            MoveCamToRight();
        }
    }
    private void MoveCamToRight()
    {
        cam.transform.position = new Vector3(cam.transform.position.x + (moveCam * 1 / 5), cam.transform.position.y, cam.transform.position.z);
        if (moveCam < 1.5)
        {
            moveCam += 0.05f;
        }
    }
    private void MoveCamToLeft()
    {
        cam.transform.position = new Vector3(cam.transform.position.x - (moveCam * 1 / 5), cam.transform.position.y, cam.transform.position.z);
        if (moveCam >= 0)
        {
            moveCam -= 0.05f;
        }
    }

    //Sets the direction of the player and the new animation
    private void SetDirection(Vector2 newDirection, SpriteRendererController spriteController)
    {
        direction = newDirection;

        spriteAnimUp.enabled = spriteController == spriteAnimUp; 
        spriteAnimDown.enabled = spriteController == spriteAnimDown;
        spriteAnimLeft.enabled = spriteController == spriteAnimLeft;
        spriteAnimRight.enabled = spriteController == spriteAnimRight;

        activeAnimation = spriteController;
        activeAnimation.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            deathSFX.Play();
            DeathSequence();

        }
        if (other.gameObject.tag == "PickUP")
        {

            ScoreManager.instance.AddScore(1000);
            ScoreManager.instance.AddFinalScore(1000); 

            stopBGM.Stop();
            if(!updateBGM.isPlaying)
            updateBGM.Play();

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            deathSFX.Play();
            DeathSequence();
        }
    }
    private void DeathSequence()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        enabled = false;
       
        //Disable renderers
        spriteAnimUp.enabled=false;
        spriteAnimDown.enabled=false;
        spriteAnimLeft.enabled=false;
        spriteAnimRight.enabled=false;

        spriteAnimDeath.enabled = true;
        //Add new death renderer
        
        Invoke(nameof(OnDeathSequenceEnded), 1.45f);
       
    }
    void OnDeathSequenceEnded()
    {
        spriteAnimDeath.animationFrame = 0;
        enabled = true;
        GetComponent<BombController>().enabled = true;
        
        GameManager.instance.resetGame();
        this.gameObject.SetActive(false);
        Destroy(gameObject, 2);
    }

    //Event triggers for mobile input
    public void PointerDownLeft()
    {
        Instance.moveLeft = true;
    }
    public void PointerUpLeft()
    {
        Instance.moveLeft = false;
    }
    public void PointerDownRight()
    {
        Instance.moveRight = true;
    }
    public void PointerUpRight()
    {
        Instance.moveRight = false;
    }
    public void PointerDownUP()
    {
        Instance.moveUp = true;
    }
    public void PointerUpUP()
    {
        Instance.moveUp = false;
    }
    public void PointerDownDOWN()
    {
        Instance.moveDown = true;
    }
    public void PointerUpDown()
    {
        Instance.moveDown = false;
    }

}
