using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxRotationAngle;
    public float turnSpeed;
    public float defaultSpeed;
    public float speed;
    
    public float _distanceTravelled;
    public float _distanceOfLastRoot = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        // update rotation using controls
        if (Input.GetKey(KeyCode.RightArrow) && 
            (transform.eulerAngles.z < 180f || transform.eulerAngles.z > 360f - maxRotationAngle))
        {
            transform.Rotate(new Vector3(0, 0, 1), -1 * Time.deltaTime * turnSpeed);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) &&
            (transform.eulerAngles.z > 180f || transform.eulerAngles.z < maxRotationAngle))
        {
            transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * turnSpeed);
        }

        // update distance travelled
        _distanceTravelled += Time.deltaTime * speed;


        
    }
}
