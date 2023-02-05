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

    public delegate void GameControllerDelegate();
    public static GameControllerDelegate playerDied;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerProperties playerProperties;
    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private Transform gameOverPanel;
    [SerializeField] private AudioSource music;

    public bool alive = false;

    [HideInInspector] public Vector2 worldScrollingDir;
    public float _gameSpeed;
    public Vector2 _virtualPlayerPosition;

    private float previousDepth = -5f;

    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        music.volume = 1;
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

            // Life decrement:
            playerProperties.Life -= Time.deltaTime * 0.75f;
            if (playerProperties.Life <= 0)
            {
                alive = false;
                if (playerDied != null) playerDied();
                gameOverPanel.gameObject.SetActive(true);
                music.volume = 0.4f;
            }

            // Score increment:
            playerProperties.Score += (_virtualPlayerPosition.y - previousDepth) * 2f;
            previousDepth = _virtualPlayerPosition.y;

            yield return null;
        }

        backgroundScroller.StopAllCoroutines();
        objectSpawner.StopAllCoroutines();

        //playerController.gameObject.SetActive(false);
    }
}
