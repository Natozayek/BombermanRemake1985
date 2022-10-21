using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChase : MonoBehaviour
{
    [SerializeField]
    private EnemyAI EnemyAI;
    private Collider2D myCollider;
    private Transform myTransform;
  [SerializeField] GameObject ChildGameObject1; 
    private void Start()
    {
       // myTransform = GetComponent<Transform>();
        myCollider = GetComponent<Collider2D>();
      
    }
    private void Update()
    {
         myCollider.transform.position = ChildGameObject1.transform.position;

    }
    private void OnTriggerEnter2D(Collider2D other)
    { //Player in range
        if(other.gameObject.tag == "Player")
        {
            EnemyAI.canSeePlayer = true;

        }    
    }

    private void OnTriggerExit2D(Collider2D other)
    {//Player out of range
        if (other.gameObject.tag == "Player")
        {
            EnemyAI.canSeePlayer = false;
        }
    }

}
