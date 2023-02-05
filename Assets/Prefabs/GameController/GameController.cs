using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [HideInInspector] public Vector2 worldScrollingDir;
    public float _gameSpeed;
    public Vector2 _virtualPlayerPosition;

    void Start()
    {
        _virtualPlayerPosition = new Vector2(0, -5);
    }

    void Update()
    {
        Vector2 playerMoveDirection = PlayerController.Instance.transform.rotation * Vector2.down;
        worldScrollingDir = Time.deltaTime * GameController.Instance._gameSpeed * playerMoveDirection;

        // move the player's virtual position
        _virtualPlayerPosition -= worldScrollingDir;
    }
}