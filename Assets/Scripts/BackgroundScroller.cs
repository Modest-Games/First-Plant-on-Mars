using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 5f;
    public bool isScrolling = true;

    private Material backgroundMaterial;

    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
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

        else
            matOffset.y -= Time.deltaTime * speed;

        backgroundMaterial.mainTextureOffset = matOffset;
    }
}
