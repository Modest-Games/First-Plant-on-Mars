using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 10f;

    [SerializeField] private GameObject waterDroplet;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            Instantiate(waterDroplet, RandomPosition(), Quaternion.identity);
        }
    }

    private Vector2 RandomPosition()
    {
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        var screenDimensions = new Vector2(screenWidth, screenHeight);

        var gameAreaMaxBounds = Camera.main.ScreenToWorldPoint(screenDimensions);
        var gameAreaMinBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);

        var randomX = Random.Range(gameAreaMaxBounds.x * 0.1f, gameAreaMaxBounds.x * 0.9f);

        return new Vector2(randomX, gameAreaMinBounds.y * 1.1f);
    }
}
