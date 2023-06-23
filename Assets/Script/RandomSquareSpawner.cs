using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSquareSpawner : MonoBehaviour
{
    public GameObject squarePrefab;
    public Color color1 = new Color(241f / 255f, 17f / 255f, 94f / 255f);
    public Color color2 = new Color(1f, 1f, 1f);
    public float colorChangeInterval = 1f;

    public GameObject currentSquare;
    private SpriteRenderer spriteRenderer;
    private Collider2D environmentCollider;

    private Vector3 playerPosition;

    private void Start()
    {
        environmentCollider = GetComponent<Collider2D>();
        SpawnSquare();
    }

    private void Update()
    {
        if (currentSquare == null)
        {
            SpawnSquare();
        }
    }

    public void SpawnSquare()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        currentSquare = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
        spriteRenderer = currentSquare.GetComponent<SpriteRenderer>();

        if (currentSquare != null)
        {
            StartCoroutine(ChangeColor(currentSquare));
        }
    }

    public Vector3 GetRandomSpawnPosition()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        playerPosition = player.transform.position;

        float spawnX;
        do
{
    spawnX = Random.Range(-5f, 5f);
} while (Mathf.Abs(spawnX - playerPosition.x) <= 3f);


        float spawnY = GetRandomYPosition();
        return new Vector3(spawnX, spawnY, 0f);
    }
    else
    {
        Debug.LogError("Player object not found!");
        return Vector3.zero;
    }
}


    private float GetRandomYPosition()
    {
        float[] possibleYPositions = { 3f, 1f, -1f, -3f };
        int randomIndex = Random.Range(0, possibleYPositions.Length);
        return possibleYPositions[randomIndex];
    }

    private IEnumerator ChangeColor(GameObject square)
    {
        SpriteRenderer squareRenderer = square.GetComponent<SpriteRenderer>();

        while (true)
        {
            if (squareRenderer != null)
            {
                squareRenderer.color = squareRenderer.color == color1 ? color2 : color1;
            }

            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    public bool IsCollidingWithPlayer(Collider2D playerCollider)
    {
        if (currentSquare != null)
        {
            Collider2D squareCollider = currentSquare.GetComponent<Collider2D>();

            if (environmentCollider != null && !environmentCollider.bounds.Intersects(playerCollider.bounds))
            {
                return squareCollider.bounds.Intersects(playerCollider.bounds) && currentSquare.transform.position.x != transform.position.x;
            }
        }
        return false;
    }

    public void DestroySquare()
    {
        if (currentSquare != null)
        {
            Destroy(currentSquare.gameObject);
            currentSquare = null;
        }
    }
}
