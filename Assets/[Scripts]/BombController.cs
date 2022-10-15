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
    public float bombFueseTime = 2.0f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;


    private void OnEnable()
    {
        bombsRemaining = bombAmount;
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
      
        yield return new WaitForSeconds(1.0f);
        bomb.GetComponent<CircleCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(bombFueseTime);
        bomb.GetComponent<CircleCollider2D>().isTrigger = false;
        Destroy(bomb);


        Explosion explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(pos, Vector2.up, explosionRadius);
        Explode(pos, Vector2.down, explosionRadius);
        Explode(pos, Vector2.left, explosionRadius);
        Explode(pos, Vector2.right, explosionRadius);

        Destroy(bomb.gameObject);
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

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile =destructibleTiles.GetTile(cell);

        if(tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }
}
