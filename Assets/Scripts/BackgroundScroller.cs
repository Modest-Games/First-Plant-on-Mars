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
    public bool isScrolling = true;
    public Transform rootContainer;

    private Material backgroundMaterial;

    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
    }

    public void StartScrollingLoop()
    {
        StartCoroutine(ScrollingLoop());
    }

    private IEnumerator ScrollingLoop()
    {
        while (GameController.Instance.alive)
        {
            var matOffset = backgroundMaterial.mainTextureOffset;

            if (matOffset.y < -100f)
                matOffset.y = 0f;

            if (matOffset.x < -100f || matOffset.x > 100f)
                matOffset.x = 0f;

            matOffset += GameController.Instance.worldScrollingDir;

            backgroundMaterial.mainTextureOffset = matOffset;

            yield return null;
        }
    }
}
