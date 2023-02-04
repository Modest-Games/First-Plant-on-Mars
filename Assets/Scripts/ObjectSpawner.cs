using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float waterSpawnRate = 5f;
    [SerializeField] private float rockSpawnRate = 0.5f;

    [SerializeField] private GameObject waterDroplet;
    [SerializeField] private GameObject baseRock;

    private Vector2 lastSpawnLocation = Vector2.zero;

    private void Start()
    {
        StartCoroutine(WaterSpawnLoop());
        StartCoroutine(RockSpawnLoop());
    }

    private IEnumerator WaterSpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waterSpawnRate);

            Instantiate(waterDroplet, RandomPosition(), Quaternion.identity);
        }
    }

    private IEnumerator RockSpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(rockSpawnRate);

            Instantiate(baseRock, RandomPosition(), Quaternion.identity);
        }
    }

    private Vector2 RandomPosition()
    {
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        var screenDimensions = new Vector2(screenWidth, screenHeight);

        var gameAreaMaxBounds = Camera.main.ScreenToWorldPoint(screenDimensions);
        var gameAreaMinBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Debug.Log(gameAreaMinBounds);

        var randomX = Random.Range(gameAreaMinBounds.x * 0.9f, gameAreaMaxBounds.x * 0.9f);

        var randomPosition = new Vector2(randomX, gameAreaMinBounds.y * 1.1f);

        if (lastSpawnLocation == Vector2.zero)
            return randomPosition;

        while (Vector2.Distance(randomPosition, lastSpawnLocation) < 5.0f)
            randomPosition = new Vector2(randomX, gameAreaMinBounds.y * 1.1f);

        return randomPosition;
    }
}
