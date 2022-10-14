using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite[] animationSprites;
    public float animationTime = 0.25f;
    public int animationFrame;
    public bool loop = true;
    public bool idle = true;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
       spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame()
    {
        animationFrame++;
        // if beyond bounds of array go back to start frame
        if(loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        if(idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        // Guarantee that script wont acces animationSprites array out of bounds
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
