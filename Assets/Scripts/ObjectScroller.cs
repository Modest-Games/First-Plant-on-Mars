using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroller : MonoBehaviour
{
    private void Update()
    {
        if (!GameController.Instance.alive) return;

        transform.position -= (Vector3)GameController.Instance.worldScrollingDir;
    }
}
