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

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerProperties playerProperties;
    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private ObjectSpawner objectSpawner;

    public bool alive = false;

    [HideInInspector] public Vector2 worldScrollingDir;
    public float _gameSpeed;
    public Vector2 _virtualPlayerPosition;

    void Start()
    {
        _virtualPlayerPosition = new Vector2(0, -5);

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(1);

        alive = true;

        backgroundScroller.StartScrollingLoop();
        objectSpawner.StartObjectSpawning();

        while (alive)
        {
            Vector2 playerMoveDirection = playerController.transform.rotation * Vector2.down;
            worldScrollingDir = Time.deltaTime * _gameSpeed * playerMoveDirection;

            // move the player's virtual position
            _virtualPlayerPosition -= worldScrollingDir;

            playerProperties.Life -= Time.deltaTime * 0.75f;
            if (playerProperties.Life <= 0)
                alive = false;

            yield return null;
        }

        backgroundScroller.StopAllCoroutines();
        objectSpawner.StopAllCoroutines();

        playerController.gameObject.SetActive(false);
    }
}
