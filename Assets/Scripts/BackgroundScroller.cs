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

    private Material backgroundMaterial;
    private LineRenderer _rootLine;

    void Start()
    {
        // setup variables
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
        var matOffset = backgroundMaterial.mainTextureOffset;

        if (matOffset.y < -100f)
            matOffset.y = 0f;

        if (matOffset.x < -100f || matOffset.x > 100f)
            matOffset.x = 0f;

        matOffset += GameController.Instance.worldScrollingDir;

        backgroundMaterial.mainTextureOffset = matOffset;
    }
}
