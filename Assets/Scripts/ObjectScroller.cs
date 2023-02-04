using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroller : MonoBehaviour
{
    private void Update()
    {
        transform.position -= (Vector3)GameController.Instance.worldScrollingDir;
    }
}
