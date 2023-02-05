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
        //_rootLine = rootContainer.GetComponent<LineRenderer>();
        backgroundMaterial = GetComponent<MeshRenderer>().material;

        backgroundMaterial.SetVector("_Offset", Vector2.zero);
    }

    public void StartScrollingLoop()
    {
        StartCoroutine(ScrollingLoop());
    }

    private IEnumerator ScrollingLoop()
    {
        ResetBG();

        while (GameController.Instance.alive)
        {
            var matOffset = backgroundMaterial.GetVector("_Offset");

            matOffset.x += GameController.Instance.worldScrollingDir.x;
            matOffset.y += GameController.Instance.worldScrollingDir.y;

            backgroundMaterial.SetVector("_Offset", matOffset);

            yield return null;
        }
    }

    public void ResetBG()
    {
        backgroundMaterial.SetVector("_Offset", Vector4.zero);
    }
}
