using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1.0f;

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }
}
