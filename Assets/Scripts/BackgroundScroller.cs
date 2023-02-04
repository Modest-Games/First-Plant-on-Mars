using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    #region Singleton
    public static BackgroundScroller Instance { get; private set; }
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

    [Header("Config")]
    //[SerializeField] private float speed = 5f;
    public bool isScrolling = true;
    public Transform rootContainer;

    [HideInInspector] public Vector2 worldScrollingDir;

    private PlayerController _playerController;
    private Material backgroundMaterial;
    private LineRenderer _rootLine;

    void Start()
    {
        // setup variables
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        backgroundMaterial = GetComponent<MeshRenderer>().material;
        _rootLine = rootContainer.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isScrolling)
            ScrollBackground();
    }

    

    private void ScrollBackground()
    {
        Vector2 playerMoveDirection = new Vector2();
        var matOffset = backgroundMaterial.mainTextureOffset;

        if (matOffset.y < -100f)
            matOffset.y = 0f;

        if (matOffset.x < -100f || matOffset.x > 100f)
            matOffset.x = 0f;

        playerMoveDirection = _playerController.transform.rotation * Vector2.down;
        worldScrollingDir = Time.deltaTime * _playerController.speed * playerMoveDirection;
        matOffset += worldScrollingDir;

        backgroundMaterial.mainTextureOffset = matOffset;
    }
}
