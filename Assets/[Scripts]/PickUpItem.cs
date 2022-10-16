using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public enum ItemType
    {
        EXTRABOMB,
        BLASTRADIUS,
        EXTRALIFE,
        SPEEDUP
    }

    public ItemType type;
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
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            OnItemPickUp(other.gameObject); 
        }
    }
}
