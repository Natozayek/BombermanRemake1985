using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode InputDropBomb = KeyCode.Space;
    public float bombFueseTime = 3.0f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public Explosion explosion2;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public GameObject destructibleTiles;
    public Destructible destructiblePrefab;


    private void OnEnable()
    {
        bombsRemaining = bombAmount;
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Explosion");
        explosion2 = explosionPrefab.GetComponent<Explosion>();
    }

    private void Start()
    {
        destructibleTiles = GameObject.Find("DestructibleTiles");
      
    }
    private void Update()
    {
        if(bombsRemaining > 0 && Input.GetKeyDown(InputDropBomb))
        {
            StartCoroutine(PlaceBomb());

        }
    }

    public IEnumerator PlaceBomb()
    {
        Vector2 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);


        GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
        bombsRemaining--;
     
        yield return new WaitForSeconds(bombFueseTime);
      
        Explosion explosion = Instantiate(explosionPrefab.GetComponent<Explosion>(), pos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(pos, Vector2.up, explosionRadius);
        Explode(pos, Vector2.down, explosionRadius);
        Explode(pos, Vector2.left, explosionRadius);
        Explode(pos, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
           ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosion2, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.GetComponent<Tilemap>().WorldToCell(position);
        TileBase tile =destructibleTiles.GetComponent<Tilemap>().GetTile(cell);

        if(tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.GetComponent<Tilemap>().SetTile(cell, null);
            ScoreManager.instance.AddScore(100);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

}
