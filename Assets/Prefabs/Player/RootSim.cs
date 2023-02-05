using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSim : MonoBehaviour
{
    public float distanceBetweenRootSegments;
    public int maxLineSegments;
    public Transform _rootContainer;
    public float rootSpeed;

    private PlayerController _playerController;
    private LineRenderer _rootLine;
    private float _distanceOfLastRoot = 0f;

    void Start()
    {
        // setup variables
        _rootLine = _rootContainer.GetComponent<LineRenderer>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (!GameController.Instance.alive) return;

        if (_playerController._distanceTravelled - _distanceOfLastRoot >= distanceBetweenRootSegments)
            SpawnRootSegment();

        // update all roots
        ScrollBGRoot(GameController.Instance.worldScrollingDir);
    }

    private void SpawnRootSegment()
    {
        _rootLine.positionCount++;
        _distanceOfLastRoot = _playerController._distanceTravelled;
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
}
