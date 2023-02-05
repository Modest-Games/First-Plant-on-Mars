using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartingSeed : MonoBehaviour
{
    public Sprite[] sprites;
    public TextMeshProUGUI text;

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

        text.text = "ROOTS: " + (GameController.Instance.playthrough + 1) + " / 5";
    }
}
