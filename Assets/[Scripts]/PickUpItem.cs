using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] AudioSource itemPickUP;

    //Type of pick up items
    public enum ItemType
    {
        EXTRABOMB,
        BLASTRADIUS,
        EXTRALIFE,
        SPEEDUP
    }

    public ItemType type;

    //Function to execute ability on collision with pick-up prefab
    private void OnItemPickUp(GameObject player)
    {

        switch (type)
        { 
            case ItemType.EXTRABOMB:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.BLASTRADIUS:
                player.GetComponent<BombController>().explosionRadius++;
                break;

            case ItemType.EXTRALIFE:
                GameManager.instance.IncreaseLives();
                break;

            case ItemType.SPEEDUP:
                player.GetComponent<MovementController>().speed++;
                break;

        }
        itemPickUP.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject,1.4f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            OnItemPickUp(other.gameObject);
            
        }
    }
}
