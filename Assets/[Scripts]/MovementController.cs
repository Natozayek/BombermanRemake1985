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

    [Header("Camera")]
    Camera cam;
    bool translate;
    [Range(0f, 2f)]
    float moveCam = 0;
    float MoveCamRight2 = 0;
    float moveCamRight = 0;




    private void Awake()
    {
        cam = Camera.main;
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
        MoveCamera();

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

    public void MoveUp()
    {
        SetDirection(Vector2.up, spriteAnimUp);
    }
}
