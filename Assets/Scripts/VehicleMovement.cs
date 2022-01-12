using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceTraveled;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _distanceTraveled += _speed * Time.deltaTime;
        _transform.position = _pathCreator.path.GetPointAtDistance(_distanceTraveled);
        _transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTraveled) * Quaternion.Euler(0, 0, 90);
    }
}
