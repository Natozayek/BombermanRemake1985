using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
   // private new SpriteRenderer spriteRenderer;
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    [Header("Input")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

   
    [Header("Sprites")]
    public SpriteRendererController spriteAnimUp;
    public SpriteRendererController spriteAnimDown;
    public SpriteRendererController spriteAnimLeft;
    public SpriteRendererController spriteAnimRight;
    private SpriteRendererController activeAnimation;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
       activeAnimation =  spriteAnimDown;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(inputUp))
        {
            Debug.Log(" NEW DIRECTION UP");
            SetDirection(Vector2.up, spriteAnimUp);
           
        }
        else if (Input.GetKey(inputDown))
        {
            Debug.Log(" NEW DIRECTION DOWN");
            SetDirection(Vector2.down, spriteAnimDown);
            
        }
        else if (Input.GetKey(inputLeft))
        {
            Debug.Log(" NEW DIRECTION LEFT");
            SetDirection(Vector2.left, spriteAnimLeft);
            
        }
        else if (Input.GetKey(inputRight))
        {
            Debug.Log(" NEW DIRECTION RIGHT");
            SetDirection(Vector2.right, spriteAnimRight);
            
        }
        else
        {
            Debug.Log("NOT MOVING");
            SetDirection(Vector2.zero, activeAnimation);
        }
    }

    //Moving position to a new direction
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }

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
}
