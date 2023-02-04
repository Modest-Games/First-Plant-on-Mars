using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float yThreshold = 10.1f;
    [SerializeField] private float speed = 5f;
    public bool isScrolling = true;

    private Transform bgOne;
    private Transform bgTwo;

    void Start()
    {
        bgOne = transform.GetChild(0);
        bgTwo = transform.GetChild(1);
    }

    void Update()
    {
        if (isScrolling)
            ScrollBackground();
    }

    private void ScrollBackground()
    {
        bgOne.transform.localPosition += speed * Time.deltaTime * Vector3.up;
        bgTwo.transform.localPosition += speed * Time.deltaTime * Vector3.up;

        foreach (Transform bgSprite in transform)
        {
            if (bgSprite.localPosition.y > yThreshold)
            {
                bgSprite.transform.localPosition = Vector3.up * -yThreshold;
            }
        }
    }
}
