using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSim : MonoBehaviour
{
    #region Singleton
    public static RootSim Instance { get; private set; }
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

    public float distanceBetweenRootSegments;
    public int maxLineSegments;
    public Transform _rootContainer;
    public float rootSpeed;

    private LineRenderer _rootLine;
    private float _distanceOfLastRoot = 0f;
    private Vector3[] startingRootPositions;

    void Start()
    {
        // setup variables
        _rootLine = _rootContainer.GetComponent<LineRenderer>();
        startingRootPositions = new Vector3[] { _rootLine.GetPosition(0) , _rootLine.GetPosition(1) };
    }

    void Update()
    {
        if (!GameController.Instance.alive) return;

        if (PlayerController.Instance._distanceTravelled - _distanceOfLastRoot >= distanceBetweenRootSegments)
            SpawnRootSegment();

        // update all roots
        ScrollBGRoot(GameController.Instance.worldScrollingDir);
    }

    private void SpawnRootSegment()
    {
        _rootLine.positionCount++;
        _distanceOfLastRoot = PlayerController.Instance._distanceTravelled;
    }

    private void ScrollBGRoot(Vector2 moveAmount)
    {
        // check if the root has too many segments
        if (_rootLine.positionCount > maxLineSegments)
        {

        }

        for (int i = 0; i < _rootLine.positionCount - 1; i++)
        {
            _rootLine.SetPosition(i, _rootLine.GetPosition(i) - new Vector3(moveAmount.x, moveAmount.y, 0f));
        }

        int j = _rootLine.positionCount - 1;

        _rootLine.SetPosition(j, PlayerController.Instance.transform.Find("Art").position);
    }

    public void ResetRoot()
    {
        if (_rootLine.positionCount <= 2) return;

        _rootLine.positionCount = 2;
        _rootLine.SetPositions(startingRootPositions);
        _distanceOfLastRoot = 0f;
    }
}
