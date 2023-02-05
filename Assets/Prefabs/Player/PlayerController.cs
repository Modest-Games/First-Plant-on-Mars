using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerControllerDelegate();
    public static PlayerControllerDelegate CollectedWater;
    public static PlayerControllerDelegate HitBones;
    public static PlayerControllerDelegate HitObstacle;

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
    public float occillateAmount;
    public float occillateSpeed;
    public float timeBetweenFlips;
    
    public float _distanceTravelled;
    public float _distanceOfLastRoot = 0f;

    void Start()
    {
        playerProperties = GetComponent<PlayerProperties>();
        InvokeRepeating("Flip", 0f, timeBetweenFlips); ;
    }

    void Update()
    {
        if (!GameController.Instance.alive) return;

        PlayerInput();

        // Update distance travelled
        _distanceTravelled += Time.deltaTime * GameController.Instance._gameSpeed;

        // occillate on the x axis
        //transform.Find("Art").localPosition = new Vector2(
        //    occillateAmount * Mathf.PerlinNoise(Time.time * occillateSpeed, 0) /* Mathf.Sin(Time.time * occillateSpeed) */, 0);

        //LineRenderer rootLine = RootSim.Instance._rootLine;
        //Vector3 rootDir = rootLine.GetPosition(rootLine.positionCount - 1) 
        //    - rootLine.GetPosition(rootLine.positionCount - 2);

        //transform.Find("Art").rotation = Quaternion.FromToRotation(Vector2.down, new Vector2(rootDir.x, rootDir.y));
    }

    private void Flip()
    {
        if (!GameController.Instance.alive) return;

        transform.Find("Art").GetChild(0).GetComponent<SpriteRenderer>().flipX =
            !transform.Find("Art").GetChild(0).GetComponent<SpriteRenderer>().flipX;
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
                playerProperties.Score += 50;
                if (CollectedWater != null) CollectedWater();
                break;

            case "Rock(Clone)":
                playerProperties.Life -= 2;
                HitObstacle?.Invoke();
                break;

            case "Bones(Clone)":
                playerProperties.Life -= 5;
                HitBones?.Invoke();
                break;

            case "UFO(Clone)":
                playerProperties.Life -= 10;
                HitObstacle?.Invoke();
                break;

            case "Pellets(Clone)":
                playerProperties.Score += 100;
                break;
        }

        Destroy(collision.gameObject);
    }
}
