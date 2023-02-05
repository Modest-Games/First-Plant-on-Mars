using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroller : MonoBehaviour
{
    private Vector3 startPosition;

    private void Awake()
    {
        GameController.gameReset += OnGameReset;
        startPosition = gameObject.transform.position;
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        GameController.gameReset -= OnGameReset;
    }

    private void Update()
    {
        if (!GameController.Instance.alive) return;

        transform.position -= (Vector3)GameController.Instance.worldScrollingDir;
    }

    private void OnGameReset()
    {
        transform.position = startPosition;
    }
}
