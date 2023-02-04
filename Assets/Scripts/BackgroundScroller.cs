using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 5f;
    public bool isScrolling = true;

    private PlayerController _playerController;
    private Material backgroundMaterial;

    void Start()
    {
        // setup variables
        _playerController = GameObject.FindObjectOfType<PlayerController>();
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

        if (matOffset.x < -100f || matOffset.x > 100f)
            matOffset.x = 0f;

        else
        {

            Vector2 playerMoveDirection = _playerController.transform.rotation * Vector2.down;
            
            matOffset += Time.deltaTime * speed * playerMoveDirection;

        }

        backgroundMaterial.mainTextureOffset = matOffset;
    }
}
