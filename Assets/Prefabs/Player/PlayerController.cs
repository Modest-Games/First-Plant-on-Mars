using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance { get; private set; }
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

    private PlayerProperties playerProperties;

    public float maxRotationAngle;
    public float turnSpeed;
    public float defaultSpeed;
    
    public float _distanceTravelled;
    public float _distanceOfLastRoot = 0f;

    void Start()
    {
        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update()
    {
        if (!GameController.Instance.alive) return;

        PlayerInput();

        // Update distance travelled
        _distanceTravelled += Time.deltaTime * GameController.Instance._gameSpeed;
    }

    private void PlayerInput()
    {
        // Update rotation using controls
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "Water(Clone)":
                playerProperties.Life += 5;
                break;

            case "Rock(Clone)":
                playerProperties.Life -= 2;
                break;

            case "Bones(Clone)":
                playerProperties.Life -= 5;
                break;

            case "UFO(Clone)":
                playerProperties.Life -= 10;
                break;
        }

        Destroy(collision.gameObject);
    }
}
