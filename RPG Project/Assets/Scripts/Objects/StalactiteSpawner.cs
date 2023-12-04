using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject stalactite;
    private GameObject spawnedStalactite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnedStalactite = Instantiate(stalactite);
            spawnedStalactite.transform.position = new Vector2(transform.position.x, transform.position.y + 7);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == spawnedStalactite)
        {
            Destroy(collision.gameObject);
            spawnedStalactite = null;
        }
    }
}
