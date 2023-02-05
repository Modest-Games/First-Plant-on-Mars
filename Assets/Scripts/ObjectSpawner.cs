using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float waterSpawnRate = 5f;
    [SerializeField] private float obstacleSpawnRate = 5f;

    [SerializeField] private GameObject waterDroplet;
    [SerializeField] private GameObject[] staticObstacles;

    private Vector2 previousSpawnLocation = Vector2.zero;

    public void StartObjectSpawning()
    {
        StartCoroutine(WaterSpawnLoop());
        StartCoroutine(ObstacleSpawnLoop());
    }

    private IEnumerator WaterSpawnLoop()
    {
        while (GameController.Instance.alive)
        {
            yield return new WaitForSeconds(waterSpawnRate);

            Instantiate(waterDroplet, RandomPosition(true), Quaternion.identity);
        }
    }

    private IEnumerator ObstacleSpawnLoop()
    {
        while (GameController.Instance.alive)
        {
            yield return new WaitForSeconds(obstacleSpawnRate);

            var randomProbability = Random.Range(1, 11);
            var randomRotation = Vector3.zero;
            int randomObstacleIndex;
            switch (randomProbability)
            {
                // Bones
                case 8:
                case 9:
                    randomObstacleIndex = 1;
                    randomRotation = Vector3.forward * Random.Range(-15f, 30f);
                    break;

                // UFO
                case 10:
                    randomObstacleIndex = 2;
                    randomRotation = Vector3.forward * Random.Range(-80f, 0f);
                    break;

                // Rock
                default:
                    randomObstacleIndex = 0;
                    break;
            }

            var spawnedObject = Instantiate(staticObstacles[randomObstacleIndex], RandomPosition(false), Quaternion.Euler(randomRotation));

            if (randomObstacleIndex == 0)
                spawnedObject.transform.localScale *= Random.Range(0.75f, 1.25f);
        }
    }

    private Vector2 RandomPosition(bool water)
    {
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        var screenDimensions = new Vector2(screenWidth, screenHeight);

        var gameAreaMaxBounds = Camera.main.ScreenToWorldPoint(screenDimensions);
        var gameAreaMinBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Debug.Log(gameAreaMinBounds);

        var rangeModifier = water ? 0.4f : 0.9f;

        var randomPosition = new Vector2(Random.Range(gameAreaMinBounds.x * rangeModifier, gameAreaMaxBounds.x * rangeModifier), gameAreaMinBounds.y * 1.1f);

        if (previousSpawnLocation == Vector2.zero)
        {
            previousSpawnLocation = randomPosition;
            return randomPosition;
        }

        while (Vector2.Distance(randomPosition, previousSpawnLocation) < 2.0f)
            randomPosition = new Vector2(Random.Range(gameAreaMinBounds.x * rangeModifier, gameAreaMaxBounds.x * rangeModifier), gameAreaMinBounds.y * 1.1f);

        previousSpawnLocation = randomPosition;
        return randomPosition;
    }
}
