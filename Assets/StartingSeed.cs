using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSeed : MonoBehaviour
{
    public Sprite[] sprites;

    private void Awake()
    {
        GameController.gameReset += OnGameReset;
    }

    private void OnDestroy()
    {
        GameController.gameReset -= OnGameReset;
    }

    private void OnGameReset()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[GameController.Instance.playthrough];
    }
}
