using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxRotationAngle;

    private Vector2 _moveDirection;

    void Start()
    {
        // setup variables
        _moveDirection = Vector2.down;
    }

    void Update()
    {
        Debug.Log(transform.eulerAngles.z);

        // update rotation using controls
        if (Input.GetKey(KeyCode.RightArrow) && 
            (transform.eulerAngles.z < 180f || transform.eulerAngles.z > 360f - maxRotationAngle))
        {
            transform.Rotate(new Vector3(0, 0, 1), -0.1f);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) &&
            (transform.eulerAngles.z > 180f || transform.eulerAngles.z < maxRotationAngle))
        {
            transform.Rotate(new Vector3(0, 0, 1), 0.1f);
        }

        // allowed range: 270 -> 90
        // (360 - 90) -> 90

        // left = angle > 180 || angle < max
        // right = angle < 180 || angle > 360 - max


        // update movement direction
    }
}
